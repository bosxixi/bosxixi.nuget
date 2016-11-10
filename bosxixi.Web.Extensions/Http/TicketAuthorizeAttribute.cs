using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Security;
using bosxixi.Web.Extensions;

namespace bosxixi.Web.Extensions.Http
{
    public class TicketAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public Type UserDataType { get; private set; }
        public TicketAuthorizeAttribute(Type userDataType)
        {
            UserDataType = userDataType;
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            HttpRequestHeaders headers = actionContext.Request.Headers;

            var authorization = headers.Authorization;

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authorization.Scheme);
            Principal user = new Principal(new Identity(authorization.Scheme));
            actionContext.RequestContext.Principal = user;
            if (ticket.Expired)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
