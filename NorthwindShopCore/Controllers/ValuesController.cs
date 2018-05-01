using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindShopCore.Models;
using System.IO;

namespace NorthwindShopCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet("Greeting")]
        [Produces("text/html")]
        public HttpResponseMessage Greeting()
        {
            var path = "Views/Values/Greeting.html";
            var response = new HttpResponseMessage();
            response.Content = new StringContent(System.IO.File.ReadAllText(path));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

    }
}
