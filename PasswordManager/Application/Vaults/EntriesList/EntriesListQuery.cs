using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.EntriesList
{
    public class EntriesListQuery : IRequest<IEnumerable<EntryItemVM>>
    {
        public long VaultId { get; set; }
        public string MasterPassword { get; set; }
    }
}
