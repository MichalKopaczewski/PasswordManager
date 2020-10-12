using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Persistance;
using PasswordManager.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.RemoveVault
{
    public class RemoveVaultCommandHandler : BaseRequestHandler, IRequestHandler<RemoveVaultCommand>
    {
        public RemoveVaultCommandHandler(PasswordManagerContext context,UserResolverService userResolverService,IVaultService vaultService) : base(context)
        {
            UserResolverService = userResolverService;
            VaultService = vaultService;
        }

        public UserResolverService UserResolverService { get; }
        public IVaultService VaultService { get; }

        public async Task<Unit> Handle(RemoveVaultCommand request, CancellationToken cancellationToken)
        {

            if (!VaultService.ValidateVaultPassword(request.VaultId, request.MasterPassword))
            {
                throw new Exception("Podano nie poprawne hasło");
            }
            var vault = await (from v in PmContext.Vaults
                         where v.Username == UserResolverService.GetUsername() && v.Id == request.VaultId
                         select v).FirstOrDefaultAsync();
            if (vault == null)
            {
                throw new Exception();
            }
            var entries = await (from en in PmContext.Entries
                           where en.VaultId == vault.Id
                           select en
                           ).ToListAsync();
            using var trans = PmContext.Database.BeginTransaction();

            PmContext.Entries.RemoveRange(entries);
            PmContext.Vaults.Remove(vault);
            PmContext.SaveChanges();
            await trans.CommitAsync();
            return Unit.Value;
        }
    }
}
