using System;
using System.Linq;
using ChaosMonkey.Guards;
using DotNetNinja.Identity.Configuration.Settings;
using DotNetNinja.Identity.Constants;
using DotNetNinja.Identity.Domain;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Identity.Data.Migrations
{
    public class SqlDbMigrator : IDbMigrator
    {
        public SqlDbMigrator(MigrationSettings settings,
            PersistedGrantDbContext persistedGrantDb,
            ConfigurationDbContext configurationDb, 
            UserDbContext userDb,
            IPasswordHasher hasher)
        {
            PersistedGrantDb = Guard.IsNotNull(persistedGrantDb, nameof(persistedGrantDb));
            ConfigurationDb = Guard.IsNotNull(configurationDb, nameof(configurationDb));
            UserDb = Guard.IsNotNull(userDb, nameof(userDb));
            Settings = Guard.IsNotNull(settings, nameof(settings));
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
        }

        protected PersistedGrantDbContext PersistedGrantDb { get; }
        protected ConfigurationDbContext ConfigurationDb { get; }
        protected UserDbContext UserDb { get; }
        protected MigrationSettings Settings { get; }
        protected IPasswordHasher Hasher { get; }

        /// <summary>
        /// Migrates all used contexts if enabled and add seed data if enabled and tables are empty.
        /// Seeding only happens in migrations are enabled.
        /// </summary>
        public void Migrate()
        {
            if (Settings.IsMigrationsEnabled(PersistedGrantDb))
            {
                Migrate(PersistedGrantDb);
            }

            if (Settings.IsMigrationsEnabled(ConfigurationDb))
            {
                Migrate(ConfigurationDb);
                if (Settings.IsDataSeedingEnabled(SeedEntityType.ApiResources))
                {
                    if (!ConfigurationDb.ApiResources.Any())
                    {
                        ConfigurationDb.ApiResources
                            .AddRange(SeedData.ApiResources.Select(entity=>entity.ToEntity()));
                    }
                }
                if (Settings.IsDataSeedingEnabled(SeedEntityType.IdentityResources))
                {
                    if (!ConfigurationDb.IdentityResources.Any())
                    {
                        ConfigurationDb.IdentityResources
                            .AddRange(SeedData.IdentityResources.Select(entity => entity.ToEntity()));
                    }
                }
                if (Settings.IsDataSeedingEnabled(SeedEntityType.Clients))
                {
                    if (!ConfigurationDb.Clients.Any())
                    {
                        ConfigurationDb.Clients
                            .AddRange(SeedData.Clients.Select(entity => entity.ToEntity()));
                    }
                }
                ConfigurationDb.SaveChanges();
            }

            if (Settings.IsMigrationsEnabled(UserDb))
            {
                Migrate(UserDb);
                if (Settings.IsDataSeedingEnabled(SeedEntityType.Users))
                {
                    if (!UserDb.UserAccounts.Any())
                    {
                        var accounts = SeedData.TestUsers.Select(user => new UserAccount
                        {
                            Subject = Guid.Parse(user.SubjectId),
                            UserName = user.Username,
                            DateCreated = DateTimeOffset.Now,
                            DateModified = DateTimeOffset.Now,
                            PasswordHash = Hasher.HashPassword(user.Password)
                        });
                        UserDb.UserAccounts.AddRange(accounts);
                        UserDb.SaveChanges();
                    }
                }
            }
        }

        private void Migrate(DbContext context)
        {
            //context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}