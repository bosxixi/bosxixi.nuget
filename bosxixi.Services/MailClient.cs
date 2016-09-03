using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Services
{
    public class MailClient : IDisposable, IMailClient
    {
        public MailClient(string userName, string password)
        {
            this.client = new SmtpClient("smtp-mail.outlook.com");
            this.userName = userName;
            this.password = password;
        }

        private string userName { get; }
        private string password { get; }
        private SmtpClient client { get; }
        public event SendCompletedEventHandler OnSendCompleted
        {
            add
            {
                client.SendCompleted += value;
            }
            remove
            {
                client.SendCompleted -= value;
            }
        }

        public async Task<bool> SendMailAsync(string recipientEmail, string subject, string message)
        {
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            NetworkCredential credentials = new NetworkCredential(userName, password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                MailAddress from = new MailAddress(userName.Trim());
                MailAddress to = new MailAddress(recipientEmail.Trim());
                var mail = new MailMessage(from, to);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = false;
                await client.SendMailAsync(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
