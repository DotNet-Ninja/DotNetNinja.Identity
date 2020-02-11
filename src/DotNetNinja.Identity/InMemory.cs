using System.Collections.Generic;
using IdentityServer4.Models;

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
                    AllowedScopes = { "test-api" }
                }
            };
    }
}