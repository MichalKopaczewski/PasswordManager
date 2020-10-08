using MediatR;
using Microsoft.EntityFrameworkCore;

using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.RemoveUserFromRole
{
    public class RemoveUserFromRoleCommandHandler : BaseRequestHandler, IRequestHandler<RemoveUserFromRoleCommand, string>
    {
        public RemoveUserFromRoleCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<string> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            using var erpTrans = PmContext.Database.BeginTransaction();
            UserRole userRole = await PmContext.UserRoles.FirstOrDefaultAsync(item => item.Username == request.Username && item.RoleName== request.Rolename);

            if (userRole!=null)
            {
                PmContext.UserRoles.Remove(userRole);
                await PmContext.SaveChangesAsync(cancellationToken);
            } else
            {
                throw new Exception( request.Rolename + " " + request.Username);
            }

            erpTrans.Commit();
            return request.Username;
        }
    }
}
