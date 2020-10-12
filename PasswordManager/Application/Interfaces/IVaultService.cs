using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Interfaces
{
    public interface IVaultService
    {
        public bool ValidateVaultPassword(long vaultId, string password);
    }
}
