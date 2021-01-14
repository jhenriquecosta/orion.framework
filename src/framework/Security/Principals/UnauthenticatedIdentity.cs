using System.Security.Claims;

namespace Orion.Framework.Security.Core.Principals {
    /// <summary>
    /// 
    /// </summary>
    public class UnauthenticatedIdentity : ClaimsIdentity {
        /// <summary>
        /// 
        /// </summary>
        public override bool IsAuthenticated => false;

        /// <summary>
        /// 
        /// </summary>
        public static readonly UnauthenticatedIdentity Instance = new UnauthenticatedIdentity();
    }
}
