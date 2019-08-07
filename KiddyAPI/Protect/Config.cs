/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////

using System;
using System.Security.Principal;

namespace KiddyAPI.Protect
{
    /// <summary>
    /// Module config
    /// </summary>
    public class Config
    {
        /// <summary>
        /// True if u have adm privilege
        /// </summary>
        public static bool IsAdmin => WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.AccountAdministratorSid);
        /// <summary>
        /// Url for upload ur key
        /// </summary>
        public static string uploadRSAPrivateKey = null;

        /// <summary>
        /// Path to save all collected data from PC
        /// </summary>
        public static string PathToReportDataDirectory = null;
        /// <summary>
        /// FTP Server user name
        /// </summary>
        public static string FTPUserName = null;
        /// <summary>
        /// FTP Server Password
        /// </summary>
        public static string FTPPassword = null;
        /// <summary>
        /// FTP Host Name
        /// </summary>
        public static string FTPHostName = null;
        /// <summary>
        /// Check Upload 
        /// </summary>
        public static bool UploadSuccess = false;
    }
}
