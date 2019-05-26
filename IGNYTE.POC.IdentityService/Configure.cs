using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dheeraj.POC.IdentityService
{
    public class APPConfigure
    {
    public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "customAPI.read"
                    },
                    ClientSecrets =
                    {
                    new Secret("secret".Sha256())
                    },

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:44356/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:44356/signout-callback-oidc" },
                    RequireConsent = false,


                }
            };
        }
   

    public static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };
        }

    public static IEnumerable<ApiResource> GetAPIResources()
    {
        return new List<ApiResource> {
        new ApiResource {
            Name = "customAPI",
            DisplayName = "Custom API",
            Description = "Custom API Access",
            UserClaims = new List<string> {"role"},
            ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
            Scopes = new List<Scope> {
                new Scope("customAPI.read"),
                new Scope("customAPI.write")
            }
        }
    };
    }

     public static List<TestUser> GetUsers()
    {
        return new List<TestUser> {
        new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "password",

                Claims = new []
                {
                    new Claim("name", "Alice"),
                    new Claim("website", "https://alice.com")
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "password",

                Claims = new []
                {
                    new Claim("name", "Bob"),
                    new Claim("website", "https://bob.com")
                }
            }
        };
        
    }
    }
}

