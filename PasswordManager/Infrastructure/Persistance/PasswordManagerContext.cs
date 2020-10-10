using Microsoft.EntityFrameworkCore;

using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Persistance.Configurations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Persistance
{
    public class PasswordManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vault> Vaults { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public PasswordManagerContext()
        {
        }

        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new EntryConfiguration());
            builder.ApplyConfiguration(new VaultConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
