using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Auth;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Services
{
    public class CryptoService : ICryptoService
    {
        
        public CryptoService()
        {

        }

        public string HashString(string text, string key)
        {
            return Encryptor.HashString(text, key);
        }

        public bool ValidateHash(string value, string hash, string key)
        {
            return Encryptor.Validate(value, key, hash);
        }
    }
}
