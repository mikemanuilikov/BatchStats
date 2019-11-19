using BatchStats.Api;
using BatchStats.Api.Hubs;
using BatchStats.Core;
using BatchStats.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchStats
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            foreach (var module in GetDIModules())
            {
                module.ConfigureServices(services);
            }

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CalcDataHub>("/calc-data/hub");
            });
        }

        private IModule[] GetDIModules()
        {
            return new IModule[]
            {
                new CoreModule(),
                new ApiModule()
            };
        }
    }
}
