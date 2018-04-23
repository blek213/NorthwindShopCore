using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindShopCore.Models;

namespace NorthwindShopCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        NORTHWNDContext DbNorthWind = new NORTHWNDContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("AddToCart")]
        public ActionResult AddToCart(int? IdProductFromView)
        {
            var Product = DbNorthWind.Products.First(p => p.ProductId == IdProductFromView);

            return PartialView(Product);
        }

        [HttpPost]
        public ActionResult Cart(int? IdProductSet, int? InputText, string button)
        {
            var Product = DbNorthWind.Products.First(p => p.ProductId == IdProductSet);

            string key = "ProductName";
            string value = IdProductSet.ToString() + "/" + InputText.ToString();

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddDays(30);

            Response.Cookies.Append(key,value, option);

            if (button == "Buy")
            {
                return RedirectToAction("BuyProduct", "Product");
            }

            return RedirectToAction("Confection", "Product", new { Id = IdProductSet });
        }



    }
}
