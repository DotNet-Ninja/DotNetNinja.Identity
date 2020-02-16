using DotNetNinja.Identity.Configuration.Settings;
using DotNetNinja.Identity.Data;
using DotNetNinja.Identity.Data.Migrations;
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
            var defaultServices = services;
            services.Services.AddDbContext<UserDbContext>(options => 
            { 
                options.UseSqlServer(settings.UserDb, builder =>
                {
                    builder.MigrationsAssembly(typeof(UserDbContext).Assembly.GetName().Name);
                });
            });
            return defaultServices;
        }

        public static IIdentityServerBuilder  AddSqlServerDataStores(this IIdentityServerBuilder  services, ConnectionSettings settings)
        {
            var defaultServices = services.Services;

            services.AddSqlOperationalStore(settings)
                .AddSqlConfigurationStore(settings)
                .AddSqlUserStore(settings);
            defaultServices.AddScoped<IDbMigrator, SqlDbMigrator>();
            return services;
        }
    }
}