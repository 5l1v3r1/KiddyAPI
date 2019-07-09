using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KiddyAPI.Crypt
{
    class Key
    {
        public static string GenerateKey(int length)
        {
            const string alp = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/";
            Random rnd = new Random();
            var sb = new StringBuilder();
            for (int i = length; i > 0; i--)
            {
                sb.Append(alp[rnd.Next(alp.Length)]);
            }

            return sb.ToString();
        }

        public static void GenerateRsaKey(out string pubKey, out string prvKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            pubKey = rsa.ToXmlString(false);
            prvKey = rsa.ToXmlString(true);
            /////////////////////////////////////////////////////////////////////////////
            //// Think about rsa key store
            /// TODO:RSA Key
            /////////////////////////////////////////////////////////////////////////////
            rsa.Clear();

        }
    }
}
