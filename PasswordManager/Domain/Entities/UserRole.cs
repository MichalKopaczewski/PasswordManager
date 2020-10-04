using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Domain.Entities
{
    public class UserRole 
    {
        public string Username { get; set; }
        public string RoleName { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
        public string Id { get;  set; }
    }
}
