using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Web.Extensions
{
    public interface IUserData<T>
    {
        string SerializeUserData(T userData);
        T DeserializeUserData(string userDataString);
    }
}
