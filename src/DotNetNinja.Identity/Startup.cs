using System.Security.Cryptography.X509Certificates;
using ChaosMonkey.Guards;
using DotNetNinja.AutoBoundConfiguration;
using DotNetNinja.Identity.Configuration;
using DotNetNinja.Identity.Configuration.Settings;
using DotNetNinja.Identity.Data;
using DotNetNinja.Identity.Data.Migrations;
using DotNetNinja.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetNinja.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = Guard.IsNotNull(configuration, nameof(configuration));
        }

        protected IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = services
                .AddAutoBoundConfigurations(Configuration)
                .FromAssembly(typeof(Program).Assembly).Provider;

            var certificateSettings = settings.Get<CertificateSettings>();
            var signingCertificate = new X509Certificate2(certificateSettings.Path, certificateSettings.Password);

            services
                .AddApplicationServices()
                .AddControllersWithViews().Services
                .AddIdentityServer()
                .AddSqlServerDataStores(settings.Get<ConnectionSettings>())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                //.AddDeveloperSigningCredential() // Uncomment this line and comment out the one below if you wish to run locally without
                .AddSigningCredential(signingCertificate);  // generating a certificate.  DO NOT RUN THIS WAY IN PRODUCTION!
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbMigrator migrations)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseStaticFiles()
                .UseRouting()
                .UseIdentityServer()
                .UseAuthorization()
                .UseDefaultEndpoints();

            migrations.Migrate();
        }
    }
}
