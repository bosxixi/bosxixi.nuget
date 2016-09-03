using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Services
{
    public interface IMailClient
    {
        event SendCompletedEventHandler OnSendCompleted;

        void Dispose();
        Task<bool> SendMailAsync(string recipientEmail, string subject, string message);
    }
}
