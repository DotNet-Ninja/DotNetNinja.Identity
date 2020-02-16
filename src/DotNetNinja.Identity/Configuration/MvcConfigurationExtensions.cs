using DotNetNinja.Identity.Data;
using DotNetNinja.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNinja.Identity.Configuration
{
    public static class MvcConfigurationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>()
                .AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserValidator, UserValidator>();
        }

        public static IApplicationBuilder UseDefaultEndpoints(this IApplicationBuilder app)
        {
            return app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}