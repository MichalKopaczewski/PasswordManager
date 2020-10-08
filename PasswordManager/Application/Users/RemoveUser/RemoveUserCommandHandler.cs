using MediatR;
using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.RemoveUser
{
    public class RemoveUserCommandHandler : BaseRequestHandler,IRequestHandler<RemoveUserCommand>
    {
        public RemoveUserCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            using var erpTrans = PmContext.Database.BeginTransaction();
            var a = await PmContext.Users.FirstOrDefaultAsync(item => item.Username == request.Username);

            if (a == null)
            {
                //TODO
                throw new Exception();
            }
            //TODO add validation
            PmContext.Users.Remove(a);
            await PmContext.SaveChangesAsync();

            erpTrans.Commit();
            return Unit.Value;
        }
    }
}
