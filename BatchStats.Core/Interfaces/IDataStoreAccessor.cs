using System.Threading.Tasks;

namespace BatchStats.Core.Interfaces
{
    public interface IDataStoreAccessor
    {
        Task AddDocument<TDocument>(string collection, TDocument document);

        Task<TDocument[]> GetDocuments<TDocument>(string collection, int skip, int take);
    }
}
