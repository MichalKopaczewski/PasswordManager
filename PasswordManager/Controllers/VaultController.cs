using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PasswordManager.Application.Entries.EntriesList;
using PasswordManager.Application.Vaults.CreateVault;
using PasswordManager.Application.Vaults.RemoveVault;
using PasswordManager.Application.Vaults.VaultsList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    public class VaultController : BaseController
    {
        [Authorize]
        [HttpGet("GetVaults")]
        public async Task<ActionResult> GetVaultsList()
        {
            var a = await Mediator.Send(new VaultsListQuery());
            return Ok(a);
        }
        [Authorize]
        [HttpGet("GetEntriesList/{vaultId}")]
        public async Task<ActionResult> GetEntriesList(long vaultId)
        {
            var a = await Mediator.Send(new EntriesListQuery() { VaultId = vaultId});
            return Ok(a);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateVault")]
        [IgnoreAntiforgeryToken(Order = 1001)]
        public async Task<ActionResult> CreateVault([FromBody] CreateVaultCommand command)
        {
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("RemoveVault")]
        [IgnoreAntiforgeryToken(Order = 1001)]
        public async Task<ActionResult> RemoveVault([FromBody] RemoveVaultCommand command)
        {
            var a = await Mediator.Send(command);
            return Ok(a);
        }

    }
}
