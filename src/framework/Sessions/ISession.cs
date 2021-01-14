using Orion.Framework.Dependency;

namespace Orion.Framework.Sessions 
{

    public interface ISession : ISingletonDependency
    {

        bool IsAuthenticated { get; }

        string UserId { get; }
        int? OrganizationCode { get; }
    }
}