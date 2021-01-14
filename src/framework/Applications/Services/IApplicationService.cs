using Orion.Framework.Dependency;
using Orion.Framework.Domains.Services;

namespace Orion.Framework.Applications.Services
{
     
   
    /// <summary>
    ///     This interface must be implemented by all application services to identify them by convention.
    /// </summary>
    /// <seealso cref="Autofac.Extras.IocManager.ITransientDependency" />
    public interface IApplicationService : IDomainService
    {

    }
}
