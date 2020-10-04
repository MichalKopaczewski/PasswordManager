using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    public class TestController : BaseController
    {
        public async Task<ActionResult> GetTestPage()
        {
            return Ok("Ala ma kota");
        }
    }
}
