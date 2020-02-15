using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.Identity.Configuration.Settings
{
    [AutoBind("Secrets:ConnectionStrings")]
    public class ConnectionSettings
    {
        public string PersistedGrantDb { get; set; }
        public string ConfigurationDb { get; set; }
        public string UserDb { get; set; }
    }
}