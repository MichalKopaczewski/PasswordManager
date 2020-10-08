using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.RemoveUserFromRole
{
    public class RemoveUserFromRoleCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Rolename { get; set; }
    }
}
