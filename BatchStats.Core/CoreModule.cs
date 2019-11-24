using BatchStats.Core.Commands;
using BatchStats.Core.Infrastructure;
using BatchStats.Core.Interfaces;
using BatchStats.Core.Queries;
using BatchStats.Core.Services;
using BatchStats.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BatchStats.Core
{
    public class CoreModule : IModule
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();

            services.AddTransient<IQueryHandler<GetCaclDataQuery, CalcData[]>, GetCalcDataQueryHandler>();
            services.AddTransient<IQueryHandler<GetRawDataQuery, DataPoint[]>, GetRawDataQueryHandler>();

            services.AddTransient<ICommandHandler<AddTelemetryCommand>, AddTelemetryCommandHandler>();
            services.AddTransient<ICommandHandler<AddAggregationsCommand>, AddAggregationsCommandHandler>();

            services.AddTransient<IBatchCachingService, BatchCachingService>();
            services.AddTransient<ICalcService, CalcService>();
        }
    }
}