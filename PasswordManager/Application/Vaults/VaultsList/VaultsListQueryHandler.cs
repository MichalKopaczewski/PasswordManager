using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;
using PasswordManager.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Vaults.VaultsList
{
    public class VaultsListQueryHandler : BaseRequestHandler, IRequestHandler<VaultsListQuery, IEnumerable<VaultItemVM>>
    {
        public VaultsListQueryHandler(PasswordManagerContext context,UserResolverService userResolverService) : base(context)
        {
            UserResolverService = userResolverService;
        }

        public UserResolverService UserResolverService { get; }

        public async Task<IEnumerable<VaultItemVM>> Handle(VaultsListQuery request, CancellationToken cancellationToken)
        {
            var vaults = await (from v in PmContext.Vaults
                                where v.Username == UserResolverService.GetUsername()
                                select new VaultItemVM
                                {
                                    Name = v.Name,
                                    VaultId = v.Id
                                }
                                ).ToListAsync();
            return vaults;
        }
    }
}
