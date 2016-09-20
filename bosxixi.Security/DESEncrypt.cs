using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace bosxixi.Security
{
    public class DESEncrypt
    {
        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }

        public string EncryptString(string plainText, string password)
        {
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            MemoryStream myStream = new MemoryStream();

            TripleDES des = CreateDES(password);

            CryptoStream cryptoStream = new CryptoStream(myStream, des.CreateEncryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(myStream.ToArray());
        }

        public string DecryptString(string encryptedText, string password)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            MemoryStream myStream = new MemoryStream();

            TripleDES des = CreateDES(password);

            CryptoStream decryptoStream = new CryptoStream(myStream, des.CreateDecryptor(), CryptoStreamMode.Write);

            decryptoStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
            decryptoStream.FlushFinalBlock();

            return Encoding.Unicode.GetString(myStream.ToArray());
        }
    }
}
