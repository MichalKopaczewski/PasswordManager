﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Auth
{
    public class Encryptor
    {
        private static int saltLengthLimit = 32;
        private const string SALT = "LMbGPk4TXPMfkPP8lFkBXw==";
        
        public static string HashString(string value, string salt)
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
            return HashString(value, SALT);
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        public static string GenerateSalt()
        {
            return Encoding.UTF8.GetString(GetSalt(saltLengthLimit));
        }

        public static bool Validate(string value, string salt, string hash)
            => HashString(value, salt) == hash;
        public static bool Validate(string value, string hash)
            => HashString(value, SALT) == hash;

    }
}
