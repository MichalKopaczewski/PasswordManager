using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Domain.Entities
{
    public class Vault
    {
        public string Username { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string MasterPassword { get; set; }
        public string MasterSalt { get;  set; }
    }
}
