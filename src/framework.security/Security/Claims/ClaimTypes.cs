using IdentityModel;

namespace Orion.Framework.Security.Claims {
    /// <summary>
    /// 
    /// </summary>
    public static class ClaimTypes {
        /// <summary>
        /// 
        /// </summary>
        public static string Email { get; set; } = JwtClaimTypes.Email;

        /// <summary>
        /// 
        /// </summary>
        public static string Mobile { get; set; } = JwtClaimTypes.PhoneNumber;

        /// <summary>
        /// 
        /// </summary>
        public static string FullName { get; set; } = JwtClaimTypes.FamilyName;

        /// <summary>
        /// 
        /// </summary>
        public static string ApplicationId { get; set; } = "application_id";

        /// <summary>
        /// 
        /// </summary>
        public static string ApplicationCode { get; set; } = "application_code";

        /// <summary>
        /// 
        /// </summary>
        public static string ApplicationName { get; set; } = "application_name";

        /// <summary>
        /// 
        /// </summary>
        public static string TenantId { get; set; } = "tenant_id";

        /// <summary>
        /// 
        /// </summary>
        public static string TenantCode { get; set; } = "tenant_code";

        /// <summary>
        /// 
        /// </summary>
        public static string TenantName { get; set; } = "tenant_name";

        /// <summary>
        /// 
        /// </summary>
        public static string RoleIds { get; set; } = JwtClaimTypes.Role;

        /// <summary>
        /// 
        /// </summary>
        public static string RoleName { get; set; } = "role_name";
    }
}