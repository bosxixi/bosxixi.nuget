using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Web.Extensions
{
    public static class IdentityExtension
    {
        public static string GetUserDataValue(this IIdentity identity, string key)
        {
            Identity user = CastToIdentity(identity);

            return user.UserData[key];
        }

        public static Dictionary<string, string> GetUserData(this IIdentity identity)
        {
            Identity user = CastToIdentity(identity);

            return user.UserData;

        }
        private static Identity CastToIdentity(IIdentity identity)
        {
            var user = identity as Identity;
            if (user == null)
            {
                throw new InvalidCastException();
            }
            return user;
        }
    }
}
