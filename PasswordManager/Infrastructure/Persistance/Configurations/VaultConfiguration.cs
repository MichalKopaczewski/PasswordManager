using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PasswordManager.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Persistance.Configurations
{
    public class VaultConfiguration : IEntityTypeConfiguration<Vault>
    {
        public void Configure(EntityTypeBuilder<Vault> builder)
        {

            builder.HasKey(item => item.Id);
            builder.Property(item => item.Id).ValueGeneratedOnAdd();

            builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(v => v.Username)
               .HasPrincipalKey(u => u.Username);

        }
    }
}
