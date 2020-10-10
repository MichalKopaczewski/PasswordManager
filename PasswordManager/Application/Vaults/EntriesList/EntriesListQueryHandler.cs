using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;
using PasswordManager.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.EntriesList
{
    public class EntriesListQueryHandler : BaseRequestHandler, IRequestHandler<EntriesListQuery,IEnumerable<EntryItemVM>>
    {

        public EntriesListQueryHandler(PasswordManagerContext context,UserResolverService userResolverService) : base(context)
        {
            UserResolverService = userResolverService;
        }

        public UserResolverService UserResolverService { get; }

        public async Task<IEnumerable<EntryItemVM>> Handle(EntriesListQuery request, CancellationToken cancellationToken)
        {
            var entries = await (from en in PmContext.Entries
                                 where en.VaultId == request.VaultId
                                 join vault in PmContext.Vaults on en.VaultId equals vault.Id
                                 where vault.Username == UserResolverService.GetUsername()
                                 select new EntryItemVM { Email = en.Email, Portal = en.Portal, Password = en.Password, Login = en.Login, EntryId = en.Id }
                                 ).ToListAsync();
            return entries;
        }
    }
}
