using BatchStats.Core.Interfaces;
using BatchStats.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BatchStats.Core.Services
{
    public class EventBus : IEventBus
    {
        private readonly List<IEventSubscriber> subscribers;

        public EventBus()
        {
            subscribers = new List<IEventSubscriber>();
        }

        public void Publish(EventTopic topic, IMessage message)
        {
            Parallel.ForEach(subscribers.Where(x => x.Topic == topic), async subscriber =>
            {
                try
                {
                    await subscriber.HandleAsync(message);
                }
                catch (Exception)
                {
                    //todo: logs
                }
            });
        }

        public void Subscribe(IEventSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }
    }
}