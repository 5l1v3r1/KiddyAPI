/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
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

        public static string uploadRSAPrivateKey = null;
    }
}
