using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthwindShopCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindShopCore.Controllers
{
    [Route("user/[controller]")]
    public class UserController : Controller
    {
        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string name, string email, string password, string repeatpassword)
        {
            //Validation

            //Adding user

            IdentityUser IdentityUser = new IdentityUser { Email = email, UserName = name };

            var result = await _userManager.CreateAsync(IdentityUser, password);

            if (result.Succeeded)
            {
                
                return RedirectToAction("Index","Values","api");
            }

            return Json(result);
        }

        [HttpPost("LogOff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {

            return RedirectToAction("Index", "Home", "api");
        }



    }
}
