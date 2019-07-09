using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KiddyAPI.Crypt
{
    class Decrypt
    {
        private static byte[] AES_Decrypt(byte[] bytesToDecrypt, byte[] password)
        {
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] decryptedBytes = null;
            var ms = new MemoryStream();
            var AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(password, saltBytes, 1000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CBC;
            var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(bytesToDecrypt, 0, bytesToDecrypt.Length);
            cs.Close();
            decryptedBytes = ms.ToArray();
            AES.Clear();
            ms.Close();
            return decryptedBytes;
        }
        public static void FileDecrypt(string file, string password)
        {
            byte[] bytesToBeDecrypt = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypt, passwordBytes);
            File.WriteAllBytes(file, bytesDecrypted);
            string ext = Path.GetExtension(file);
            string result = file.Substring(0, file.Length - ext.Length);
            File.Move(file, result);
        }
        public static string RSADecrypt(string PrivateKey, string encrypted)//shitty method. Need better
        {
            var rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(PrivateKey);
            return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(encrypted), false));
        }
    }
}
