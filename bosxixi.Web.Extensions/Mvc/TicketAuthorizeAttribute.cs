using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bosxixi.Web.Extensions.Mvc
{
    public class TicketAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            HttpCookieCollection cookies = httpContext.Request.Cookies;
            var cookie = cookies[".ASPXAUTH"];
            if (cookie == null)
            {
                return false;
            }

            Principal user = new Principal(new Identity(cookie.Value));
            httpContext.User = user;

            if (user.Identity.IsAuthenticated)
            {
                httpContext.User = user;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
