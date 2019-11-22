using System.Threading.Tasks;

namespace BatchStats.Core.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}