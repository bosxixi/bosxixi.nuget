using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bosxixi.Services.SmsProviders
{
    /// <summary>
    /// http://www.haoservice.com/center/
    /// </summary>
    public class HaoServicesSmsProvider : ISmsSender, IMessageSender<string>
    {
        public HaoServicesSmsProvider(string key)
        {
            this.HaoServicesSmsKey = key;
        }

        public readonly string HaoServicesSmsKey;

        public async Task<bool> SendMessageAsync(string target, string templeteId, IDictionary<string, string> keyValuePair, string url = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://apis.haoservice.com");
                StringBuilder sb = new StringBuilder();
                foreach (var item in keyValuePair)
                {
                    if (item.Key.StartsWith("#") && item.Key.EndsWith("#"))
                    {
                        sb.Append(item.Key);
                    }
                    else
                    {
                        sb.Append($"#{item.Key}#");
                    }
         
                    sb.Append('=');
                    sb.Append(HttpUtility.UrlEncode(item.Value));
                    sb.Append('&');
                }
                string templeteValues = sb.ToString();
                templeteValues = templeteValues.Substring(0, templeteValues.Length - 1);
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("mobile", $"{target}"),//发送至的手机号码
                        new KeyValuePair<string, string>("tpl_id", $"{templeteId}"),//haoservice 短信模板
                        new KeyValuePair<string, string>("key", $"{HaoServicesSmsKey}"),
                        new KeyValuePair<string, string>("tpl_value", templeteValues)//模板变量
                });
                var result = await client.PostAsync("/sms/send", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                if (resultContent.Contains("成功"))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> SendSMSAsync(string phoneNumber, string templeteId, IDictionary<string, string> keyValuePair)
        {
            return await this.SendMessageAsync(phoneNumber, templeteId, keyValuePair);
        }
    }
}
