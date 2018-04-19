using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindShopCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindShopCore.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        // GET: api/values
        [HttpGet]
        public IActionResult Confections()
        {
            var confections = DbNorthWind.Products.FromSql("SELECT * FROM Products WHERE CategoryID = 3");

            return Json(confections);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
