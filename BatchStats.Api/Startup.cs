using BatchStats.Api;
using BatchStats.Api.Hubs;
using BatchStats.Core;
using BatchStats.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

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

            services.AddCors(options => options.AddDefaultPolicy(x => 
                                            x.AllowCredentials()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .WithOrigins("http://localhost:3000")));
            services.AddSignalR();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBus eventBus, IEnumerable<IEventSubscriber> subscribers)
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
                endpoints.MapHub<AggregationsHub>("/hub/aggregations");
            });

            foreach (var subscriber in subscribers)
            {
                eventBus.Subscribe(subscriber);
            }
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
