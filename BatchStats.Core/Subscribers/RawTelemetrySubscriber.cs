using BatchStats.Core.Commands;
using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System.Threading.Tasks;

namespace BatchStats.Core.Subscribers
{
    public class RawTelemetrySubscriber : IEventSubscriber
    {
        private readonly ICommandDispatcher dispatcher;

        public EventTopic Topic { get; }

        public RawTelemetrySubscriber(ICommandDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            Topic = EventTopic.RawData;
        }

        public async Task HandleAsync(IMessage message)
        {
            var dp = message as DataPoint;

            if (dp != null)
            {
                await dispatcher.DispatchAsync(new AddTelemetryCommand { SensorData = dp });
            }
        }
    }
}