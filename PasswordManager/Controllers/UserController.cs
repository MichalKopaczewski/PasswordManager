using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PasswordManager.Application.Users.AddUserToRole;
using PasswordManager.Application.Users.CreateUser;
using PasswordManager.Application.Users.RemoveUser;
using PasswordManager.Application.Users.RemoveUserFromRole;
using PasswordManager.Application.Users.RolesList;
using PasswordManager.Application.Users.UpdateUser;
using PasswordManager.Application.Users.UserDetails;
using PasswordManager.Application.Users.UsersList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    public class UserController : BaseController
    {
        [Authorize]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserListItemVM>>> GetUserList()
        {
            var a = await Mediator.Send(new GetUserListQuery());
            return Ok(a);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUser/{username}")]
        public async Task<ActionResult<UserDetailsVM>> GetUser(string username)
        {
            return Ok(await Mediator.Send(new GetUserDetailQuery { Username = username }));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateUser")]
        [IgnoreAntiforgeryToken(Order = 1001)]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        {
            string username = await Mediator.Send(updateUserCommand);
            return RedirectToAction("GetUser", new { username = username });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        [IgnoreAntiforgeryToken(Order = 1001)]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            string username = await Mediator.Send(createUserCommand);
            return RedirectToAction("GetUser", new { username = username });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddUserToRole")]
        [IgnoreAntiforgeryToken(Order = 1001)]
        public async Task<ActionResult> AddUserToRole([FromBody] AddUserToRoleCommand addUserToRole)
        {
            string username = await Mediator.Send(addUserToRole);
            return RedirectToAction("GetUser", new { username = username });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("RemoveUserFromRole")]
        [IgnoreAntiforgeryToken(Order = 1001)]
        public async Task<ActionResult> RemoveUserFromRole([FromBody] RemoveUserFromRoleCommand removeUserFromRole)
        {
            string userId = await Mediator.Send(removeUserFromRole);
            return RedirectToAction("GetUser", new { username = userId });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("RemoveUser")]
        [IgnoreAntiforgeryToken(Order = 1001)]
        public async Task<ActionResult> RemoveUser([FromBody] RemoveUserCommand removeUser)
        {
            await Mediator.Send(removeUser);
            return Ok();
        }
        [Authorize]
        [HttpGet("GetRoles")]
        public async Task<ActionResult<IEnumerable<RoleListItemVM>>> GetRoles()
        {
            var a = await Mediator.Send(new GetRoleListQuery());
            return Ok(a);
        }
        [Authorize]
        [HttpGet("GetRolesNames")]
        public async Task<ActionResult<IEnumerable<RoleListItemVM>>> GetRolesNames()
        {
            var a = await Mediator.Send(new GetRoleListQuery());
            var x = a.Select(item => item.Name);
            return Ok(x);
        }
    }
}
