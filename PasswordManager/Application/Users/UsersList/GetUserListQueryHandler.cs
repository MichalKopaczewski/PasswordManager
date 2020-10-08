using MediatR;
using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.UsersList
{
    public class GetUserListQueryHandler : BaseRequestHandler, IRequestHandler<GetUserListQuery, IEnumerable<UserListItemVM>>
    {
        public GetUserListQueryHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserListItemVM>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var a = await PmContext.Users.Select(item => new UserListItemVM() { FullName = item.Username, Username = item.Username }).ToListAsync();

            return a;
        }
    }
}
