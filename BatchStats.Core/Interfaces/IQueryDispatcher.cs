using System.Threading.Tasks;

namespace BatchStats.Core.Interfaces
{
    public interface IQueryDispatcher
    {
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}
