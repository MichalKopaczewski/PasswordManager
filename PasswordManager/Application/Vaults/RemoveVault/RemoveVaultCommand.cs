using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.RemoveVault
{
    public class RemoveVaultCommand : IRequest
    {
        public long VaultId { get; set; }
        public string MasterPassword { get; set; }
    }
}
