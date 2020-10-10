using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PasswordManager.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(item => item.Username);
            builder.Property(item => item.Password).IsRequired(true);
            builder.HasIndex(item => item.Username).IsUnique(true);
            builder.Property(item => item.Username).IsRequired(true);

        }
    }
}
