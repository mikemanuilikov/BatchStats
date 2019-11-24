﻿using System.Threading.Tasks;
using BatchStats.Core.Interfaces;
using BatchStats.Models;
using Microsoft.AspNetCore.SignalR;

namespace BatchStats.Api.Hubs
{
    public class AggregationsNotifier : IEventSubscriber
    {
        private readonly IHubContext<AggregationsHub> hubContext;

        private const string TargetSensorId = "Temperature";

        public EventTopic Topic { get; }

        public AggregationsNotifier(IHubContext<AggregationsHub> hubContext)
        {
            Topic = EventTopic.CalcData;
            this.hubContext = hubContext;
        }

        public async Task HandleAsync(IMessage message)
        {
            var calcData = message as CalcData;

            if (calcData != null && calcData.SensorId == TargetSensorId)
            {
                await hubContext.Clients.All.SendAsync(nameof(IAggregationsHub.PushBatchStats), calcData);
            }
        }
    }
}