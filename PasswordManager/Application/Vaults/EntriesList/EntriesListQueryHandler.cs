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

namespace PasswordManager.Application.Entries.EntriesList
{
    public class EntriesListQueryHandler : BaseRequestHandler, IRequestHandler<EntriesListQuery,IEnumerable<EntryItemVM>>
    {

        public EntriesListQueryHandler(PasswordManagerContext context,UserResolverService userResolverService,ICryptoService cryptoService,IVaultService vaultService) : base(context)
        {
            UserResolverService = userResolverService;
            CryptoService = cryptoService;
            VaultService = vaultService;
        }

        public UserResolverService UserResolverService { get; }
        public ICryptoService CryptoService { get; }
        public IVaultService VaultService { get; }

        public async Task<IEnumerable<EntryItemVM>> Handle(EntriesListQuery request, CancellationToken cancellationToken)
        {

            if (!VaultService.ValidateVaultPassword(request.VaultId,request.MasterPassword))
            {
                throw new Exception("Podano nie poprawne hasło");
            }

            var entries = await (from en in PmContext.Entries
                                 where en.VaultId == request.VaultId
                                 select new EntryItemVM { Email = en.Email, Portal = en.Portal, Password = en.Password, Login = en.Login, EntryId = en.Id }
                                 ).ToListAsync();
            return entries;
        }
    }
}
