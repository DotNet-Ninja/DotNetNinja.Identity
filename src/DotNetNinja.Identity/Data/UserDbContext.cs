using DotNetNinja.Identity.Data.TypeConfigurations;
using DotNetNinja.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Identity.Data
{
    public class UserDbContext: DbContext
    {
        public UserDbContext() { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserAccountEntityTypeConfigurations());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserAccount> UserAccounts { get; set; }

    }
}