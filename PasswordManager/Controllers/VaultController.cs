using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PasswordManager.Application.Entries.EntriesList;
using PasswordManager.Application.Vaults.CreateVault;
using PasswordManager.Application.Vaults.RemoveVault;
using PasswordManager.Application.Vaults.ValidateVaultPassword;
using PasswordManager.Application.Vaults.VaultDetail;
using PasswordManager.Application.Vaults.VaultsList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    public class VaultController : BaseController
    {
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpGet("GetVaults")]
        public async Task<ActionResult<IEnumerable<VaultItemVM>>> GetVaultsList()
        {
            var a = await Mediator.Send(new VaultsListQuery());
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpGet("GetVault/{vaultId}")]
        public async Task<ActionResult<VaultDetailVM>> GetVault(long vaultId)
        {
            var a = await Mediator.Send(new VaultDetailQuery() { VaultId = vaultId });
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpGet("GetEntriesList/{vaultId}")]
        public async Task<ActionResult<IEnumerable<EntryItemVM>>> GetEntriesList(long vaultId)
        {
            var a = await Mediator.Send(new EntriesListQuery() { VaultId = vaultId,MasterPassword = Request.Headers["masterPassword"] });
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpPost("CreateVault")]
        public async Task<ActionResult> CreateVault([FromBody] CreateVaultCommand command)
        {
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpPost("ValidateVaultPassword")]
        public async Task<ActionResult> ValidateVaultPassword([FromBody] ValidateVaultPasswordCommand command)
        {
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpPost("RemoveVault")]
        public async Task<ActionResult> RemoveVault([FromBody] RemoveVaultCommand command)
        {
            command.MasterPassword = Request.Headers["masterPassword"];
            var a = await Mediator.Send(command);
            return Ok(a);
        }

    }
}
