using System;
using System.Windows.Forms;

namespace MousePaw
{
    static class Program
    {

        [STAThread]
        static void Main ()
        {
            using (new System.Threading.Mutex(
                    true, $"com.spaceflint.{Application.ProductName}",
                    out var createdNew))
            {
                if (!createdNew)
                    return;

                if (Environment.OSVersion.Version.Major >= 6)
                    Win32.SetProcessDPIAware();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Globals.Config = new Config();
                if (ShouldRunAsAdmin())
                    return;

                new KeyboardHook(MyKeyboardCallback);

                var form = new Form1();
                InitNotifyIcon(form);
                Application.Run(form);

                Globals.NotifyIcon.Visible = false;
            }
        }

        //
        // InitNotifyIcon
        //

        private static void InitNotifyIcon (Form1 form)
        {
            var NotifyIcon = Globals.NotifyIcon = new NotifyIcon();

            NotifyIcon.ContextMenuStrip = new ContextMenuStrip();
            NotifyIcon.ContextMenuStrip.Items.Add("", null, EnableDisable_Click);

            NotifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            NotifyIcon.ContextMenuStrip.Items.Add("Settings", null,
                (sender, args) => { form.RestoreAndShow(); });

            NotifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            Globals.KeySetMenuItem =
                NotifyIcon.ContextMenuStrip.Items.Add("Key Set", null, null)
                    as ToolStripMenuItem;

            for (var index = 1; index <= 3; index++)
            {
                var indexCopy = index;
                Globals.KeySetMenuItem.DropDownItems.Add($"Key Set #{index}", null,
                    (sender, args) => Globals.Config.SetCurrentKeySet(indexCopy));
            }

            Globals.KeySetMenuItem.DropDownOpened += (sender, args) =>
            {
                var parent = sender as ToolStripMenuItem;
                var items = parent.DropDownItems;
                for (var index = 0; index < 3; )
                {
                    var child = items[index++] as ToolStripMenuItem;
                    child.Checked = (Globals.Config.CurrentKeySet ==
                                        Globals.Config.KeySets[index]);
                }
            };

            NotifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            NotifyIcon.ContextMenuStrip.Items.Add("Exit", null,
                (sender, args) => { Application.Exit(); });

            // must be called after setting up context menu
            SetNotifyIconAndText();

            NotifyIcon.Click += EnableDisable_Click;
            NotifyIcon.Visible = true;

            Globals.NotifyTimer = new System.Threading.Timer(
                MyTimerCallback, new int[2], 750, 750);
        }

        private static void SetNotifyIconAndText ()
        {
            var NotifyIcon = Globals.NotifyIcon;
            NotifyIcon.Text = $"{Application.ProductName} - ";
            if (Globals.Disabled)
            {
                NotifyIcon.Icon = Properties.Resources.DisableIcon;
                NotifyIcon.Text += "suspended";
                NotifyIcon.ContextMenuStrip.Items[0].Text = "Enable";
            }
            else
            {
                NotifyIcon.Icon = Properties.Resources.EnableIcon;
                NotifyIcon.Text += "active";
                NotifyIcon.ContextMenuStrip.Items[0].Text = "Disable";
            }
        }

        //
        // MyKeyboardCallback
        //

