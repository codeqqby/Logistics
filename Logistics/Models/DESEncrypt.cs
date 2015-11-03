using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Logistics.Models
{
    public class DESEncrypt
    {
        private static DESEncrypt uniqueInstance;
        private static readonly object locker = new object();
        private string _key = "zhouygaow";

        private DESEncrypt() { }

        public static DESEncrypt CreateInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new DESEncrypt();
                    }
                }
            }
            return uniqueInstance;
        }

        /**/
        /// <summary> 
        /// DES加密 
        /// </summary> 
        /// <param name="encryptString"></param> 
        /// <returns></returns> 
        public string Encrypt(string encryptString)
        {
            byte[] Key = Encoding.UTF8.GetBytes(_key.Substring(0, 8));
            byte[] IV = Key;
            byte[] input = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, provider.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                {
                    cs.Write(input, 0, input.Length);
                    cs.FlushFinalBlock();
                    buffer = stream.ToArray();
                }
            }
            return Convert.ToBase64String(buffer);
        }

        /**/
        /// <summary> 
        /// DES解密 
        /// </summary> 
        /// <param name="decryptString"></param> 
        /// <returns></returns> 
        public string Decrypt(string decryptString)
        {
            byte[] Key = Encoding.UTF8.GetBytes(_key.Substring(0, 8));
            byte[] IV = Key;
            byte[] input = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, provider.CreateDecryptor(Key, IV), CryptoStreamMode.Write))
                {
                    cs.Write(input, 0, input.Length);
                    cs.FlushFinalBlock();
                    buffer = stream.ToArray();
                }
            }
            return Encoding.UTF8.GetString(buffer);
        }
    }
}