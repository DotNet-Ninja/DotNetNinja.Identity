using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.Identity.Configuration.Settings
{
    [AutoBind("Secrets:SigningCertificate")]
    public class CertificateSettings
    {
        public string Path { get; set; }
        public string Password { get; set; }
    }
}