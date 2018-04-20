using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NorthwindShopCore.Models;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthwindShopCore.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        // GET: api/values
        [HttpGet("Confections")]
        public IActionResult Confections()
        {
            var confections = DbNorthWind.Products.Where(p => p.CategoryId == 3);

            return View(confections);
        }

        
        [HttpGet("Beverages")]
        public IActionResult Beverages()
        {
            var beverages = DbNorthWind.Products.Where(p => p.CategoryId == 1);

            return Json(beverages);
        }
        
        // GET api/values/5
        [HttpGet("Confection/{Id}")]
        public IActionResult Confection(int? id)
        {
            id = 2;
        
            if(id == null)
            {
                var modelConfection = DbNorthWind.Products.First(p => p.ProductId == id);

                var modelCategories = DbNorthWind.Categories.First(p => p.CategoryId == modelConfection.CategoryId);

            

                return Json(modelConfection);
            }

            return RedirectToAction("Confections", "Product", "api");

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
