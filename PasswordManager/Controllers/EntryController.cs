using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PasswordManager.Application.Entries.CreateEntry;
using PasswordManager.Application.Entries.DeleteEntry;
using PasswordManager.Application.Entries.EntryDetail;
using PasswordManager.Application.Entries.UpdateEntry;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    public class EntryController : BaseController
    {

        [Authorize(Roles = "Admin,SystemUser")]
        [HttpPost("CreateEntry")]
        public async Task<ActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            command.MasterPassword = Request.Headers["masterPassword"];
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpPost("UpdateEntry")]
        public async Task<ActionResult> UpdateEntry([FromBody] UpdateEntryCommand command)
        {
            command.MasterPassword = Request.Headers["masterPassword"];
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpPost("DeleteEntry")]
        public async Task<ActionResult> DeleteEntry([FromBody] DeleteEntryCommand command)
        {
            command.MasterPassword = Request.Headers["masterPassword"];
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin,SystemUser")]
        [HttpGet("GetEntry/{entryId}")]
        public async Task<ActionResult<EntryDetailVM>> GetEntry(long entryId)
        {
            var a = await Mediator.Send(new EntryDetailQuery() { EntryId = entryId,MasterPassword= Request.Headers["masterPassword"] });
            return Ok(a);
        }
    }
}
