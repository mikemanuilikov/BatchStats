using BatchStats.Core.Interfaces;
using BatchStats.Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BatchStats.Api
{
    public class ApiModule : IModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataStoreAccessor, DataStoreAccessor>();
            services.AddTransient<IMongoDatabase>(_ => new MongoClient().GetDatabase("BatchStats"));
        }
    }
}
