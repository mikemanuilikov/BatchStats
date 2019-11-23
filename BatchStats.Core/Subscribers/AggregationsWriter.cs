using BatchStats.Core.Commands;
using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System.Threading.Tasks;

namespace BatchStats.Core.Subscribers
{
    public class AggregationsWriter : IEventSubscriber
    {
        private readonly ICommandDispatcher dispatcher;

        public EventTopic Topic { get; }

        public AggregationsWriter(ICommandDispatcher dispatcher)
        {
            Topic = EventTopic.CalcData;

            this.dispatcher = dispatcher;
        }

        public async Task HandleAsync(IMessage message)
        {
            var calcData = message as CalcData;

            if (calcData != null)
            {
                await dispatcher.DispatchAsync(new AddAggregationsCommand { CalcData = calcData });
            }
        }
    }
}