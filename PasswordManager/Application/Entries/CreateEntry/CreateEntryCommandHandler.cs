using MediatR;

using Microsoft.EntityFrameworkCore;

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

namespace PasswordManager.Application.Entries.CreateEntry
{
    public class CreateEntryCommandHandler : BaseRequestHandler, IRequestHandler<CreateEntryCommand>
    {
        public CreateEntryCommandHandler(PasswordManagerContext context,UserResolverService userResolverService,IVaultService vaultService) : base(context)
        {
            UserResolverService = userResolverService;
            VaultService = vaultService;
        }

        public UserResolverService UserResolverService { get; }
        public IVaultService VaultService { get; }

        public async Task<Unit> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {

            if (!VaultService.ValidateVaultPassword(request.VaultId, request.MasterPassword))
            {
                throw new Exception("Podano nie poprawne hasło");
            }

            var vault = await (from v in PmContext.Vaults
                               where v.Id == request.VaultId
                               select new { v.Id }
                               ).FirstOrDefaultAsync();
            if (vault == null)
            {
                throw new Exception();
            }

            Entry entry = new Entry();
            if (String.IsNullOrEmpty(request.Login) || String.IsNullOrEmpty(request.Password) || String.IsNullOrEmpty(request.Portal) )
            {
                throw new Exception("Wrong data");
            }
            entry.Login = request.Login;
            entry.Password = request.Password;
            entry.Portal = request.Portal;
            entry.Email = request.Email;
            entry.VaultId = vault.Id;

            PmContext.Add(entry);
            await PmContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
