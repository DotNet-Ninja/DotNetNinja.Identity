using Microsoft.AspNetCore.Builder;

namespace DotNetNinja.Identity.Configuration
{
    public static class MvcConfigurationExtensions
    {
        public static IApplicationBuilder UseDefaultEndpoints(this IApplicationBuilder app)
        {
            return app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}