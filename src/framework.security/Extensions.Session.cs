using System;
using System.Collections.Generic;
using IdentityModel;
using Orion.Framework.Security.Claims;
using Orion.Framework.Helpers;
using Orion.Framework.Sessions;
using TypeConvert = Orion.Framework.Helpers.TypeConvert;

namespace Orion.Framework
{

    public static class SessionExtensions 
    {
       
        public static Guid GetUserId( this ISession session ) {
            return session.UserId.ToGuid();
        }

     
        public static T GetUserId<T>( this ISession session ) {
            return TypeConvert.To<T>( session.UserId );
        }

     
        public static string GetUserName( this ISession session ) {
            var result = WebHttp.Identity.GetValue( JwtClaimTypes.Name );
            return string.IsNullOrWhiteSpace( result ) ? WebHttp.Identity.GetValue( System.Security.Claims.ClaimTypes.Name ) : result;
        }

     
        public static string GetFullName( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.FullName );
        }

     
        public static string GetEmail( this ISession session ) {
            var result = WebHttp.Identity.GetValue( JwtClaimTypes.Email );
            return string.IsNullOrWhiteSpace( result ) ? WebHttp.Identity.GetValue( System.Security.Claims.ClaimTypes.Email ) : result;
        }

      
        public static string GetMobile( this ISession session ) {
            var result = WebHttp.Identity.GetValue( JwtClaimTypes.PhoneNumber );
            return string.IsNullOrWhiteSpace( result ) ? WebHttp.Identity.GetValue( System.Security.Claims.ClaimTypes.MobilePhone ) : result;
        }

        
        public static Guid GetApplicationId( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.ApplicationId ).ToGuid();
        }

        
        public static T GetApplicationId<T>( this ISession session ) {
            return TypeConvert.To<T>( WebHttp.Identity.GetValue( ClaimTypes.ApplicationId ) );
        }

       
        public static string GetApplicationCode( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.ApplicationCode );
        }

       
        public static string GetApplicationName( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.ApplicationName );
        }

      
        public static Guid GetTenantId( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.TenantId ).ToGuid();
        }

      
        public static T GetTenantId<T>( this ISession session ) {
            return TypeConvert.To<T>( WebHttp.Identity.GetValue( ClaimTypes.TenantId ) );
        }

     
        public static string GetTenantCode( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.TenantCode );
        }

        
        public static string GetTenantName( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.TenantName );
        }

      
        public static List<Guid> GetRoleIds( this ISession session ) {
            return session.GetRoleIds<Guid>();
        }

      
        public static List<T> GetRoleIds<T>( this ISession session ) {
            return TypeConvert.ToList<T>( WebHttp.Identity.GetValue( ClaimTypes.RoleIds ) );
        }

    
        public static string GetRoleName( this ISession session ) {
            return WebHttp.Identity.GetValue( ClaimTypes.RoleName );
        }
    }
}
