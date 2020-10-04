using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Identity
{
    public class User
    {
        public object UserName { get; internal set; }
        public IEnumerable<string> Roles { get; internal set; }
    }
}
