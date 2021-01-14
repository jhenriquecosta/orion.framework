using Microsoft.Extensions.DependencyInjection;

namespace Orion.Framework.Dependency {
 
    public interface IDependencyRegistrar {
  
        void Register( IServiceCollection services );
    }
}
