using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Auth.LoginCommand
{
    public class UserVM
    {
        public string Username { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
