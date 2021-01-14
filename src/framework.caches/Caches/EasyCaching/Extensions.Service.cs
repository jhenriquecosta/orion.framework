using System;
using EasyCaching.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orion.Framework.Caches.EasyCaching;

namespace Orion.Framework.Caches
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configAction"></param>
        public static void AddCache(this IServiceCollection services, Action<EasyCachingOptions> configAction ) {
            services.TryAddScoped<ICache, CacheManager>();
            services.AddEasyCaching( configAction );
        }
    }
}