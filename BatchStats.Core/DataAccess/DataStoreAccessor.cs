using BatchStats.Core.Interfaces;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace BatchStats.Core.DataAccess
{
    public class DataStoreAccessor : IDataStoreAccessor
    {
        private readonly IMongoDatabase database;

        public DataStoreAccessor(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task AddDocument<TDocument>(string collectionName, TDocument document)
        {
            var collection = database.GetCollection<TDocument>(collectionName);
            await collection.InsertOneAsync(document);
        }

        public Task<TDocument[]> GetDocuments<TDocument>(string collectionName, int skip, int take)
        {
            var collection = database.GetCollection<TDocument>(collectionName);
            return Task.FromResult(collection
                .AsQueryable()
                .Skip(skip)
                .Take(take)
                .ToArray());
        }
    }
}