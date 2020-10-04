using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Domain.Entities
{
    public class Role 
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<UserRole> RoleUsers { get; set; }
    }
}
