using MediatR;

using Microsoft.EntityFrameworkCore;

using PasswordManager.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.EntryDetail
{
    public class EntryDetailQueryHandler : BaseRequestHandler, IRequestHandler<EntryDetailQuery, EntryDetailVM>
    {
        public EntryDetailQueryHandler(PasswordManagerContext context) : base(context)
        {
        }

        public async Task<EntryDetailVM> Handle(EntryDetailQuery request, CancellationToken cancellationToken)
        {
            var entry = await (from en in PmContext.Entries
                               where en.Id == request.EntryId
                               select new { en.Id,en.Email,en.Password,en.Portal,en.Login,en.VaultId }
                               ).FirstOrDefaultAsync();
            var entryDetails = new EntryDetailVM()
            {
                Id = entry.Id,
                Email = entry.Email,
                Password = entry.Password,
                Portal = entry.Portal,
                Login = entry.Login,
                VaultId = entry.VaultId
            };

            return entryDetails;

        }
    }
}
