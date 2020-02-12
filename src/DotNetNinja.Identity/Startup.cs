using ChaosMonkey.Guards;
using DotNetNinja.AutoBoundConfiguration;
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
            services
                .AddAutoBoundConfigurations(Configuration)
                .FromAssembly(typeof(Program).Assembly).Services
                .AddControllersWithViews().Services
                .AddIdentityServer()
                .AddInMemoryApiResources(InMemory.ApiResources)
                .AddInMemoryClients(InMemory.Clients)
                .AddInMemoryIdentityResources(InMemory.IdentityResources)
                .AddTestUsers(InMemory.TestUsers)
                .AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                });
        }
    }
}
