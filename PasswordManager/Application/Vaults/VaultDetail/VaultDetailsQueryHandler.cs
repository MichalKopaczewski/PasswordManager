using MediatR;
using MediatR.Pipeline;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;
using PasswordManager.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.VaultDetail
{
    public class VaultDetailsQueryHandler : BaseRequestHandler, IRequestHandler<VaultDetailQuery, VaultDetailVM>
    {
        public VaultDetailsQueryHandler(PasswordManagerContext context,UserResolverService userResolverService) : base(context)
        {
            UserResolverService = userResolverService;
        }

        public UserResolverService UserResolverService { get; }

        public async Task<VaultDetailVM> Handle(VaultDetailQuery request, CancellationToken cancellationToken)
        {
            VaultDetailVM vault = new VaultDetailVM();

            var v = await (from va in PmContext.Vaults
                           where va.Id == request.VaultId && va.Username == UserResolverService.GetUsername()
                           select va
                           ).FirstAsync();
            vault.Name = v.Name;

            var entriesCount = await (from en in PmContext.Entries
                                      where request.VaultId == en.VaultId
                                      group en by en.VaultId into grouped
                                      select grouped.Count()).FirstOrDefaultAsync();
            vault.EntriesCount = entriesCount;
            return vault;
        }
    }
}
