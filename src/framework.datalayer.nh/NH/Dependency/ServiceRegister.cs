using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.NH.Dao;
using Orion.Framework.DataLayer.NH.Repositories;
using Orion.Framework.DataLayer.NH.UnitOfWorks;
using Orion.Framework.DataLayer.Repositories.Contracts;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.DataLayer.UnitOfWorks;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;
using Orion.Framework.Domains.Entities;
using System;
using System.Collections.Generic;

namespace Orion.Framework.DataLayer.NH.Dependency
{
    public class IocConfigNHibernate : ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
        {

            LoadUnitOfWork(builder);

        }


        private void LoadUnitOfWork(ContainerBuilder builder)
        {
          
            builder.AddScoped<DbDataContext>();
            builder.RegisterUnitOfWorkBase();
            builder.RegisterUoW();
            builder.RegisterRepositories();

        }

    }
    public static class Extensions
    {
        public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder)
        {
            var sessionContexts = AppHelper.GetServices<ISessionContext>();
            foreach (var item in sessionContexts)
            {
                var name = item.Name;
                var entities = AppHelper.GetCache<IEnumerable<EntityTypeInfo>>(name);
                foreach (var entity in entities)
                {
                    Type primaryKeyType = EntityHelper.GetPrimaryKeyType(entity.EntityType);
                    if (primaryKeyType == typeof(int))
                    {

                        Type genericRepository = (typeof(IRepository<>)).MakeGenericType(entity.EntityType);
                        Type implRepository = (typeof(NHRepository<,>)).MakeGenericType(entity.EntitySessionContext, entity.EntityType);
                        builder.RegisterType(implRepository).As(genericRepository).AsSelf().AsImplementedInterfaces();

                        //Type implDao = (typeof(DbDao<>)).MakeGenericType(entity.EntityType);
                        //builder.RegisterType(implRepository).AsSelf().AsImplementedInterfaces().PropertiesAutowired();
                    }
                }
            }
            return builder;
        }
        public static ContainerBuilder RegisterUoW(this ContainerBuilder builder)
        {
            builder.RegisterType<NhUnitOfWorkFilterExecuter>().As<IUnitOfWorkFilterExecuter>().InstancePerLifetimeScope();
            builder.RegisterType<NhUnitOfWorkSessionProvider>().As<ISessionProvider>().InstancePerLifetimeScope();
            builder.RegisterType<NhUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            return builder;
        }
    }
 
}
