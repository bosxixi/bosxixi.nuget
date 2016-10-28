using Microsoft.VisualStudio.TestTools.UnitTesting;
using bosxixi.Services.SmsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Services.SmsProviders.Tests
{
    [TestClass()]
    public class HaoServicesSmsProviderTests
    {
        [TestMethod()]
        public void HaoServicesSmsProviderTest()
        {
            HaoServicesSmsProvider pro = new HaoServicesSmsProvider("36d942f7b8ef4b4eba1c169ba37613cd");
            var result = pro.SendSMSAsync("18250701303", "896", new Dictionary<string, string>() { ["#time#"] = "bee@bosxixi.com", ["#league#"] = "bee@bosxixi.com", ["#team#"] = "d08" }).GetAwaiter().GetResult();
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SendMessageAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendSMSAsyncTest()
        {
            Assert.Fail();
        }
    }
}