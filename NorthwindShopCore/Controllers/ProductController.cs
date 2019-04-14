using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NorthwindShopCore.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace NorthwindShopCore.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        public ProductController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
  
        [HttpPost("ConfectionsJsonResult")]
        public JsonResult ConfectionsJsonResult() {
            return Json(DbNorthWind.Products.Where(p => p.CategoryId == 3));
        }

        [HttpPost("BeveragesJsonResult")]
        public JsonResult BeveragesJsonResult() {
            return Json(DbNorthWind.Products.Where(p => p.CategoryId == 1));
        }

        [HttpGet("ConfectionJsonResult/{ConfectionId}")]
        public JsonResult ConfectionJsonResult(int? ConfectionId) {
            return Json(DbNorthWind.Products.Where(p => p.ProductId == ConfectionId));
        }

        [HttpGet("BeverageJsonResult/{BeverageId}")]
        public JsonResult BeverageJsonResult(int? BeverageId) {
            return Json(DbNorthWind.Products.Where(p => p.ProductId == BeverageId));
        }

        [HttpGet("AddToCart")]
        public JsonResult AddToCart(int? IdProductFromView) {
            return Json(DbNorthWind.Products.First(p => p.ProductId == IdProductFromView));
        }

        [HttpGet("CartJsonResult")]
        public JsonResult CartJsonResult()
        {
            string cookieValueFromReq = Request.Cookies["ProductName"];

            if(cookieValueFromReq != null) {
                int positionStr = cookieValueFromReq.IndexOf("/");

                string IdValue = "";

                for (int i = 0; i < positionStr; i++) {
                    IdValue += cookieValueFromReq[i];
                }

                positionStr++;

                string CountValue = "";

                for (int i = positionStr; i < cookieValueFromReq.Length; i++) {
                    CountValue += cookieValueFromReq[i];
                }

                int ProductId = Int32.Parse(IdValue);

                int Count = Int32.Parse(CountValue);

                // Found our model by ProductId

                var FoundModel = DbNorthWind.Products.First(p => p.ProductId == ProductId);

                var ProductResult = new { Obj = FoundModel, CountProducts = Count };

                return Json(ProductResult);
            }
            return null;
        }

        [HttpPost("Cart")]
        public JsonResult Cart(int? IdProductSet, int? InputText, string button) {
            var Product = DbNorthWind.Products.First(p => p.ProductId == IdProductSet);

            string key = "ProductName";
            string value = IdProductSet.ToString() + "/" + InputText.ToString();

            CookieOptions option = new CookieOptions {
                Expires = DateTime.Now.AddDays(30)
            };

            Response.Cookies.Append(key, value, option);
              
            if(button == "Continue") {
                if (Product.CategoryId == 3) {
                    return Json("Confection");
                }
                if (Product.CategoryId == 1) {
                    return Json("Beverage");
                }
            }
            return Json("Buy");
        }

        [HttpPost("isNullCookie")]
        public JsonResult isNullCookie() {
            if (Request.Cookies["ProductName"] != null) {
                return Json("false");
            }
            return Json("true");
        }

        [HttpPost("DeleteProducts")]
        public HttpResponseMessage DeleteProducts() {
            Response.Cookies.Delete("ProductName");
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpPost("BuyProduct")]
        public HttpResponseMessage BuyProduct() {
            Response.Cookies.Delete("ProductName");
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
