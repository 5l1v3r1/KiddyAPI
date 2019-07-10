using System;
using System.Diagnostics;
using static KiddyAPI.Injector.NativeAPI;

namespace KiddyAPI.Info
{
    /// <summary>
    /// Доступ к методу получения активного окна
    /// </summary>
    public class CheckWindow
    {
        /// <summary>
        /// Получаем активное окно пользователя
        /// </summary>
        /// <param name="name">Имя программы для отслеживания</param>
        /// <returns>Вернет true, если окно совпадает с name</returns>
        public static bool GetWindow(string name)
        {
            IntPtr hWnd = GetForegroundWindow();
            int pId = 0;
            GetWindowThreadProcessId(hWnd, ref pId);
            var p = Process.GetProcessById(pId);
            return p.MainWindowTitle.Contains(name);

        }
    }
}
