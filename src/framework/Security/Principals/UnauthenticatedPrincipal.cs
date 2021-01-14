using System.Security.Claims;
using System.Security.Principal;

namespace Orion.Framework.Security.Core.Principals {
    /// <summary>
    /// 
    /// </summary>
    public class UnauthenticatedPrincipal : ClaimsPrincipal {
        /// <summary>
        /// 
        /// </summary>
        private UnauthenticatedPrincipal(){
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly UnauthenticatedPrincipal Instance = new UnauthenticatedPrincipal();

        /// <summary>
        /// 
        /// </summary>
        public override IIdentity Identity => UnauthenticatedIdentity.Instance;
    }
}
