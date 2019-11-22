using BatchStats.Models;
using System.Threading.Tasks;

namespace BatchStats.Core.Interfaces
{
    public interface IEventSubscriber
    {
        EventTopic Topic { get; }

        Task HandleAsync(IMessage message);
    }
}