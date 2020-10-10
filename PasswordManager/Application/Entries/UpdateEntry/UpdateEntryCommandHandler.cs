using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.UpdateEntry
{
    public class UpdateEntryCommandHandler : BaseRequestHandler, IRequestHandler<UpdateEntryCommand>
    {
        public UpdateEntryCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<Unit> Handle(UpdateEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = await PmContext.Entries.FirstOrDefaultAsync(item => item.Id == request.Id);
            if (entry == null)
            {
                throw new Exception();
            }
            entry.Email = request.Email;
            entry.Login = request.Login;
            entry.Password = request.Password;
            entry.Portal = request.Portal;

            PmContext.Update(entry);
            PmContext.SaveChanges();
            return Unit.Value;
        }
    }
}
