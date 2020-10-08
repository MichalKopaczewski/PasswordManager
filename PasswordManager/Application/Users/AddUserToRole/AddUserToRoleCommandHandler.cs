using MediatR;

using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.AddUserToRole
{
    public class AddUserToRoleCommandHandler : BaseRequestHandler, IRequestHandler<AddUserToRoleCommand,string>
    {
        public AddUserToRoleCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<string> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            using var erpTrans = PmContext.Database.BeginTransaction();
            UserRole userRole = new UserRole()
            {
                RoleName = request.Rolename,
                Username = request.Username
            };
            PmContext.UserRoles.Add(userRole);

            await PmContext.SaveChangesAsync(cancellationToken);
            erpTrans.Commit();
            return request.Username;
        }
    }
}
