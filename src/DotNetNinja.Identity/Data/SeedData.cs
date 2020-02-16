using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace DotNetNinja.Identity.Data
{
    public static class SeedData
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
                Password = "Password123",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, ".Net"),
                    new Claim(JwtClaimTypes.FamilyName, "Ninja"),
                    new Claim(JwtClaimTypes.Name, ".Net Ninja"),
                    new Claim(JwtClaimTypes.Email, "dotnetninja@example.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': '1010 Circuit Path', 'locality': 'Silicon Valley', 'postal_code': 55555, 'country': 'USA' }", 
                            IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.WebSite, "https://dotnetninja.net"),
                    new Claim(JwtClaimTypes.Role, "User")
                }
            },
            new TestUser{
                SubjectId = "4a7a89a3-ef1b-406d-845a-23da4418d60c", 
                Username = "AliceSmith", 
                Password = "Password123",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", 
                            IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.Role, "User")
                }
            },
            new TestUser{
                SubjectId = "d49020a0-4bd5-46fd-920c-2afc8518eaba", 
                Username = "BobSmith", 
                Password = "Password123",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", 
                            IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "somewhere"),
                    new Claim(JwtClaimTypes.Role, "User")
                }
            }
        };
}
}