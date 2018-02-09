// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerWithAspNetIdentity
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ContosoUniversity.API", "Contoso University API")
                {
                   
                    UserClaims =
                    {
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role
                    }
                }
                  
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {                                

                // OpenID Connect hybrid flow and client credentials client (MVC)             
         
                new Client
                {
                    ClientId = "mvcB",
                    ClientName = "MVCB Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secretB".Sha256())
                    },

                    RedirectUris = { "http://localhost:5007/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5007/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ContosoUniversity.API"
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}