using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchStats.Core.Services
{
    public class EventBus : IEventBus
    {
        private readonly List<IEventSubscriber> subscribers;

        public EventBus(IEnumerable<IEventSubscriber> subscribers)
        {
            this.subscribers = subscribers.ToList();
        }

        public void Publish(EventTopic topic, IMessage message)
        {
            Parallel.ForEach(subscribers.Where(x => x.Topic == topic),
                async subscriber => await subscriber.HandleAsync(message));
        }

        public void Subscribe(IEventSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }
    }
}