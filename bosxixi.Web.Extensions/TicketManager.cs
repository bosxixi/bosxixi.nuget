using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Newtonsoft.Json;
using System.Web;

namespace bosxixi.Web.Extensions
{
    public class TicketManager
    {
        public static string SignTicket(string userName, Dictionary<string, string> userData, DateTime expiration)
        {
           
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
              userName,
              DateTime.Now,
              expiration,
              false,
              JsonConvert.SerializeObject(userData),
              FormsAuthentication.FormsCookiePath);
        
            return FormsAuthentication.Encrypt(ticket);
        }

        public static Identity GetIdentity(string encryptedTicket)
        {
            return new Identity(encryptedTicket);
        }

        public static void Signin(string encryptTicket, HttpResponseBase response)
        {
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
            response.Cookies.Add(cookie);
        }

        public static void Signout(HttpResponseBase response)
        {
            var c = new HttpCookie(FormsAuthentication.FormsCookieName);
            c.Expires = DateTime.Now.AddDays(-1);
            response.Cookies.Set(c);
        }
    }
}
