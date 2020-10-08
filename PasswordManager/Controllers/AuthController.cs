using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using PasswordManager.Application.Auth.LoginCommand;
using PasswordManager.Infrastructure.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    public class AuthController : BaseController
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;

        public AuthController(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            UserVM user = await Mediator.Send(loginCommand);
            return Ok(new 
            { 
                token = TokenManager.GenerateAccessToken(configuration.GetSection("AppSettings:Token").Value, new Infrastructure.Identity.User() {UserName = user.Username,Roles = user.UserRoles }) 
            });
        }
    }
}
