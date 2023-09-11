using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MousePaw
{
    static class Win32
    {
        //
        // dpi
        //

        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware ();

        //
        // mouse_event
        //

        [DllImport("user32.dll")]
        public static extern void mouse_event (uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;

        public const int MOUSEEVENTF_LEFTUP = 0x0004;

        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;

        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;

        public const int MOUSEEVENTF_MOVE = 0x0001;

        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;

        public const int MOUSEEVENTF_RIGHTUP = 0x0010;

        public const int MOUSEEVENTF_WHEEL = 0x0800;

        public const int MOUSEEVENTF_XDOWN = 0x0080;

        public const int MOUSEEVENTF_XUP = 0x0100;

        //
        // keyboard hook
        //
        // https://stackoverflow.com/questions/604410
        // https://stackoverflow.com/questions/46013287
        // https://stackoverflow.com/questions/61567576
        // https://learn.microsoft.com/en-us/archive/msdn-magazine/2002/october/cutting-edge-windows-hooks-in-the-net-framework
        //

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx (int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx (IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx (IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle (string lpModuleName);

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;

        public const int WH_KEYBOARD_LL = 13;

        public delegate IntPtr LowLevelKeyboardProc (int nCode, IntPtr wParam, IntPtr lParam);
    }

    //
    // KeyboardHook
    //

    class KeyboardHook : IDisposable
    {
        public delegate bool Callback (int vk, bool down);

        //
        // constructor
        //

        public KeyboardHook (Callback cb)
        {
            this.cb = cb;

            using (var process = System.Diagnostics.Process.GetCurrentProcess())
            {
                using (var module = process.MainModule)
                {
                    // prevent the closure from being garbage collected
                    this.kbdproc = InternalCallback;

                    this.id = Win32.SetWindowsHookEx(
                            Win32.WH_KEYBOARD_LL, this.kbdproc,
                            Win32.GetModuleHandle(module.ModuleName), 0);

                    AppDomain.CurrentDomain.ProcessExit += (sender, args) => Dispose();
                }
            }
        }

        //
        // Dispose
        //

        public void Dispose ()
        {
            if (this.id != IntPtr0)
            {
                Win32.UnhookWindowsHookEx(this.id);
                this.id = IntPtr0;
            }
        }

        //
        // InternalCallback
        //

        private IntPtr InternalCallback (int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var down = wParam == (IntPtr)Win32.WM_KEYDOWN
                     || wParam == (IntPtr)Win32.WM_SYSKEYDOWN;
                var up = wParam == (IntPtr)Win32.WM_KEYUP
                     || wParam == (IntPtr)Win32.WM_SYSKEYUP;

                if (down || up)
                {
                    var vk = Marshal.ReadInt32(lParam);
                    if (this.cb(vk, down))
                    {
                        // callback indicates it handled the event
                        return IntPtr1;
                    }
                }
            }

            return Win32.CallNextHookEx(this.id, nCode, wParam, lParam);
        }

        //
        //
        //

        private IntPtr id;
        private readonly Callback cb;
        private readonly Win32.LowLevelKeyboardProc kbdproc;

        private static readonly IntPtr IntPtr0 = (IntPtr)0;
        private static readonly IntPtr IntPtr1 = (IntPtr)1;
    }

    //
    // IniFile
    //
    // https://stackoverflow.com/questions/217902
    //

    class IniFile
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool WritePrivateProfileString (string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString (string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile ()
        {
            using (var process = System.Diagnostics.Process.GetCurrentProcess())
            {
                using (var module = process.MainModule)
                {
                    var dir = System.IO.Path.GetDirectoryName(module.FileName);
                    var name = System.IO.Path.GetFileNameWithoutExtension(module.FileName);
                    path = $"{dir}\\{name}.ini";
                }
            }
        }

        public string Read (string section, string key)
        {
            var buf = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", buf, 255, path);
            return buf.ToString();
        }

        public void Write (string section, string key, string value)
        {
            bool ok = WritePrivateProfileString(section, key, value, path);
            if (!ok) {
                int errCode = Marshal.GetLastWin32Error();
                var errText = Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()).Message;
                var line1 = "ERROR! Failed to update ";
                var line2 = "configuration in file:\n\n" + path;
                var line3 = $"\n\n{errText} (Win32 error {errCode})";
                var line4 = "\n\nTry moving program EXE to another folder.";
                System.Windows.Forms.MessageBox.Show(line1 + line2 + line3 + line4);
            }
        }

        public void DeleteKey (string section, string key)
          => Write(section, key, null);

        public void DeleteSection (string section)
          => Write(section, null, null);

        public bool KeyExists (string section, string key)
          => Read(section, key).Length > 0;

        private readonly string path;
    }

}
