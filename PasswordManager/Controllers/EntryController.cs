﻿using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateEntry")]
        public async Task<ActionResult> CreateVault([FromBody] CreateEntryCommand command)
        {
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateEntry")]
        public async Task<ActionResult> CreateVault([FromBody] UpdateEntryCommand command)
        {
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteEntry")]
        public async Task<ActionResult> DeleteEntry([FromBody] DeleteEntryCommand command)
        {
            var a = await Mediator.Send(command);
            return Ok(a);
        }
        [Authorize]
        [HttpGet("GetEntry/{entryId}")]
        public async Task<ActionResult> GetEntry(long entryId)
        {
            var a = await Mediator.Send(new EntryDetailQuery() { EntryId = entryId});
            return Ok(a);
        }
    }
}