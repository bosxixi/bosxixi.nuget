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
              DateTime.Now.AddDays(7),
              false,
              JsonConvert.SerializeObject(userData),
              FormsAuthentication.FormsCookiePath);

            return FormsAuthentication.Encrypt(ticket);
        }

        public static void Signin(string encryptTicket, HttpResponseBase response)
        {
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
            response.Cookies.Add(cookie);
        }

        public static void Signout(string encryptTicket, HttpResponseBase response)
        {
            response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }
    }
}
