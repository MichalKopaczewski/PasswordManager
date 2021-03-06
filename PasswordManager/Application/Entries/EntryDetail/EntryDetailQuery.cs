﻿using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.EntryDetail
{
    public class EntryDetailQuery : IRequest<EntryDetailVM>
    {
        public string MasterPassword { get; set; }
        public long EntryId{ get; set; }
    }
}
