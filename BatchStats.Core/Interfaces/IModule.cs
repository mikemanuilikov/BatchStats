﻿using Microsoft.Extensions.DependencyInjection;

namespace BatchStats.Core.Interfaces
{
    public interface IModule
    {
        void ConfigureServices(IServiceCollection services);
    }
}
