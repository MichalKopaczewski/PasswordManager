using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.VaultDetail
{
    public class VaultDetailVM 
    {
        public long VaultId { get; set; }
        public string Name { get; set; }
        public long EntriesCount { get; set; }
    }
}
