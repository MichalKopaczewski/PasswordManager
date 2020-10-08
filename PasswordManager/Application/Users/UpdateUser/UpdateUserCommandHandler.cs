using MediatR;
using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Auth;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.UpdateUser
{
    public class UpdateUserCommandHandler : BaseRequestHandler, IRequestHandler<UpdateUserCommand, string>
    {
        public UpdateUserCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using var erpTrans = PmContext.Database.BeginTransaction();
            var user = await PmContext.Users.FirstOrDefaultAsync(item => item.Username == request.Username);

            if (user == null)
            {
                throw new Exception("Nie znaleziono użytkownika");
            }
            user.Username = request.Username;


            user.Password = Encryptor.Encode(request.Password);
            PmContext.Users.Update(user);

            await PmContext.SaveChangesAsync();

            erpTrans.Commit();
            return user.Username;
        }
    }
}
