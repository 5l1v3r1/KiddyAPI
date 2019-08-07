/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using static KiddyAPI.Injector.NativeAPI;

namespace KiddyAPI.Injector
{
    public class Injector
    {
        private static bool is64BitProcess = (IntPtr.Size == 8);
        private static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();
        private static bool isInjected = false;

        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        private static int Inject(string pathDLL, Process process) //Приватный метод, который выполняет инжект, вынес для удобства
        {
            var targetProc = process;
            string dllName = pathDLL;
            //Получаем хэндл процесса, с нужными правами. В некоторых ситуациях, скорее всего
            //Необходим будет запуск от админа, так как Винда блокирует операцию и на Си функция бы возвращала 0
            //А в шарпе будет выкидывать исключение Недостаток прав
            var processHandle = OpenProcess(
                ProcessAccessFlags.PROCESS_CREATE_THREAD | ProcessAccessFlags.PROCESS_QUERY_INFORMATION |
                ProcessAccessFlags.PROCESS_VM_OPERATION | ProcessAccessFlags.PROCESS_VM_WRITE |
                ProcessAccessFlags.PROCESS_VM_WRITE | ProcessAccessFlags.PROCESS_VM_READ, false, targetProc.Id);
            IntPtr libAddres = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            //Собственно, выделяем необходимую память
            IntPtr allocMemoryAdress = VirtualAllocEx(processHandle, IntPtr.Zero,
                (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            UIntPtr bytesWritten;
            //Записываем
            WriteProcessMemory(processHandle, allocMemoryAdress, Encoding.Default.GetBytes(dllName),
                (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
            CreateRemoteThread(processHandle, IntPtr.Zero, 0, libAddres, allocMemoryAdress, 0, IntPtr.Zero);
            return 0;
        }
        /// <summary>
        /// Inject DLL to Process
        /// </summary>
        /// <param name="pathDll">Path to DLL</param>
        /// <param name="process">Process name</param>
        public static void Execute(string pathDll, Process process)
        {
            string stockDll = string.Empty;
            Inject(pathDll, process);
            isInjected = true;

        }
        /// <summary>
        /// 64Bit or not
        /// </summary>
        /// <returns></returns>
        private static bool InternalCheckIsWow64()
        {

            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    return IsWow64Process(p.Handle, out var retVal) && retVal;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