        static bool MyKeyboardCallback (int vk, bool down)
        {
            if (vk == 0)
                return false;
            if (Globals.SavedVirtualKey == -1 && down)
            {
                Globals.SavedVirtualKey = vk;
                return true;
            }
            if (Globals.Disabled)
                return false;

            var keyset = Globals.Config.CurrentKeySet;
            var isAny = vk == keyset.AnyHold;
            bool retval = false;

            if (vk == keyset.LeftPress || vk == keyset.LeftHold || isAny)
            {
                Check(vk == keyset.LeftPress, isAny, down, ref Globals.LeftButtonFlags,
                      Win32.MOUSEEVENTF_LEFTDOWN, Win32.MOUSEEVENTF_LEFTUP);
                retval = true;
            }

            if (vk == keyset.MiddlePress || vk == keyset.MiddleHold || isAny)
            {
                Check(vk == keyset.MiddlePress, isAny, down, ref Globals.MiddleButtonFlags,
                      Win32.MOUSEEVENTF_MIDDLEDOWN, Win32.MOUSEEVENTF_MIDDLEUP);
                retval = true;
            }

            if (vk == keyset.RightPress || vk == keyset.RightHold || isAny)
            {
                Check(vk == keyset.RightPress, isAny, down, ref Globals.RightButtonFlags,
                      Win32.MOUSEEVENTF_RIGHTDOWN, Win32.MOUSEEVENTF_RIGHTUP);
                retval = true;
            }

            return retval;

            // parameters for Check ()
            // isPressKey - if this is xxxPress key, otherwise xxxHold key
            // isAnyKey - if this is exactly the AnyHoldKey
            // isKeyDown - if this is a key down event, otherwise key up
            // flags - button flags
            // evtDown - event code for button down
            // evtUp - event code for button up

            // as a reminder:  the xxxPress key simulates a mouse button
            // in a direct way.  the AnyHold key 'locks' any simulated mouse
            // buttons that are 'held', so the corresponding xxxPress keys
            // can be released without releasing the mouse button.  and the
            // other xxxHold keys 'locks' a mouse button in a 'held' state
            // even if the button is not already 'held'.

            void Check (bool isPressKey, bool isAnyKey, bool isKeyDown,
                        ref int flags, uint evtDown, uint evtUp)
            {
                if (isPressKey)    // the xxxPress key
                {
                    bool isAlreadyDown = (flags & Globals.BUTTON_IS_DOWN) != 0;
                    if (isKeyDown) // the xxxPress key is pressed
                    {
                        if ((flags & Globals.BUTTON_IS_HELD) != 0)
                        {
                            // if the xxxPress key is pressed while the button
                            // is simulated as 'held' due to an xxxHold key:
                            // simulate button release, reset button flags.
                            Win32.mouse_event(evtUp, 0, 0, 0, 0);
                            flags = 0;
                        }
                        else if (!isAlreadyDown)
                        {
                            // if the xxxPress key is pressed when the button
                            // is is initial state, i.e. it is not down:
                            //
                            // simulate button press, set flags to indicate
                            // button is down, set flag to simulate button
                            // release upon release of the xxxPress key.
                            Win32.mouse_event(evtDown, 0, 0, 0, 0);
                            flags = Globals.BUTTON_IS_DOWN
                                  | Globals.PRESS_KEY_IS_DOWN
                                  | Globals.NOTIFY_ON_KEY_UP;
                        }
                    }
                    else if (isAlreadyDown) // the xxxPress key is released
                    {
                        // when the xxxPress key is released, a flag indicates
                        // if we should simulate a button release.  normally,
                        // the flag is set (see above), unless the button is
                        // simulated as 'held' due to an xxxHold key (see below).
                        if ((flags & Globals.NOTIFY_ON_KEY_UP) != 0)
                        {
                            Win32.mouse_event(evtUp, 0, 0, 0, 0);
                            flags &= ~(Globals.BUTTON_IS_DOWN
                                     | Globals.NOTIFY_ON_KEY_UP);
                        }
                        flags &= ~Globals.PRESS_KEY_IS_DOWN;
                    }
                }
                else if (isAnyKey) // the AnyHold key
                {
                    if (isKeyDown) // the AnyHold key is pressed
                    {
                        if ((flags & Globals.ANY_KEY_IS_DOWN) == 0)
                        {
                            flags |= Globals.ANY_KEY_IS_DOWN;

                            // if the button is down, 'lock' it as simulated
                            // 'held', by clearing the flag to issue a button
                            // release upon xxxPress key release (see above)
                            if ((flags & Globals.BUTTON_IS_DOWN) != 0)
                            {
                                flags &= ~Globals.NOTIFY_ON_KEY_UP;
                                flags |= Globals.BUTTON_IS_HELD;
                            }
                        }
                    }
                    else
                    {
                        // the AnyHold key is released:  clear ignore flag
                        flags &= ~Globals.ANY_KEY_IS_DOWN;
                    }
                }
                else if (isKeyDown) // the xxxHold key is pressed
                {
                    if ((flags & Globals.HOLD_KEY_IS_DOWN) == 0)
                    {
                        flags |= Globals.HOLD_KEY_IS_DOWN;

                        if ((flags & Globals.BUTTON_IS_HELD) != 0)
                        {
                            // if the button is already simulated 'held'
                            // when the xxxHold key is pressed:
                            // simulate button release, reset button flags.
                            Win32.mouse_event(evtUp, 0, 0, 0, 0);
                            flags = 0;
                        }
                        else
                        {
                            // if the xxxHold key is pressed when the mouse
                            // button is in initial state:  simulate button
                            // press, and flag the button as simulated 'held'.
                            Win32.mouse_event(evtDown, 0, 0, 0, 0);
                            flags = Globals.BUTTON_IS_DOWN
                                  | Globals.BUTTON_IS_HELD
                                  | Globals.HOLD_KEY_IS_DOWN;
                        }
                    }
                }
                else
                {
                    // the xxxHold key is released:  clear ignore flag
                    flags &= ~Globals.HOLD_KEY_IS_DOWN;
                }
            }
        }

