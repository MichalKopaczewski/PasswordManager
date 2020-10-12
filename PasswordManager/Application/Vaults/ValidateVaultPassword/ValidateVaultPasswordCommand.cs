using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.ValidateVaultPassword
{
    public class ValidateVaultPasswordCommand : IRequest<bool>
    {
        public string MasterPassword { get; set; }
        public long VaultId { get;  set; }
    }
}
