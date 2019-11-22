using BatchStats.Models;

namespace BatchStats.Core.Interfaces
{
    public interface IEventBus
    {
        void Subscribe(IEventSubscriber subscriber);

        void Publish(EventTopic topic, IMessage message);
    }
}
