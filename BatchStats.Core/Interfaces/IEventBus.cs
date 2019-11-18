using BatchStats.Models;

namespace BatchStats.Core.Interfaces
{
    public interface IEventBus
    {
        void Subscribe(EventTopic topic, IEventSubscriber subscriber);

        void Publish(EventTopic topic, IMessage message);
    }
}
