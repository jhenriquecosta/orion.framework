using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Dependency;

namespace Orion.Framework.DataLayer.UnitOfWorks 
{
    public static class RegisterExtensions
    {
        public static ContainerBuilder RegisterUnitOfWorkBase(this ContainerBuilder builder)
        {
            builder.RegisterType<AsyncLocalCurrentUnitOfWorkProvider>().As<ICurrentUnitOfWorkProvider>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWorkDefaultOptions>().As<IUnitOfWorkDefaultOptions>().InstancePerLifetimeScope();
            builder.AddSingleton<UnitOfWorkDefaultOptions>(new UnitOfWorkDefaultOptions());
            builder.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