        //
        // MyTimerCallback
        //

        private static void MyTimerCallback (object state_)
        {
            var state = (int[])state_;

            if (0 != (Globals.BUTTON_IS_HELD & (Globals.LeftButtonFlags
                                              | Globals.MiddleButtonFlags
                                              | Globals.RightButtonFlags)))
            {
                // mark the time when the first mouse button is held
                var now = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerSecond);
                if (state[0] == 0)
                    state[0] = now;

                if (now - state[0] > 7)
                {
                    // start blinking the icon when any mouse button is
                    // simulated as 'held' for longer than seven seconds
                    Globals.NotifyIcon.Icon = ((++state[1]) & 1) != 0
                             ? Properties.Resources.WarningIcon
                             : Properties.Resources.EnableIcon;
                }
            }
            else if (state[1] != 0)
            {
                state[0] = state[1] = 0;
                Globals.NotifyIcon.Icon = Globals.Disabled
                                        ? Properties.Resources.DisableIcon
                                        : Properties.Resources.EnableIcon;
            }
        }

        //
        // ReleaseHeldKeys
        //

        public static void ReleaseHeldKeys ()
        {
            if (0 != (Globals.LeftButtonFlags &
                        (Globals.BUTTON_IS_DOWN | Globals.BUTTON_IS_HELD)))
            {
                Win32.mouse_event(Win32.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Globals.LeftButtonFlags = 0;
            }

            if (0 != (Globals.MiddleButtonFlags &
                        (Globals.BUTTON_IS_DOWN | Globals.BUTTON_IS_HELD)))
            {
                Win32.mouse_event(Win32.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                Globals.MiddleButtonFlags = 0;
            }

            if (0 != (Globals.RightButtonFlags &
                        (Globals.BUTTON_IS_DOWN | Globals.BUTTON_IS_HELD)))
            {
                Win32.mouse_event(Win32.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                Globals.RightButtonFlags = 0;
            }
        }

        //
        //
        //

        private static void EnableDisable_Click (object sender, EventArgs e)
        {
            if (sender is NotifyIcon)
            {
                if (!Globals.Config.TrayIconClicks)
                    return;
                if (e is MouseEventArgs me && me.Button != MouseButtons.Left)
                    return;
            }
            Globals.Disabled = !Globals.Disabled;
            SetNotifyIconAndText();
        }

        //
        // ShouldRunAsAdmin
        //

        private static bool ShouldRunAsAdmin ()
        {
            if (!Globals.Config.RunAsAdmin)
                return false;

            // check if already administrator
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                return false;

            try
            {
                (new System.Diagnostics.Process()
                {
                    StartInfo =
                    {
                        FileName = Application.ExecutablePath,
                        UseShellExecute = true,
                        Verb = "runas"
                    }
                }).Start();
                return true;
            }
            catch { }

            return false;
        }

    }

    //
    //
    //

    public static class Globals
    {

        public static Config Config;
        public static NotifyIcon NotifyIcon;
        public static ToolStripMenuItem KeySetMenuItem;
        public static System.Threading.Timer NotifyTimer;

        // if set to -1, records the next virtual key pressed
        public static int SavedVirtualKey;

        // if true, keyboard hook does not simulate mouse gestures
        public static bool Disabled;

        // flags tracking key and button state
        public static int LeftButtonFlags;
        public static int MiddleButtonFlags;
        public static int RightButtonFlags;
        // flag constants
        public const int BUTTON_IS_DOWN = 1;
        public const int BUTTON_IS_HELD = 2;
        public const int PRESS_KEY_IS_DOWN = 4;
        public const int HOLD_KEY_IS_DOWN = 8;
        public const int ANY_KEY_IS_DOWN = 16;
        public const int NOTIFY_ON_KEY_UP = 32;
    }

}
