using System;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.Domains;
using Orion.Framework.DataLayer.SessionContext;
using System.Linq;
using Orion.Framework.DataLayer.NH;
using Orion.Framework.Validations;
using Orion.Framework.DataLayer.NH.Events.Interceptors;

namespace Orion.Framework
{
    public class NhBuildConfiguration
    {
        private static IServiceCollection _services;
        private static SessionFactoryProvider configProvider;
        public NhBuildConfiguration(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            _services = services;
        }
        public NhBuildConfiguration WithAuditInterceptor( )
        {
            if (_services == null)
            {
                throw new ArgumentNullException(nameof(_services));
            }
            _services.AddTransient<AuditInterceptors>();
            return this;
        }
        public NhBuildConfiguration AddContext<TSessionContext>() where TSessionContext : ISessionContext
        {
            if (_services == null)
            {
                throw new ArgumentNullException(nameof(_services));
            }


            var typeContext = typeof(TSessionContext);
            var instanceContext = (ISessionContext)TypeHelper.CreateInstance(typeContext);

            _services.AddScoped(typeof(ISessionContext), typeof(TSessionContext));
            _services.AddScoped(typeContext, f => instanceContext);


            var sessionContexts = (ISessionContext)_services.BuildServiceProvider().GetService(typeContext);
            var ctx = sessionContexts;
            var nhConfig = new DbBuildFluentConfiguration(ctx);
            var cfg = nhConfig.GetConfiguration();

            Ensure.NotNull(cfg, nameof(cfg));

            var entities = cfg.ClassMappings.ToList().Select(c => new EntityTypeInfo { EntitySessionContext = typeContext, EntityType = c.ProxyInterface }).ToList();
            AppHelper.AddCache(ctx.Name, entities);
            _services.AddSessionFactoryProvider(cfg, ctx.Name);
            return this;
        }
       
    }
    public static partial class Extensions
    {

        private static SessionFactoryProvider configProvider;

        private static IServiceCollection _services;

        //init configuration nhibernate;
        public static NhBuildConfiguration AddNHibernate(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            _services = services;

            return new NhBuildConfiguration(_services);
        }
        public static IServiceCollection AddSessionFactoryProvider(this IServiceCollection services, Configuration cfg, string key)
        {
            bool boolFirstService = false;
            if (configProvider == null) boolFirstService = true;

            AddConfigurationProvider(services);
            configProvider.SetConfiguration(cfg, key);
            if (boolFirstService)
            {
                services.AddSingleton(provider =>
                {
                    var cfgProvider = provider.GetService<ISessionFactoryProvider>();
                    return cfgProvider.GetConfiguration(key);
                });
                AddNHibernate(services, cfg);

            }
            return services;
        }
        private static void AddConfigurationProvider(this IServiceCollection services)
        {

            if (configProvider == null)
            {
                configProvider = new SessionFactoryProvider();
                services.AddSingleton<ISessionFactoryProvider>(f => configProvider);
            }
        }


        public static IServiceCollection AddNHibernate(this IServiceCollection services, Configuration cfg)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (cfg == null)
            {
                throw new ArgumentNullException(nameof(cfg));
            }




            services.AddSingleton(provider =>
            {
                var config = provider.GetService<Configuration>();
                return config.BuildSessionFactory();
            });

            services.AddScoped(provider =>
            {
                var factory = provider.GetService<ISessionFactory>();
                var session = factory.OpenSession();

                return session;
            });
            services.AddScoped(provider =>
            {
                var factory = provider.GetService<ISessionFactory>();
                var session = factory.OpenStatelessSession();

                return session;
            });
            return services;
        } 
        

        public static void AddNHibernate(this IServiceCollection services, Configuration cfg, string key)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (cfg == null)
            {
                throw new ArgumentNullException(nameof(cfg));
            }
            AddConfigurationProvider(services);
            configProvider.SetConfiguration(cfg, key);
        }
       

        
        public static IServiceCollection UseNHibernate<TSessionContext>(this IServiceCollection services, Func<IDbBuildConfiguration, IDbBuildConfiguration> nhconfig)
            where TSessionContext : ISessionContext
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }


            var typeContext = typeof(TSessionContext);
            var instanceContext = (ISessionContext)TypeHelper.CreateInstance(typeContext);

            services.AddScoped(typeof(ISessionContext), typeof(TSessionContext));
            services.AddScoped(typeContext, f => instanceContext);
          

            var sessionContexts = (ISessionContext)services.BuildServiceProvider().GetService(typeContext);

            var ctx = sessionContexts;
            var cfg = nhconfig(null).WithContext(ctx).GetConfiguration();
            Ensure.NotNull(cfg, nameof(cfg));

            var entities = cfg.ClassMappings.ToList().Select(c => new EntityTypeInfo { EntitySessionContext = typeContext, EntityType = c.ProxyInterface }).ToList();
            AppHelper.AddCache(ctx.Name, entities);

            AddSessionFactoryProvider(services, cfg, ctx.Name);
           

            return services;
        }
   
     

    }
}
