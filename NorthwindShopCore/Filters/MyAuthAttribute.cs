using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;


namespace NorthwindShopCore.Filters
{
    public class MyAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            /*
            var user = filterContext.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            */
        }
        /*
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
            var user = filterContext.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "User" }, { "action", "Register" } //Needed to be changed since time
                   });
            }
            
        }
    */
    }
}
