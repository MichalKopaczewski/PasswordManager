using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.RemoveUser
{
    public class RemoveUserCommand : IRequest
    {
        public string Username{ get; set; }
    }
}
