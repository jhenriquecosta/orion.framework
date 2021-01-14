using System;
using Microsoft.Extensions.DependencyInjection;
using System.Xml;
using System.Reflection;

namespace Orion.Framework 
{
    public static partial class Extensions    
    {

        public static T GetService<T>(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            return services.BuildServiceProvider().GetService<T>();
        }


    }
}
