using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Web.Extensions
{
    public class UserData
    {
        public UserData(string openId, LoginType loginType, long userId)
        {
            this.OpenId = openId;
            this.LoginType = loginType;
            this.UserId = userId;
        }
        public string OpenId { get; private set; }
        public LoginType LoginType { get; private set; }
        public long UserId { get; private set; }
    }
}
