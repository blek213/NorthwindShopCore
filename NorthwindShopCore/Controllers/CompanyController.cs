using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NorthwindShopCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        CompaniesRangeContext companiesRangeContext = new CompaniesRangeContext();

        [HttpPost("ShowContributors")]
        public JsonResult ShowContributors()
        {
            //var companyData = companiesRangeContext.Company.FromSql("SELECT company.name as CompanyName,owner.name AS ownername,companytype.nametype,company.profitperyear," +
            //    "COUNT(*) AS CountInvestors FROM company JOIN investor ON company.companyid=investor.companyid JOIN owner ON company.ownerid=owner.ownerid JOIN companytype " +
            //    "ON company.typeid=companytype.companytypeid GROUP BY company.name,owner.name,companytype.nametype,company.profitperyear");

            IEnumerable<Company> companyData = companiesRangeContext.Company.ToList();

            return Json(companyData);
        }

        [HttpPost("SignInChat")]
        public JsonResult SignInChat(string nickname, string group)
        {
            string NickNamekey = "NickName";
            string NickNamekeyvalue = nickname;

            string GroupKey = "Group";
            string GroupValue = group;

            if(NickNamekeyvalue == null)
            {
                return Json(HttpStatusCode.BadRequest);
            }

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddDays(30);

            Response.Cookies.Append(NickNamekey, NickNamekeyvalue, option);
            Response.Cookies.Append(GroupKey, GroupValue, option);
            
            return Json(HttpStatusCode.Accepted);
        }

        [HttpPost("GetNickNameByCookie")]
        public JsonResult GetNickNameByCookie()
        {
            return Json(Request.Cookies["NickName"]);
        }

    }
}