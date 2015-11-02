using System;
using System.Security.Cryptography;
using System.Text;

namespace Logistics.Models
{
    public class Md5Crypto
    {
        public string GetMd5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(text));
            text = BitConverter.ToString(result).Replace("-", "");
            return text;
        }
    }
}