﻿using Gestor.Settings.Domain.ValueObjects;
using Orion.Framework.App.Services.Domain.ValueObjects;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Orion.Framework.App.Services.Services
{
    public class IdentityServerConfig
    {
        public const string ApiName = "gestor_api";
        public const string ApiFriendlyName = "Gestor API";
        public const string AppClientID = "gestorsettings_spa";
        public const string SwaggerClientID = "swaggerui";

        // Identity resources (used by UserInfo endpoint).
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Email(),
                new IdentityResource(ScopeConstants.Roles, new List<string> { JwtClaimTypes.Role })
            };
        }

        // Api resources.
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ApiName) {
                    UserClaims = {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Role,
                        ClaimConstants.Permission,
                        Policies.IsUser,
                        Policies.IsAdmin
                    }
                }
            };
        }

        // Clients want to access resources.
        public static IEnumerable<IdentityServer4.Models.Client> GetClients()
        {
            // Clients credentials.
            return new List<IdentityServer4.Models.Client>
            {
                // http://docs.identityserver.io/en/release/reference/client.html.
                new IdentityServer4.Models.Client
                {
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Resource Owner Password Credential grant.
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        ScopeConstants.Roles,
                        ApiName
                    },
                    AllowRememberConsent = true,
                    AllowOfflineAccess = true, // For refresh token.
                    ClientId = IdentityServerConfig.AppClientID,
                    ClientName = IdentityServerConfig.ApiName,
                    //ClientUri = "https://localhost:5003",
                    ClientSecrets = new List<Secret> { new Secret { Value = "OrionSistemas".Sha512() }},
                    Enabled = true,
                    //PostLogoutRedirectUris = new List<string> {"http://localhost:5436"},
                    RequireClientSecret = true, // This client does not need a secret to request tokens from the token endpoint.
                    //RedirectUris = new List<string> {"http://localhost:5436/account/oAuth2"},
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    //SlidingRefreshTokenLifetime = 900,
                },

                new IdentityServer4.Models.Client
                {
                    ClientId = SwaggerClientID,
                    ClientName = "Swagger UI",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,

                    AllowedScopes = {
                        ApiName
                    }
                }
            };
        }
    }
}
