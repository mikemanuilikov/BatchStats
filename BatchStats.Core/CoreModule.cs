using BatchStats.Core.Infrastructure;
using BatchStats.Core.Interfaces;
using BatchStats.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Linq;
using System.Reflection;

namespace BatchStats.Core
{
    public class CoreModule : IModule
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();

            services.AddTransient<IBatchCachingService, BatchCachingService>();
            services.AddTransient<ICalcService, CalcService>();

            AddAllCommandHandlers(services);
            AddAllQueryHandlers(services);
        }

        private void AddAllCommandHandlers(IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(ICommandHandler<>)))
                .Where(type => type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .AsPublicImplementedInterfaces();
        }

        private void AddAllQueryHandlers(IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(IQueryHandler<,>)))
                .Where(type => type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .AsPublicImplementedInterfaces();
        }
    }
}
