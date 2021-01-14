using System;
using Exceptionless;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Abstractions;
using Orion.Framework.Logs.Core;
using Orion.Framework.Logs.Formats;

namespace Orion.Framework {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddNLog( this IServiceCollection services ) {
            services.TryAddScoped<ILogProviderFactory, Logs.NLog.LogProviderFactory>();
            services.TryAddSingleton<ILogFormat, ContentFormat>();
            services.TryAddScoped<ILogContext, LogContext>();
            services.TryAddScoped<ILog, Log>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configAction"></param>
        public static void AddExceptionless( this IServiceCollection services, Action<ExceptionlessConfiguration> configAction ) {
            services.TryAddScoped<ILogProviderFactory, Orion.Framework.Logs.Exceptionless.LogProviderFactory>();
            services.TryAddSingleton( typeof( ILogFormat ), t => NullLogFormat.Instance );
            services.TryAddScoped<ILogContext, Orion.Framework.Logs.Exceptionless.LogContext>();
            services.TryAddScoped<ILog, Log>();
            configAction?.Invoke( ExceptionlessClient.Default.Configuration );
        }
    }
}
