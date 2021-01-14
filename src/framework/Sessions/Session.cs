using Orion.Framework.Helpers;
using IdentityModel;
using Orion.Framework.Sessions;
using Orion.Framework.Infrastructurelications;
using Orion.Framework.Settings;

namespace Orion.Framework.Sessions {
    
    public class Session : ISession {
     
        public static readonly ISession Null = NullSession.Instance;

        
        public static readonly ISession Instance = new Session();

        
        public bool IsAuthenticated => WebHttp.Identity.IsAuthenticated;

        public int? OrganizationCode
        {
            get
            {
                return XTConstants.INST_DEFAULT_CODE;
            }
        }
        public string UserId 
        {
            get {
                  var result = WebHttp.Identity.GetValue( JwtClaimTypes.Subject );
                  var id = string.IsNullOrWhiteSpace( result ) ? WebHttp.Identity.GetValue( System.Security.Claims.ClaimTypes.NameIdentifier ) : result;
                  id = id.IsNullOrWhiteSpace() ? "0" : id;
                  return id;
            }
        }
    }
}