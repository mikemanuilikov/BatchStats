using BatchStats.Core.Interfaces;
using BatchStats.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BatchStats.Core
{
    public class CoreModule : IModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheAccessor, CacheAccessor>();
            services.AddSingleton<IEventBus, EventBus>();
          
        }
    }
}