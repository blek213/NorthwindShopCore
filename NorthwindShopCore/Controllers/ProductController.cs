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
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DotNet.PlatformAbstractions;
using System.IO;
using System.Net;

namespace NorthwindShopCore.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
       
        public ProductController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        NORTHWNDContext DbNorthWind = new NORTHWNDContext();
       
        [HttpPost("ConfectionsJsonResult")]
        public JsonResult ConfectionsJsonResult()
        {
            IEnumerable<Products> confections = DbNorthWind.Products.Where(p => p.CategoryId == 3);

            //var confections = DbNorthWind.Products.Where(p => p.CategoryId == 3);

            //var JsonConfections = JsonConvert.SerializeObject(confections);

            return Json(confections);
        }

        [HttpPost("BeveragesJsonResult")]
        public JsonResult BeveragesJsonResult()
        {
            var beverages = DbNorthWind.Products.Where(p => p.CategoryId == 1);

            var JsonBeverages = JsonConvert.SerializeObject(beverages);

            return Json(JsonBeverages);
        }

        [HttpGet("ConfectionJsonResult/{ConfectionId}")]
        public JsonResult ConfectionJsonResult(int? ConfectionId)
        {
            var modelConfection = DbNorthWind.Products.Where(p => p.ProductId == ConfectionId);

            var JsonConfection = JsonConvert.SerializeObject(modelConfection);

            return Json(JsonConfection);
        }

        [HttpGet("BeverageJsonResult/{BeverageId}")]
        public JsonResult BeverageJsonResult(int? BeverageId)
        {
            var modelBeverage = DbNorthWind.Products.Where(p => p.ProductId == BeverageId);

            var JsonBeverage = JsonConvert.SerializeObject(modelBeverage);

            return Json(JsonBeverage);
        }

        [HttpGet("AddToCart")]
        public JsonResult AddToCart(int? IdProductFromView)
        {
            var Product = DbNorthWind.Products.First(p => p.ProductId == IdProductFromView);

            var JsonProduct = JsonConvert.SerializeObject(Product);

            return Json(JsonProduct);
        }

        [HttpGet("CartJsonResult")]
        public JsonResult CartJsonResult()
        {
            string cookieValueFromReq = Request.Cookies["ProductName"];

            if(cookieValueFromReq != null)
            {
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

            return null;
        }

        [HttpPost("Cart")]
        public JsonResult Cart(int? IdProductSet, int? InputText, string button)
        {
            var Product = DbNorthWind.Products.First(p => p.ProductId == IdProductSet);

            string key = "ProductName";
            string value = IdProductSet.ToString() + "/" + InputText.ToString();

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddDays(30);

            Response.Cookies.Append(key, value, option);
                 
            if(button == "Continue")
            {
                if (Product.CategoryId == 3)
                {
                    //   return Redirect("~/html/Product/Confection.html?ConfectionIdVal=" + IdProductSet);
                    return Json("Confection");
                }

                if (Product.CategoryId == 1)
                {
                    return Json("Beverage");
                }
            }

            return Json("Buy");
        }

        [HttpPost("BuyProduct")]
        public JsonResult BuyProduct()
        {
            Response.Cookies.Delete("ProductName");

            return Json(HttpStatusCode.Accepted);

        }
    }
}
