using System.Collections.Generic;
using DotNetNinja.AutoBoundConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Identity.Configuration.Settings
{
    [AutoBind("Migrations")]
    public class MigrationSettings
    {
        public MigrationSettings()
        {
            AutomaticMigrationsEnabled = new Dictionary<string, bool>();
        }

        public Dictionary<string, bool> AutomaticMigrationsEnabled { get; set; }

        public Dictionary<string, bool> EntitySeedingEnabled { get; set; }

        public bool IsMigrationsEnabled<TContext>(TContext context) where TContext : DbContext
        {
            var contextName = typeof(TContext).Name;
            return AutomaticMigrationsEnabled.ContainsKey(contextName) && AutomaticMigrationsEnabled[contextName];
        }

        public bool IsDataSeedingEnabled(string entityType) =>
            EntitySeedingEnabled.ContainsKey(entityType) && EntitySeedingEnabled[entityType];

    }
}