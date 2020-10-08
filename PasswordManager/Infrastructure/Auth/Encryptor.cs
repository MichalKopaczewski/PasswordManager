using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Auth
{
    public class Encryptor
    {
        private const string SALT = "LMbGPk4TXPMfkPP8lFkBXw==";
        
        public static string Encode(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }
        public static string Encode(string value)
        {
            return Encode(value, SALT);
        }

        public static bool Validate(string value, string salt, string hash)
            => Encode(value, salt) == hash;
        public static bool Validate(string value, string hash)
            => Encode(value, SALT) == hash;

    }
}
