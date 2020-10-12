using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Interfaces
{
    public interface ICryptoService
    {
        public string HashString(string text, string key);
        public bool ValidateHash(string value, string hash, string key);
    }
}
