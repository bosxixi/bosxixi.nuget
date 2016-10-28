using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Services
{
    public interface ISmsSender
    {
        Task<bool> SendSMSAsync(string phoneNumber, string templeteId, IDictionary<string, string> keyValuePair);
    }
}
