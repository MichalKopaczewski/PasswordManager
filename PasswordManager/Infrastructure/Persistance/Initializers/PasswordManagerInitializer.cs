using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Auth;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Persistance.Initializers
{
    public class PasswordManagerInitializer
    {
        PasswordManagerContext PmContext;
        public PasswordManagerInitializer(PasswordManagerContext context)
        {
            PmContext = context;
        }

        public void SeedEverything()
        {
            this.SeedUsers();
            this.SeedRoles();
            this.SeedUserRoles();
        }
        private void SeedUserRoles()
        {
            if (PmContext.UserRoles.Count() >1)
            {
                return;
            }
            var userRoles = new[]
            {
                new UserRole() { RoleName = "Admin",Username = "admin"},
                new UserRole() { RoleName = "SystemUser",Username = "admin"},
                new UserRole() { RoleName = "Admin",Username = "SuperAdmin"},
                new UserRole() { RoleName = "Admin",Username = "michalk"},
                new UserRole() { RoleName = "SystemUser",Username = "michalk"}
            };
            PmContext.UserRoles.AddRange(userRoles);
            PmContext.SaveChanges();
        }

        private void SeedRoles()
        {
            if (PmContext.Roles.Count()>1)
            {
                return;
            }
            var roles = new[]
            {
                new Role() {Name = "Admin",Description="Admin"},
                new Role() {Name = "SystemUser",Description = "SystemUser"}
            };
            PmContext.Roles.AddRange(roles);
            PmContext.SaveChanges();
        }

        private void SeedUsers()
        {
            if (PmContext.Users.Count() > 1)
            {
                return;
            }
            var users = new[]
            {
                new User() {Username = "admin",Password = Encryptor.Encode("admin")},
                new User() {Username = "SuperAdmin",Password = Encryptor.Encode("SuperAdmin")},
                new User() {Username = "michalk",Password = Encryptor.Encode("1234qwer-")},
            };
            PmContext.Users.AddRange(users);
            PmContext.SaveChanges();
        }
    }
}
