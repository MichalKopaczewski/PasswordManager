using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.DeleteEntry
{
    public class DeleteEntryCommandHandler : BaseRequestHandler, IRequestHandler<DeleteEntryCommand>
    {
        public DeleteEntryCommandHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<Unit> Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
        {
            var entry = await PmContext.Entries.FirstOrDefaultAsync(x => x.Id == request.EntryId);

            if (entry == null)
            {
                throw new Exception();
            }
            PmContext.Remove(entry);
            PmContext.SaveChanges();
            return Unit.Value;


        }
    }
}
