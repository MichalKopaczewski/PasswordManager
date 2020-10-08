using MediatR;
using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.UserDetails
{
    public class GetUserDetailQueryHandler : BaseRequestHandler,IRequestHandler<GetUserDetailQuery, UserDetailsVM>
    {
        public GetUserDetailQueryHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<UserDetailsVM> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var a = await PmContext.Users.Include(b => b.UserRoles).FirstOrDefaultAsync(item => item.Username == request.Username);

            var d = from role in PmContext.Roles
                    join userRole in PmContext.UserRoles.Where(item => item.Username == a.Username) on role.Name equals userRole.RoleName
                    into Details
                    from m in Details.DefaultIfEmpty()

                    select new UserRoleItemVM
                    {
                        IsAssigned = (m !=null),
                        RoleName = role.Name
                    };
            var e = d.ToList();
            var c = new UserDetailsVM()
            {
                Password = "",
                Username = a.Username,
                UserRoles = e
            };
            if (c!=null)
            {
                c.Password = "";
            }
            return c;
        }
    }
}
