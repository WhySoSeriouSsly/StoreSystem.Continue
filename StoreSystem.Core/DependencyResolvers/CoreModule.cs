using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Core.CrossCuttingConcerns.Caching;
using StoreSystem.Core.CrossCuttingConcerns.Caching.Microsoft;
using StoreSystem.Core.Utilities.IoC;

namespace StoreSystem.Core.DependencyResolvers
{
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
