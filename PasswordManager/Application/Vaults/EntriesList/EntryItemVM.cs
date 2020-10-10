using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.EntriesList
{
    public class EntryItemVM
    {
        public long EntryId { get; internal set; }
        public string Portal { get; internal set; }
        public string Login { get; internal set; }
        public string Password { get; internal set; }
        public string Email { get; internal set; }
    }
}
