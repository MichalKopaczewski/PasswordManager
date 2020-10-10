using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.CreateVault
{
    public class CreateVaultCommand : IRequest
    {
        public string Name { get; set; }
        public string MasterPassword { get; set; }
    }
}
