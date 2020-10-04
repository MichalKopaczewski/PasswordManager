using JWT.Algorithms;
using JWT.Builder;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Identity
{
    public static class TokenManager
    {
        public static string GenerateAccessToken(string secret,User user)
        {
            List<KeyValuePair<string, object>> claimRoles = new List<KeyValuePair<string, object>>();
            foreach (string item in user.Roles)
            {
                claimRoles.Add(new KeyValuePair<string, object>(ClaimTypes.Role,item));
            }
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA512Algorithm())
                .WithSecret(Encoding.ASCII.GetBytes(secret))
                .AddClaim(ClaimTypes.Expiration, DateTimeOffset.UtcNow.AddYears(10).ToUnixTimeSeconds())
                .AddClaim(ClaimTypes.NameIdentifier, user.UserName)
                .AddClaims(claimRoles)
                .Encode();
        }
    }
}
