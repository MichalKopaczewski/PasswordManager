using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.UserDetails
{
    public class UserDetailsVM 
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public IEnumerable<UserRoleItemVM> UserRoles { get; set; }
    }
}
