using System.Collections.Generic;
using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.Identity.TestClient.Configuration
{
    [AutoBind("Oidc")]
    public class OidcConfiguration
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ResponseType { get; set; }
        public List<string> Scopes { get; set; }
    }
}