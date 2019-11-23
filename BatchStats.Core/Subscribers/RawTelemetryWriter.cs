using BatchStats.Core.Commands;
using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System.Threading.Tasks;

namespace BatchStats.Core.Subscribers
{
    public class RawTelemetryWriter : IEventSubscriber
    {
        private readonly ICommandDispatcher dispatcher;

        public EventTopic Topic { get; }

        public RawTelemetryWriter(ICommandDispatcher dispatcher)
        {
            Topic = EventTopic.RawData;

            this.dispatcher = dispatcher;
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