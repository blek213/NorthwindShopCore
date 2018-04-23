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
            return View();
        }

        [HttpPost("ConfectionsJsonResult")]
        public JsonResult ConfectionsJsonResult()
        {
            var confections = DbNorthWind.Products.Where(p => p.CategoryId == 3);

            var JsonConfections = JsonConvert.SerializeObject(confections);

            return Json(JsonConfections);
        }

        [HttpGet("Beverages")]
        public IActionResult Beverages()
        {
            return View();
        }

        [HttpPost("BeveragesJsonResult")]
        public JsonResult BeveragesJsonResult()
        {
            var beverages = DbNorthWind.Products.Where(p => p.CategoryId == 1);

            var JsonBeverages = JsonConvert.SerializeObject(beverages);

            return Json(JsonBeverages);
        }
        // GET api/values/5
        [HttpGet("Confection/{Id}")]
        public IActionResult Confection(int? id)
        {
            if (id != null)
            {
                ViewBag.IdProduct = id;
                return View();
            }

            return RedirectToAction("Confections", "Product", "api");

        }

        [HttpGet("ConfectionJsonResult/{ConfectionId}")]
        public JsonResult ConfectionJsonResult(int? ConfectionId)
        {
            var modelConfection = DbNorthWind.Products.Where(p => p.ProductId == ConfectionId);

            var JsonConfection = JsonConvert.SerializeObject(modelConfection);

            return Json(JsonConfection);
        }
 
        [HttpGet("Beverage/{Id}")]
        public IActionResult Beverage(int? id)
        {
            if (id != null)
            {

                return View();
            }

            return RedirectToAction("Beverages", "Product", "api");
        }

        [HttpGet("BeverageJsonResult/{BeverageId}")]
        public JsonResult BeverageJsonResult(int? BeverageId)
        {
            var modelBeverage = DbNorthWind.Products.Where(p => p.ProductId == BeverageId);

            var JsonBeverage= JsonConvert.SerializeObject(modelBeverage);

            return Json(JsonBeverage);
        }

        

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

    }
}
