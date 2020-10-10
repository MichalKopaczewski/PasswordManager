﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Entries.EntryDetail
{
    public class EntryDetailVM
    {
        public long Id { get; internal set; }
        public string Email { get; internal set; }
        public string Password { get; internal set; }
        public string Portal { get; internal set; }
        public string Login { get; internal set; }
    }
}
