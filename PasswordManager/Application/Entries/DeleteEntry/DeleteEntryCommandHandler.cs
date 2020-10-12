using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.DeleteEntry
{
    public class DeleteEntryCommandHandler : BaseRequestHandler, IRequestHandler<DeleteEntryCommand>
    {
        public DeleteEntryCommandHandler(PasswordManagerContext context,IVaultService vaultService) : base(context)
        {
            VaultService = vaultService;
        }

        public IVaultService VaultService { get; }

        public async Task<Unit> Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
        {

            var entry = await PmContext.Entries.FirstOrDefaultAsync(x => x.Id == request.EntryId);

            if (entry == null)
            {
                throw new Exception();
            }
            if (!VaultService.ValidateVaultPassword(entry.VaultId, request.MasterPassword))
            {
                throw new Exception("Podano nie poprawne hasło");
            }
            PmContext.Remove(entry);
            PmContext.SaveChanges();
            return Unit.Value;


        }
    }
}
