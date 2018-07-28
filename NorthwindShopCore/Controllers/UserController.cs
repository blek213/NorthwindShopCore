using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NorthwindShopCore.Filters;
using NorthwindShopCore.Models;

namespace NorthwindShopCore.Controllers
{
    [Route("user/[controller]")]
    public class UserController : Controller
    {
        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<JsonResult> Login(string name, string password)
        {
            var identity =  await GetIdentityLogin(name, password);
          
            if(identity != null)
            {
                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                //HttpContext.SignInAsync(IdentityConstants.ExternalScheme, new ClaimsPrincipal(identity));

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                //Response.ContentType = "application/json";
                //Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));

                //  return Json(new { AccessJson=JsonResult, HttpCode=HttpStatusCode.Accepted });
                return Json(new { JsonResponseRes = response, JsonHttpStatusCode = HttpStatusCode.Accepted });
            }

            return Json(HttpStatusCode.BadRequest);
        }

        [HttpPost("Register")]
        public async Task<JsonResult> Register(string name, string email, string password, string repeatpassword)
        {
            IdentityUser User = new IdentityUser { Email = email, UserName = name };

            var roleUser = new IdentityRole { Name = "user" };

             await _roleManager.CreateAsync(roleUser);

            var result =  _userManager.CreateAsync(User, password);

            if (result != null)
            {
                var identity = GetIdentityRegister(name, password);

                if (identity == null)
                {
                    AddToRoleAsyncFunc(User, roleUser);

                    var now = DateTime.UtcNow;

                    identity = SendClaimsInRegister(name);

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

                    return Json(new { JsonResponseRes = response, JsonHttpStatusCode = HttpStatusCode.Accepted });
                }
            }

            return Json(HttpStatusCode.Forbidden);

        }

        [Route("ChangeIdentity")]
        public async Task<HttpResponseMessage> ChangeUserRole()
        {
           

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        } 

        public void AddToRoleAsyncFunc(IdentityUser User, IdentityRole roleUser)
        {
            _userManager.AddToRoleAsync(User, roleUser.Name);
        }

        private async Task<ClaimsIdentity> GetIdentityLogin(string name, string password)
        {
            List<IdentityUser> identityUsers = _userManager.Users.ToList();

            IdentityUser user = identityUsers.FirstOrDefault(p => p.UserName == name);

            var checkPassword= _userManager.CheckPasswordAsync(user, password);

            if (user != null && checkPassword.Result == true)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName)

                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
                }


                ClaimsIdentity claimsIdentity =
              new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                  ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;

        }


        private ClaimsIdentity GetIdentityRegister(string username, string password)
        {
            List<IdentityUser> identityUsers = _userManager.Users.ToList();

            IdentityUser user = identityUsers.FirstOrDefault(p => p.UserName == username );

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,"user")
                };

                ClaimsIdentity claimsIdentity =
              new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                  ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        public ClaimsIdentity SendClaimsInRegister(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,"user")
                };

            ClaimsIdentity claimsIdentity =
          new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
              ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        [HttpPost("IsAuth")]
        [Authorize(Roles = "user")]
        public JsonResult IsAuth()
        {
          
            return Json(HttpStatusCode.Accepted);
        }
        
        [HttpPost("LogOff")]
        public JsonResult LogOff()
        {
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return Json(HttpStatusCode.Accepted);
        }

        //public JsonResult GetTokens()
        //{
                 
        //    return Json();
        //} 

    }
}
