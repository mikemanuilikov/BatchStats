using BatchStats.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using BatchStats.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BatchStats.Core.Services;
using BatchStats.Core.Subscribers;

namespace BatchStats.Api
{
    public class ApiModule : IModule
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection(nameof(DbSettings)));
            services.AddSingleton<IDbSettings>(sp => sp.GetRequiredService<IOptions<DbSettings>>().Value);

            services.AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetService<IDbSettings>().ConnectionString));
            services.AddTransient<IMongoDatabase>(sp => sp.GetService<IMongoClient>().GetDatabase(sp.GetService<IDbSettings>().DatabaseName));

            services.AddMemoryCache();
            services.AddSingleton<ICacheAccessor, CacheAccessor>();
            services.AddSingleton<IEventBus, EventBus>();

            services.AddSingleton<IEventSubscriber, RawTelemetrySubscriber>();
        }
    }
}