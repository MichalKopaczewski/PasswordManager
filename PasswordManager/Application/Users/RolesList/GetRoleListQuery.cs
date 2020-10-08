using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.RolesList
{
    public class GetRoleListQuery : IRequest<IEnumerable<RoleListItemVM>>
    {
    }
}
