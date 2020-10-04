using MediatR;

using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Auth.LoginCommand
{
    public class LoginCommandHandler : BaseRequestHandler, IRequestHandler<LoginCommand, UserVM>
    {
        public LoginCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public Task<UserVM> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
