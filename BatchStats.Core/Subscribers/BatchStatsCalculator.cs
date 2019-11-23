using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System.Threading.Tasks;

namespace BatchStats.Core.Subscribers
{
    public class BatchStatsCalculator : IEventSubscriber
    {
        private readonly ICalcService calcService;
        private readonly IEventBus eventBus;

        public EventTopic Topic { get; }

        public BatchStatsCalculator(ICalcService calcService, IEventBus eventBus)
        {
            Topic = EventTopic.BatchData;

            this.calcService = calcService;
            this.eventBus = eventBus;
        }

        public async Task HandleAsync(IMessage message)
        {
            var batchTelemetry = message as BatchTelemetry;

            if (batchTelemetry != null)
            {
                var aggregations = await calcService.Calculate(batchTelemetry);

                foreach (var calcData in aggregations)
                {
                    // todo: bulk publish?
                    eventBus.Publish(EventTopic.CalcData, calcData);
                }
            }
        }
    }
}
