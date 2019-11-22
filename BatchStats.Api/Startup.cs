using BatchStats.Api;
using BatchStats.Api.Hubs;
using BatchStats.Core;
using BatchStats.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchStats
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            foreach (var module in GetDIModules())
            {
                module.ConfigureServices(services, configuration);
            }

            services.AddCors(options => options.AddDefaultPolicy(x => x.AllowAnyOrigin()));
            services.AddSignalR();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CalcDataHub>("/hub/calc-data");
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
