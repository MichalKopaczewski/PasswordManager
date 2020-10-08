using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Application.Users.UserDetails
{
    public class GetUserDetailQuery : IRequest<UserDetailsVM>
    {
        public string Username { get; set; }
    }
}
