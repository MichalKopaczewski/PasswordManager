using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.RolesList
{
    public class GetRoleListQueryHandler : BaseRequestHandler, IRequestHandler<GetRoleListQuery, IEnumerable<RoleListItemVM>>
    {
        public GetRoleListQueryHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoleListItemVM>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var a = await PmContext.Roles.Select(x => new RoleListItemVM()
            {
                Description = x.Description,
                Name = x.Name
            }).ToListAsync() ;
            return a;
        }
    }
}
