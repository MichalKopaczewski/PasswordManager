using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PasswordManager.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Persistance.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public  void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasKey(item => item.Name);
            builder.HasIndex(item => item.Name).IsUnique(true);
            builder.Property(item => item.Name).IsRequired(true);

        }
    }
}
