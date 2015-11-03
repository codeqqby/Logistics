using System;
using System.Security.Cryptography;
using System.Text;

namespace Logistics.Models
{
    public class Md5Encrypt
    {
        private static Md5Encrypt uniqueInstance;
        private static readonly object locker = new object();

        private Md5Encrypt() { }

        public static Md5Encrypt CreateInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new Md5Encrypt();
                    }
                }
            }
            return uniqueInstance;
        }

        public string Encrypt(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(text));
            text = BitConverter.ToString(result).Replace("-", "");
            return text;
        }
    }
}