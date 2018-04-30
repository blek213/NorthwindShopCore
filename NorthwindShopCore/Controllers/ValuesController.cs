using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindShopCore.Models;

namespace NorthwindShopCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
