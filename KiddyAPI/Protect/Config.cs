/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
using System.Security.Principal;

namespace KiddyAPI.Protect
{
    /// <summary>
    /// Конфигурация различных модулей
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Поле, которое вернет true, если программа запущена под администратором, иначе false
        /// </summary>
        public static bool IsAdmin => WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.AccountAdministratorSid);

        public static string uploadRSAPrivateKey = "https://example.com/";
    }
}
