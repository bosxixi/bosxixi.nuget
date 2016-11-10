using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace bosxixi.Web.Extensions
{
    public class Identity : IIdentity
    {
        public Identity(string encryptedTicket)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encryptedTicket);
            this.Name = ticket.Name;

            if (ticket.Expired)
            {
                this.IsAuthenticated = false;
            }
            else
            {
                this.IsAuthenticated = true;
            }

            this.UserData = this.DeserializeUserData(ticket.UserData);
        }
        public string AuthenticationType => "Ticket";
        public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }

        public Dictionary<string,string> UserData { get; private set; }

        public Dictionary<string, string> DeserializeUserData(string userDataString)
        {
            if (String.IsNullOrEmpty(userDataString))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(userDataString);
        }

        public string SerializeUserData(Dictionary<string, string> userData)
        {
            if (userData == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(this);
        }
    }
}
