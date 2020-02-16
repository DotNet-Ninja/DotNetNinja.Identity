using System;
using DotNetNinja.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetNinja.Identity.Data.TypeConfigurations
{
    public class UserAccountEntityTypeConfigurations: IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasKey(x => x.Id).IsClustered().HasName("PK_UserAccount");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Subject).IsRequired();
            builder.HasAlternateKey(x => x.Subject).HasName("AK_UserAccount_Subject");
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(64);
            builder.HasIndex(x => x.UserName).HasName("UK_UserAccount_UserName");
            builder.Property(x => x.PasswordHash).HasMaxLength(256);
            builder.Property(x => x.DateCreated).IsRequired().HasDefaultValue(DateTimeOffset.Now);
            builder.Property(x => x.DateModified).IsRequired().HasDefaultValue(DateTimeOffset.Now);
        }
    }
}