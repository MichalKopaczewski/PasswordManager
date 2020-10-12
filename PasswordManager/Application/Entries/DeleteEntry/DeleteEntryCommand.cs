using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.DeleteEntry
{
    public class DeleteEntryCommand : IRequest
    {
        public long EntryId { get; set; }
        public string MasterPassword { get; set; }
    }
}
