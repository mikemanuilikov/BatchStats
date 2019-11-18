using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchStats.Core.Services
{
    public class EventBus : IEventBus
    {
        private readonly IDictionary<EventTopic, ICollection<IEventSubscriber>> subscribers;

        public EventBus()
        {
            subscribers = new Dictionary<EventTopic, ICollection<IEventSubscriber>>();
        }

        public void Publish(EventTopic topic, IMessage message)
        {
            if (subscribers.ContainsKey(topic))
            {
                Parallel.ForEach(subscribers[topic], async subscriber => await subscriber.HandleAsync(message));
            }
        }

        public void Subscribe(EventTopic topic, IEventSubscriber subscriber)
        {
            if (subscribers.ContainsKey(topic))
            {
                subscribers[topic].Add(subscriber);
            }
            else
            {
                subscribers.Add(topic, new IEventSubscriber[] { subscriber });
            }
        }
    }
}
