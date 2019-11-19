using BatchStats.Core.Infrastructure;
using BatchStats.Core.Interfaces;
using BatchStats.Core.Queries;
using BatchStats.Core.Services;
using BatchStats.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BatchStats.Core
{
    public class CoreModule : IModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheAccessor, CacheAccessor>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddTransient<IQueryHandler<GetCaclDataQuery, CalcData[]>, GetCalcDataQueryHandler>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
        }
    }
}