using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Orion.Framework.Locks.Default {
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddLock( this IServiceCollection services ) {
            services.TryAddScoped<ILock, DefaultLock>();
        }
    }
}
