using MediatR;

using PasswordManager.Application.Interfaces;
using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Auth;
using PasswordManager.Infrastructure.Persistance;
using PasswordManager.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.CreateVault
{
    public class CreateVaultCommandHandler : BaseRequestHandler, IRequestHandler<CreateVaultCommand>
    {
        public CreateVaultCommandHandler(PasswordManagerContext context,UserResolverService userResolverService,ICryptoService cryptoService) : base(context)
        {
            UserResolverService = userResolverService;
            CryptoService = cryptoService;
        }

        public UserResolverService UserResolverService { get; }
        public ICryptoService CryptoService { get; }

        public async Task<Unit> Handle(CreateVaultCommand request, CancellationToken cancellationToken)
        {
            var vault = new Vault()
            {
                MasterSalt = Encryptor.GenerateSalt(),
                MasterPassword = "",
                Name = request.Name,
                Username = UserResolverService.GetUsername()
            };
            vault.MasterPassword = CryptoService.HashString(request.MasterPassword, vault.MasterSalt);
            PmContext.Add(vault);
            await PmContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
