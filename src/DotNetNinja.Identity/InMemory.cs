using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace DotNetNinja.Identity
{
    public static class InMemory
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("test-api", "Test API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "test-client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("super-secret".Sha256())
                    },
                    AllowedScopes = {"test-api"}
                },
                new Client
                {
                    ClientId = "test-mvc",
                    ClientSecrets = {new Secret("super-secret".Sha256())},

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RequireConsent = false,
                    RequirePkce = false,
                    
                    // where to redirect to after login
                    RedirectUris = {"https://localhost:5021/signin-oidc"},

                    // where to redirect to after logout
                    PostLogoutRedirectUris = {"https://localhost:5021/signout-callback-oidc"},

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "test-api"
                    }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static List<TestUser> TestUsers => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "fe285f5c-e36e-48b2-9235-02191967c308",
                Username = "DotNetNinja",
                Password = "secret",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, ".Net"),
                    new Claim(JwtClaimTypes.FamilyName, "Ninja"),
                    new Claim(JwtClaimTypes.Name, ".Net Ninja"),
                    new Claim(JwtClaimTypes.Email, "dotnetninja@example.com"),
                    new Claim(JwtClaimTypes.Address, "1010 Circuit Path, Silicon Valley, MN 55555"),
                    new Claim(JwtClaimTypes.Role, "User")
                }
            }
        };
}
}