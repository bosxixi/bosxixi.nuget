using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Services
{
    public interface IMessageSender<T> where T : class
    {
        Task<bool> SendMessageAsync(string target, string templeteId, IDictionary<string, T> keyValuePair, string url = null);
    }
}
