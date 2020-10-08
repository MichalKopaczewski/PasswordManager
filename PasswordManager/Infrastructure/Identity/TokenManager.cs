using JWT.Algorithms;
using JWT.Builder;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Identity
{
    public static class TokenManager
    {
        public static string GenerateAccessToken(string secret,User user)
        {
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            foreach (var item in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptior);

            return tokenHandler.WriteToken(token);

            /*
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA512Algorithm())
                .WithSecret(Encoding.ASCII.GetBytes(secret))
                .AddClaim(ClaimTypes.Expiration, DateTimeOffset.UtcNow.AddYears(10).ToUnixTimeSeconds())
                .AddClaim(ClaimTypes.NameIdentifier, user.UserName)
                .AddClaims(claimRoles)
                .Encode();*/
        }
    }
}
