using System;
using Microsoft.AspNetCore.Builder;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.Domains.Enums;

namespace Orion.Framework.Bootstraping
{
    public partial interface IBootstrapTask
    {
        IBootstrapTask RegisterServices();
        IBootstrapTask RegisterApplication();
        IBootstrapTask RegisterOrionFramework();
        IBootstrapTask RegisterIdentityServer();
        IBootstrapTask RegisterDatabase<TProvider,TAssemblies>() where TAssemblies: IAssemblyLocator where TProvider : IPersistenceProvider;
        IServiceProvider BuildProvider();
        IBootstrapTask CreateOrUpdateDatabase(DatabaseAction action = DatabaseAction.Update);
        IBootstrapTask SeedDatabase();
        IBootstrapTask Configure();
        IBootstrapTask ApplicationBuilder(IApplicationBuilder application);
        IBootstrapTask UseIdentityServer();
        IBootstrapTask UseMiddleware();
  
    }
}
