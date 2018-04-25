using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindShopCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindShopCore.Controllers
{
    [Route("user/[controller]")]
    public class UserController : Controller
    {
        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        [HttpGet("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn(string email, string password)
        {
            //Validation

            return RedirectToAction("Index","Values","api");
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(string name, string email, string password, string repeatpassword )
        {
            //Validation

           

            return View();
        }

    }
}
