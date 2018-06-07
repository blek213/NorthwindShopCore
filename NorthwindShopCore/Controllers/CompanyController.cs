﻿using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}