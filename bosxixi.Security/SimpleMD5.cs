using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Security
{
    public static class SimpleMD5
    {
        public static string MD5Encrypt(string code)
        {
            byte[] sourceCode = Encoding.Default.GetBytes(code);
            byte[] targetCode;

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            targetCode = md5.ComputeHash(sourceCode);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in targetCode)
            {
                sb.AppendFormat("{0:X2}", b);
            }

            return sb.ToString().ToLower();
        }
    }
}
