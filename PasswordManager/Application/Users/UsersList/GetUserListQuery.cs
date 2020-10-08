using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.UsersList
{
    public class GetUserListQuery: IRequest<IEnumerable<UserListItemVM>>
    {
    }
}
