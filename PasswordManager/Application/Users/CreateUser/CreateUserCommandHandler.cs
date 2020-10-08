using MediatR;

using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Auth;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.CreateUser
{
    public class CreateUserCommandHandler : BaseRequestHandler, IRequestHandler<CreateUserCommand, string>
    {
        public CreateUserCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using var erpTrans = PmContext.Database.BeginTransaction();
            var a = new User()
            {
                Username = request.Username
            };

            a.Password = Encryptor.Encode(request.Password);

            PmContext.Users.Add(a);

            var userRole = new UserRole()
            {
                RoleName = "SystemUser",
                Username = request.Username
            };
            PmContext.UserRoles.Add(userRole);

            await PmContext.SaveChangesAsync();
            erpTrans.Commit();
            return a.Username;
        }
    }
}
