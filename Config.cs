using System;
using System.Collections.Generic;
using Application = System.Windows.Forms.Application;

namespace MousePaw
{
    public class Config
    {

        public Config ()
        {
            ini = new IniFile();

            KeySets = new KeySet[4];
            ReadKeySet(1);
            ReadKeySet(2);
            ReadKeySet(3);

            var index = ReadInteger("KeySets", "CurrentKeySet", 1);
            if (index >= 1 && index < KeySets.Length)
                CurrentKeySet = KeySets[index];

            TrayIconClicks = ReadBoolean("General", "TrayIconClicks", true);
            RunAsAdmin = ReadBoolean("General", "RunAsAdmin", false);
            RunOnStartup = ReadBoolean("General", "RunOnStartup", false);
        }

        public void SetBoolean (string which, bool value)
        {
            if (which == "TrayIconClicks")
                TrayIconClicks = value;
            else if (which == "RunAsAdmin")
                RunAsAdmin = value;
            else if (which == "RunOnStartup")
            {
                RunOnStartup = value;
                CreateOrDeleteShortcut(value);
            }
            else
                throw new ArgumentException();

            var valueStr = value ? "y" : "n";
            ini.Write("General", which, valueStr);

            void CreateOrDeleteShortcut (bool shouldCreate)
            {
                string path =
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                path = $"{path}\\{Application.ProductName}.lnk";

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                if (shouldCreate)
                {
/*                  var shell = CreateObject new IWshRuntimeLibrary.WshShell();
                    var shortcut = (IWshRuntimeLibrary.IWshShortcut)
                                    shell.CreateShortcut(path);*/
                    dynamic shell = Activator.CreateInstance(
                                        Type.GetTypeFromProgID("WScript.Shell"));
                    dynamic shortcut = shell.CreateShortcut(path);
                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.WorkingDirectory = Application.StartupPath;
                    shortcut.Description = Application.ProductName;
                    shortcut.Save();
                }
            }
        }

        public void SetCurrentKeySet (int index)
        {
            if (index >= 1 && index < KeySets.Length)
            {
                ini.Write("KeySets", "CurrentKeySet", $"{index}");
                CurrentKeySet = KeySets[index];
                Program.ReleaseHeldKeys();
            }
            else
                throw new ArgumentException();
        }

        public int GetCurrentKeySetIndex ()
        {
            for (var index = 1; index < KeySets.Length; index++)
            {
                if (CurrentKeySet == KeySets[index])
                    return index;
            }
            throw new ArgumentException();
        }

        public void SetKeyInCurrentKeySet (string which, int code)
        {
            var keyset = CurrentKeySet;

            Check("LeftPress", ref keyset.LeftPress, which, code);
            Check("MiddlePress", ref keyset.MiddlePress, which, code);
            Check("RightPress", ref keyset.RightPress, which, code);
            Check("AnyHold", ref keyset.AnyHold, which, code);
            Check("LeftHold", ref keyset.LeftHold, which, code);
            Check("MiddleHold", ref keyset.MiddleHold, which, code);
            Check("RightHold", ref keyset.RightHold, which, code);

            void Write (string which_, int code_)
            {
                var index = GetCurrentKeySetIndex();
                var extra = code_ == 0 ? "not set" : VirtualKeys.NameForKey(code_);
                ini.Write($"KeySet{index}", which_, $"0x{code_:X2},{extra}");
            }

            void Check (string keyName, ref int keyCode, string nameToSet, int codeToSet)
            {
                if (keyName == nameToSet)
                    Write(keyName, keyCode = codeToSet);
                else if (keyCode == codeToSet)
                    Write(keyName, keyCode = 0);
            }
        }

