using DotNetNinja.AutoBoundConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DotNetNinja.Identity.TestApi.Configuration.Settings
{
    [AutoBind("Auth")]
    public class AuthSettings
    {
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string Audience { get; set; }
    }
}