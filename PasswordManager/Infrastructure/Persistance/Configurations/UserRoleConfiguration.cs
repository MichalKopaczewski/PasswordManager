using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PasswordManager.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Persistance.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {

        public  void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a=>a.Id).ValueGeneratedOnAdd();
            builder.Property(item => item.RoleName).IsRequired(true);
            builder.Property(item => item.Username).IsRequired(true);
            builder.HasIndex(item => new { item.Username, item.RoleName}).IsUnique();
            builder.HasOne<Role>()
                .WithMany()
                .HasForeignKey(x => x.RoleName)
                .HasPrincipalKey(x => x.Name);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.Username)
                .HasPrincipalKey(x => x.Username);
        }
    }
}
