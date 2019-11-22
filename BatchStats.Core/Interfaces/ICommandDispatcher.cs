using System.Threading.Tasks;

namespace BatchStats.Core.Interfaces
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync(ICommand command);
    }
}
