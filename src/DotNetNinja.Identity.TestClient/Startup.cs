using System.IdentityModel.Tokens.Jwt;
using DotNetNinja.AutoBoundConfiguration;
using DotNetNinja.Identity.TestClient.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetNinja.Identity.TestClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = services
                .AddAutoBoundConfigurations(Configuration)
                .FromAssembly(typeof(Program).Assembly)
                .Provider;

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    var configuration = settings.Get<OidcConfiguration>();
                    options.Authority = configuration.Authority;
                    options.RequireHttpsMetadata = false;
                    options.ClientId = configuration.ClientId;
                    options.ClientSecret = configuration.ClientSecret;
                    options.ResponseType = configuration.ResponseType;

                    options.SaveTokens = true;
                    
                    configuration.Scopes.ForEach(scope =>
                    {
                        options.Scope.Add(scope);
                    });

                }).Services
                .AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                
            }
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}
