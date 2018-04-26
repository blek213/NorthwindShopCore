using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NorthwindShopCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindShopCore.Controllers
{
    [Route("user/[controller]")]
    public class UserController : Controller
    {
        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        [HttpGet("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(string name, string password)
        {
            var identity = GetIdentity(name,password);


            return Json(identity);
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


            IdentityUser User = new IdentityUser { Email = email, UserName = name };


            var roleUser = new IdentityRole { Name = "user" };

            await _roleManager.CreateAsync(roleUser);

            //IdentityResult identityResult = await _roleManager.CreateAsync(new IdentityRole("user"));

            var result = await _userManager.CreateAsync(User, password);

            if (result.Succeeded)
            {
                var identity = GetIdentity(name, password);

                if(identity == null)
                {

                    await _userManager.AddToRoleAsync(User, roleUser.Name);

                    return Json(identity);
                }

                var now = DateTime.UtcNow;

                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                Response.ContentType = "application/json";

                await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));

                return Json(result);
            }

            return Json(result);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            List<IdentityUser> persons = new List<IdentityUser>();

            IdentityUser user = persons.FirstOrDefault(p => p.UserName == username && p.PasswordHash == password);

            if(user != null)
            {
                
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName),
                //    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.ro)

                //};
               
                
            }

            return null;
        }



        [HttpPost("LogOff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {

            return RedirectToAction("Index", "Home", "api");
        }



    }
}
