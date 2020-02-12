using DotNetNinja.AutoBoundConfiguration;
using DotNetNinja.Identity.TestApi.Configuration.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetNinja.Identity.TestApi
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
                .FromAssembly(typeof(Program).Assembly).Provider;

            services
                .AddControllers().Services
                .AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    var auth = settings.Get<AuthSettings>();
                    options.Audience = auth.Audience;
                    options.RequireHttpsMetadata = auth.RequireHttpsMetadata;
                    options.Authority = auth.Authority;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
