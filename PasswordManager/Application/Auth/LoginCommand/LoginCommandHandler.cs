using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Auth;
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

        public async Task<UserVM> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var x = await (from us in PmContext.Users
                           where us.Username == request.Username
                           select new {us.Username,us.Password}
                           ).FirstOrDefaultAsync();
            if (x== null)
            {
                //TODO brak
                throw new Exception("Nie poprawne dane logowanie");
            }
            if (!Encryptor.Validate(request.Password,x.Password))
            {
                //TODO złe hasło
                throw new Exception("Nie poprawne dane logowanie");
            }
            UserVM user = new UserVM();
            user.Username = x.Username;

            var roles = await (from r in PmContext.UserRoles
                               where r.Username == x.Username
                               select r.RoleName
                               ).ToListAsync();
            user.UserRoles = roles;
            return user;
        }
    }
}
