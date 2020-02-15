using DotNetNinja.Identity.Configuration.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNinja.Identity.Configuration
{
    public static class DataStoreConfigurationExtensions
    {
        private static IIdentityServerBuilder  AddSqlOperationalStore(this IIdentityServerBuilder  services, ConnectionSettings settings)
        {
            return services.AddOperationalStore(options =>
            {
                options.ConfigureDbContext =
                    builder => builder.UseSqlServer(settings.PersistedGrantDb,
                        ef => ef.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
                options.EnableTokenCleanup = true;
            });
        }

        private static IIdentityServerBuilder  AddSqlConfigurationStore(this IIdentityServerBuilder  services, ConnectionSettings settings)
        {
            return services.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext =
                    builder => builder.UseSqlServer(settings.ConfigurationDb,
                        ef => ef.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            });
        }

        private static IIdentityServerBuilder  AddSqlUserStore(this IIdentityServerBuilder  services, ConnectionSettings settings)
        {
            return services;
        }

        public static IIdentityServerBuilder  AddSqlServerDataStores(this IIdentityServerBuilder  services, ConnectionSettings settings)
        {
            return services.AddSqlOperationalStore(settings)
                .AddSqlConfigurationStore(settings)
                .AddSqlUserStore(settings);
        }
    }
}