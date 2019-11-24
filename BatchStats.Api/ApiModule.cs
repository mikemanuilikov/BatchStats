using BatchStats.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using BatchStats.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BatchStats.Core.Services;
using BatchStats.Core.Subscribers;
using BatchStats.Api.Hubs;
using System.Security.Authentication;

namespace BatchStats.Api
{
    public class ApiModule : IModule
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection(nameof(DbSettings)));
            services.AddSingleton<IDbSettings>(sp => sp.GetRequiredService<IOptions<DbSettings>>().Value);
            
            services.Configure<BatchSettings>(configuration.GetSection(nameof(BatchSettings)));
            services.AddSingleton<IBatchSettings>(sp => sp.GetRequiredService<IOptions<BatchSettings>>().Value);

            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = MongoClientSettings.FromConnectionString(sp.GetService<IDbSettings>().ConnectionString);
                settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

                return new MongoClient(settings);
            });

            services.AddTransient<IMongoDatabase>(sp => sp.GetService<IMongoClient>().GetDatabase(sp.GetService<IDbSettings>().DatabaseName));

            services.AddMemoryCache();
            services.AddSingleton<ICacheAccessor, CacheAccessor>();
           

            services.AddSingleton<IEventSubscriber, RawTelemetryWriter>();
            services.AddSingleton<IEventSubscriber, RawTelemetryProcessor>();
            services.AddSingleton<IEventSubscriber, BatchStatsCalculator>();
            services.AddSingleton<IEventSubscriber, AggregationsWriter>();
            services.AddSingleton<IEventSubscriber, AggregationsNotifier>();

            services.AddSingleton<IEventBus, EventBus>();
        }
    }
}