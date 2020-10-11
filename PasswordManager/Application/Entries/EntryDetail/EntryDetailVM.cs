using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.EntryDetail
{
    public class EntryDetailVM
    {
        public long Id { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public string Portal { get;  set; }
        public string Login { get;  set; }
        public long VaultId { get;  set; }
    }
}
