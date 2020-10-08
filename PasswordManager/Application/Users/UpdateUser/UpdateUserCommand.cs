using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<string>
    {
        public string Password { get; set; }
        public string Username { get; set; }

    }
}
