using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatchStats.Core.Subscribers
{
    public class RawTelemetryProcessor : IEventSubscriber
    {
        private readonly IBatchCachingService batchCachingService;
        private readonly IEventBus eventBus;

        public EventTopic Topic { get; }

        public RawTelemetryProcessor(IBatchCachingService batchCachingService, IEventBus eventBus)
        {
            Topic = EventTopic.RawData;

            this.batchCachingService = batchCachingService;
            this.eventBus = eventBus;
        }

        public Task HandleAsync(IMessage message)
        {
            var dataPoint = message as DataPoint;

            if (dataPoint == null)
            {
                return Task.CompletedTask;
            }

            var currentBatchTelemetry = batchCachingService.GetCurrentBatchTelemetry();

            if (currentBatchTelemetry?.BatchId == dataPoint.BatchId)
            {
                // add telemetry to current batch
                currentBatchTelemetry.DataPoints.Add(dataPoint);
                batchCachingService.AddCurrentBatchTelemetry(currentBatchTelemetry);

                return Task.CompletedTask;
            }
            else if (currentBatchTelemetry != null)
            {
                // send previous batch telemetry to calc service
                eventBus.Publish(EventTopic.BatchData, currentBatchTelemetry);
            }

            // new batch started
            currentBatchTelemetry = new BatchTelemetry
            {
                BatchId = dataPoint.BatchId,
                BatchStartTime = dataPoint.BatchTimestamp,
                DataPoints = new List<DataPoint> { dataPoint }
            };

            batchCachingService.AddCurrentBatchTelemetry(currentBatchTelemetry);

            return Task.CompletedTask;
        }
    }
}
