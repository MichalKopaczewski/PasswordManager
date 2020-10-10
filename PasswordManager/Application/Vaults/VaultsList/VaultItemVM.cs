using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.VaultsList
{
    public class VaultItemVM
    {
        public long VaultId { get; internal set; }
        public string Name { get; internal set; }
    }
}
