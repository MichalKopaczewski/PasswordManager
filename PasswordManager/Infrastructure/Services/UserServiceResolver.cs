using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using PasswordManager.Domain.Entities;
using PasswordManager.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Services
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;

        public PasswordManagerContext PmContext { get; }

        public UserResolverService(IHttpContextAccessor context,PasswordManagerContext pmContext)
        {
            _context = context;
            PmContext = pmContext;
        }
        public User GetUser()
        {
            return (from us in PmContext.Users
                   where us.Username == GetUsername()
                   select us).FirstOrDefault();
        }
        public string GetUsername()
        {
            if (_context.HttpContext == null) return "";
            if (_context.HttpContext.User == null) return "";
            return _context.HttpContext.User.Identity.Name;
        }
        public IEnumerable<string> GetRoles()
        {
            var roles = ((ClaimsIdentity)_context.HttpContext.User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            return roles;
        }
        public bool UserInRole(string role)
        {
            return GetRoles().Any(item => item.Equals(role, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
