using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.DataLayer.Dao;
using Orion.Framework.DataLayer.Domains.Services;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;
using Orion.Framework.Domains.Entities;
using System;
using System.Collections.Generic;

namespace Orion.Framework.DataLayer.Dependency
{


    public class IocConfigWebDataLayer : ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
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

                        //Type genericRepository = (typeof(IDataLayerDomainService<>)).MakeGenericType(entity.EntityType);
                        Type implRepository = (typeof(DbDao<>)).MakeGenericType(entity.EntityType);                        
                        builder.RegisterType(implRepository).AsSelf().AsImplementedInterfaces().PropertiesAutowired();

                    }
                }
            }

        }
    }
}
