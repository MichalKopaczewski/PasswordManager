using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.CreateEntry
{
    public class CreateEntryCommand : IRequest
    {
        public string Portal { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public long VaultId { get; set; }
    }
}