        public bool IsAnyKeyDefined (int index)
        {
            if (index >= 1 && index < KeySets.Length)
            {
                var keyset = KeySets[index];
                if (keyset.LeftPress != 0 || keyset.MiddlePress != 0
                    || keyset.RightPress != 0 || keyset.AnyHold != 0
                    || keyset.LeftHold != 0 || keyset.MiddleHold != 0
                    || keyset.RightHold != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsAnyKeyDefined ()
        {
            for (var index = 1; index < KeySets.Length; index++)
                if (IsAnyKeyDefined(index))
                    return true;
            return false;
        }

        private int ReadInteger (string section, string key, int defval)
        {
            var s = ini.Read(section, key);
            if (s != null)
            {
                int comma = s.IndexOf(',');
                if (comma != -1)
                    s = s.Substring(0, comma);
                s = s.Trim();
                if (s.StartsWith("0x") || s.StartsWith("0X"))
                {
                    if (int.TryParse(s.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out var n1))
                        return n1;
                }
                if (int.TryParse(s, out var n2))
                    return n2;
            }
            return defval;
        }

        private void ReadKeySet (int which)
        {
            var keyset = new KeySet();
            var section = $"KeySet{which}";
            keyset.LeftPress = ReadInteger(section, "LeftPress", 0);
            keyset.MiddlePress = ReadInteger(section, "MiddlePress", 0);
            keyset.RightPress = ReadInteger(section, "RightPress", 0);
            keyset.AnyHold = ReadInteger(section, "AnyHold", 0);
            keyset.LeftHold = ReadInteger(section, "LeftHold", 0);
            keyset.MiddleHold = ReadInteger(section, "MiddleHold", 0);
            keyset.RightHold = ReadInteger(section, "RightHold", 0);
            KeySets[which] = keyset;
        }

        private bool ReadBoolean (string section, string key, bool defval)
        {
            var s = ini.Read(section, key);
            if (s != null)
            {
                s = s.Trim();
                if (s.Length >= 1)
                {
                    var c = s[0];
                    if (c == 'y' || c == 'Y' || c == '1')
                        return true;
                    if (c == 'n' || c == 'N' || c == '0')
                        return false;
                }
            }
            return defval;
        }

        public KeySet CurrentKeySet;
        public KeySet[] KeySets;

        public bool TrayIconClicks;
        public bool RunAsAdmin;
        public bool RunOnStartup;

        private readonly IniFile ini;
    }

    //
    // KeySet
    //

    public class KeySet
    {
        public int LeftPress;
        public int MiddlePress;
        public int RightPress;
        public int AnyHold;
        public int LeftHold;
        public int MiddleHold;
        public int RightHold;
    }

    //
    // virtual key names
    //

    public static class VirtualKeys
    {
        public static string NameForKey (int vk)
            => VirtualKeyName.TryGetValue(vk, out var name)
                    ? name : $"(unknown 0x{vk:X})";

        public static Dictionary<int, string> VirtualKeyName = new Dictionary<int, string>()
        {

            { 0x00, "(not set)" },
            { 0x08, "Backspace" },
            { 0x09, "Tab" },
            { 0x0C, "Clear" },
            { 0x0D, "Enter" },

            { 0x10, "Shift" },
            { 0x11, "Ctrl" },
            { 0x12, "Alt" },
            { 0x13, "Pause" },
            { 0x14, "Caps Lock" },
            { 0x15, "IME Kana" },
            { 0x16, "IME On" },
            { 0x17, "IME Junja" },
            { 0x18, "IME final" },
            { 0x19, "IME Hanja" },
            { 0x1A, "IME Off" },
            { 0x1B, "Escape" },
            { 0x1C, "IME convert" },
            { 0x1D, "IME nonconvert" },
            { 0x1E, "IME accept" },
            { 0x1F, "IME mode change" },

            { 0x20, "Space" },
            { 0x21, "Page Up" },
            { 0x22, "Page Down" },
            { 0x23, "End" },
            { 0x24, "Home" },
            { 0x25, "Left Arrow" },
            { 0x26, "Up Arrow" },
            { 0x27, "Right Arrow" },
            { 0x28, "Down Arrow" },
            { 0x29, "Select" },
            { 0x2A, "Print" },
            { 0x2B, "Execute" },
            { 0x2C, "Print Screen" },
            { 0x2D, "Insert" },
            { 0x2E, "Delete" },
            { 0x2F, "Help" },

            { 0x30, "Num Row 0" },
            { 0x31, "Num Row 1" },
            { 0x32, "Num Row 2" },
            { 0x33, "Num Row 3" },
            { 0x34, "Num Row 4" },
            { 0x35, "Num Row 5" },
            { 0x36, "Num Row 6" },
            { 0x37, "Num Row 7" },
            { 0x38, "Num Row 8" },
            { 0x39, "Num Row 9" },

            { 0x41, "A" },
            { 0x42, "B" },
            { 0x43, "C" },
            { 0x44, "D" },
            { 0x45, "E" },
            { 0x46, "F" },
            { 0x47, "G" },
            { 0x48, "H" },
            { 0x49, "I" },
            { 0x4A, "J" },
            { 0x4B, "K" },
            { 0x4C, "L" },
            { 0x4D, "M" },
            { 0x4E, "N" },
            { 0x4F, "O" },
            { 0x50, "P" },
            { 0x51, "Q" },
            { 0x52, "R" },
            { 0x53, "S" },
            { 0x54, "T" },
            { 0x55, "U" },
            { 0x56, "V" },
            { 0x57, "W" },
            { 0x58, "X" },
            { 0x59, "Y" },
            { 0x5A, "Z" },

            { 0x5B, "Left Win key" },
            { 0x5C, "Right Win key" },
            { 0x5D, "Apps key" },
            { 0x5F, "Sleep key" },

            { 0x60, "Num Pad 0" },
            { 0x61, "Num Pad 1" },
            { 0x62, "Num Pad 2" },
            { 0x63, "Num Pad 3" },
            { 0x64, "Num Pad 4" },
            { 0x65, "Num Pad 5" },
            { 0x66, "Num Pad 6" },
            { 0x67, "Num Pad 7" },
            { 0x68, "Num Pad 8" },
            { 0x69, "Num Pad 9" },

            { 0x6A, "Num Pad *" },
            { 0x6B, "Num Pad +" },
            { 0x6D, "Num Pad -" },
            { 0x6E, "Num Pad Dot" },
            { 0x6F, "Num Pad /" },

            { 0x70, "F1" },
            { 0x71, "F2" },
            { 0x72, "F3" },
            { 0x73, "F4" },
            { 0x74, "F5" },
            { 0x75, "F6" },
            { 0x76, "F7" },
            { 0x77, "F8" },
            { 0x78, "F9" },
            { 0x79, "F10" },
            { 0x7A, "F11" },
            { 0x7B, "F12" },
            { 0x7C, "F13" },
            { 0x7D, "F14" },
            { 0x7E, "F15" },
            { 0x7F, "F16" },
            { 0x80, "F17" },
            { 0x81, "F18" },
            { 0x82, "F19" },
            { 0x83, "F20" },
            { 0x84, "F21" },
            { 0x85, "F22" },
            { 0x86, "F23" },
            { 0x87, "F24" },

            { 0x90, "Num Lock" },
            { 0x91, "Scroll Lock" },

            { 0xA0, "Left Shift" },
            { 0xA1, "Right Shift" },
            { 0xA2, "Left Control" },
            { 0xA3, "Right Control" },
            { 0xA4, "Left Alt" },
            { 0xA5, "Right Alt" },

            { 0xA6, "Browser Back" },
            { 0xA7, "Browser Forward" },
            { 0xA8, "Browser Refresh" },
            { 0xA9, "Browser Stop" },
            { 0xAA, "Browser Search" },
            { 0xAB, "Browser Favorites" },
            { 0xAC, "Browser Home" },

            { 0xAD, "Volume Mute" },
            { 0xAE, "Volume Down" },
            { 0xAF, "Volume Up" },
            { 0xB0, "Next Track" },
            { 0xB1, "Previous Track" },
            { 0xB2, "Stop Media" },
            { 0xB3, "Pause Media" },

            { 0xB4, "Start Mail" },
            { 0xB5, "Select Media" },
            { 0xB6, "Start App 1" },
            { 0xB7, "Start App 2" },

            { 0xBA, "; :" },
            { 0xBB, "+ =" },
            { 0xBC, ", <" },
            { 0xBD, "- _" },
            { 0xBE, ". >" },
            { 0xBF, "/ ?" },
            { 0xC0, "` ~" },
            { 0xDB, "[ {" },
            { 0xDC, "\\ |" },
            { 0xDD, "] }" },
            { 0xDE, "' \"" },
        };
    }

}
