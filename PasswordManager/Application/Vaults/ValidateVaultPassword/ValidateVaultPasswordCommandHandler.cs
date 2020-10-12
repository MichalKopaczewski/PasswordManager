using MediatR;

using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Persistance;
using PasswordManager.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.ValidateVaultPassword
{
    public class ValidateVaultPasswordCommandHandler : BaseRequestHandler, IRequestHandler<ValidateVaultPasswordCommand, bool>
    {
        public ValidateVaultPasswordCommandHandler(PasswordManagerContext context,IVaultService vaultService) : base(context)
        {
            VaultService = vaultService;
        }

        public IVaultService VaultService { get; }

        public async Task<bool> Handle(ValidateVaultPasswordCommand request, CancellationToken cancellationToken)
        {
            return VaultService.ValidateVaultPassword(request.VaultId, request.MasterPassword);
        }
    }
}
