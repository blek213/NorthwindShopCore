using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindShopCore.Controllers.CustomClasses
{
    public class HandleCookies: Controller
    {
       public string GetCookieVal()
        {
            return Request.Cookies["NickName"];
        } 

    }
}
