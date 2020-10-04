using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Auth.LoginCommand
{
    public class LoginCommand : IRequest<UserVM>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
