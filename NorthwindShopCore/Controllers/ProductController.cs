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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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

            var JsonBeverage = JsonConvert.SerializeObject(modelBeverage);

            return Json(JsonBeverage);
        }

        [HttpGet("AddToCart")]
        public IActionResult AddToCart(int? IdProductFromView)
        {
            var Product = DbNorthWind.Products.First(p => p.ProductId == IdProductFromView);

            return PartialView(Product);
        }

        [HttpGet("Cart")]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpGet("CartJsonResult")]
        public JsonResult CartJsonResult()
        {
            string cookieValueFromReq = Request.Cookies["ProductName"];

            int positionStr = cookieValueFromReq.IndexOf("/");

            string IdValue = "";

            for (int i = 0; i < positionStr; i++)
            {
                IdValue += cookieValueFromReq[i];
            }

            positionStr++;

            string CountValue = "";

            for (int i = positionStr; i < cookieValueFromReq.Length; i++)
            {
                CountValue += cookieValueFromReq[i];
            }

            int ProductId = Int32.Parse(IdValue);

            int Count = Int32.Parse(CountValue);

            // Found our model by ProductId

            var FoundModel = DbNorthWind.Products.First(p => p.ProductId == ProductId);

            var ProductResult = new { Obj = FoundModel, CountProducts = Count };

            var JsonResult = JsonConvert.SerializeObject(ProductResult);

            return Json(JsonResult);
        }

        [HttpPost("Cart")]
        public IActionResult Cart(int? IdProductSet, int? InputText, string button)
        {
            var Product = DbNorthWind.Products.First(p => p.ProductId == IdProductSet);

            string key = "ProductName";
            string value = IdProductSet.ToString() + "/" + InputText.ToString();

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddDays(30);

            Response.Cookies.Append(key, value, option);

            if (button == "Buy")
            {
                return RedirectToAction("BuyProduct", "Product", "api");
            }

            return RedirectToAction("Confection", "Product", new { Id = IdProductSet });
        }

        [Authorize(Roles ="user")]
        [HttpGet("BuyProduct")]
        public IActionResult BuyProduct()
        {
            Response.Cookies.Delete("ProductName");

            return View();
        }

    }
}
