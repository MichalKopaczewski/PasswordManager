using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.UpdateEntry
{
    public class UpdateEntryCommandHandler : BaseRequestHandler, IRequestHandler<UpdateEntryCommand>
    {
        public UpdateEntryCommandHandler(PasswordManagerContext context,IVaultService vaultService) : base(context)
        {
            VaultService = vaultService;
        }

        public IVaultService VaultService { get; }

        public async Task<Unit> Handle(UpdateEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = await PmContext.Entries.FirstOrDefaultAsync(item => item.Id == request.Id);
            if (entry == null)
            {
                throw new Exception();
            }

            if (!VaultService.ValidateVaultPassword(entry.VaultId, request.MasterPassword))
            {
                throw new Exception("Podano nie poprawne hasło");
            }
            entry.Email = request.Email;
            entry.Login = request.Login;
            entry.Password = request.Password;
            entry.Portal = request.Portal;

            PmContext.Update(entry);
            PmContext.SaveChanges();
            return Unit.Value;
        }
    }
}
