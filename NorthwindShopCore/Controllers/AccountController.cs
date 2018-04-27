using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindShopCore.Controllers
{
    [Produces("application/json")]
    [Route("Account")]
    public class AccountController : Controller
    {
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }


    }
}