using BatchStats.Models;
using System.Threading.Tasks;

namespace BatchStats.Core.Interfaces
{
    public interface IEventSubscriber
    {
        Task HandleAsync(IMessage message);
    }
}
