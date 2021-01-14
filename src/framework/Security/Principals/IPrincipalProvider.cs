using System.Security.Principal;
using Orion.Framework.Dependency;

namespace Orion.Framework.Security.Principals
{
    public interface IPrincipalProvider : IScopeDependency
    {
        /// <summary>
        /// Gets the name of the get current principal.
        /// </summary>
        /// <value>
        /// The name of the get current principal.
        /// </value>
        string CurrentPrincipalName { get; }

        /// <summary>
        /// Gets the current principal.
        /// </summary>
        /// <returns>Current IPrincipal.</returns>
        IPrincipal GetCurrentPrincipal();
    }
}
