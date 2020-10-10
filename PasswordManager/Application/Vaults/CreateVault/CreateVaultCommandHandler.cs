using MediatR;

using PasswordManager.Domain.Entities;
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
        public CreateVaultCommandHandler(PasswordManagerContext context,UserResolverService userResolverService) : base(context)
        {
            UserResolverService = userResolverService;
        }

        public UserResolverService UserResolverService { get; }

        public async Task<Unit> Handle(CreateVaultCommand request, CancellationToken cancellationToken)
        {
            var vault = new Vault()
            {
                MasterPassword = request.MasterPassword,
                Name = request.Name,
                Username = UserResolverService.GetUsername()
            };
            PmContext.Add(vault);
            await PmContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
