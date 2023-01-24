using System;
using System.Windows.Forms;

namespace MousePaw
{
    public partial class Form1 : Form
    {
        public bool Initializing;
        private readonly Timer FlashTimer;
        private string FlashSaveText;

        public Form1 ()
        {
            Initializing = true;
            InitializeComponent();
            Icon = Properties.Resources.EnableIcon;

            CheckTrayIconClicks.CheckState = Globals.Config.TrayIconClicks
                ? CheckState.Checked : CheckState.Unchecked;

            CheckRunAsAdmin.CheckState = Globals.Config.RunAsAdmin
                ? CheckState.Checked : CheckState.Unchecked;

            CheckRunOnStartup.CheckState = Globals.Config.RunOnStartup
                ? CheckState.Checked : CheckState.Unchecked;

            Initializing = false;
            if (Globals.Config.IsAnyKeyDefined())
            {
                MinimizeAndHide();
            }
            else
            {
                FlashSaveText = TabKeys.Text;
                var texts = new string[] { "click here", "to set keys",
                                           "--*--*--*--",
                                           "CLICK HERE", "TO SET KEYS",
                                           "*--*--*--*" };
                int index = 0;

                FlashTimer = new Timer() { Interval = 750 };
                FlashTimer.Tick += (sender, args) =>
                    TabKeys.Text = texts[(index++) % texts.Length];
                FlashTimer.Start();
            }
        }

        private void MinimizeAndHide ()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            if (Globals.KeySetMenuItem != null)
                Globals.KeySetMenuItem.Enabled = true;
        }

        public void RestoreAndShow ()
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            BringToFront();

            if (ReferenceEquals(tabControl1.SelectedTab, TabKeys)
                                        && Globals.KeySetMenuItem != null)
            {
                Globals.KeySetMenuItem.Enabled = false;
                KeySetCombo.SelectedIndex =
                    Globals.Config.GetCurrentKeySetIndex() - 1;
            }
        }

        private void Form1_FormClosing (object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                MinimizeAndHide();

                if (Globals.KeySetMenuItem != null)
                    Globals.KeySetMenuItem.Enabled = true;
            }
        }

        private void UpdateButtons ()
        {
            var keyset = Globals.Config.CurrentKeySet;
            LeftPress.Text = VirtualKeys.NameForKey(keyset.LeftPress);
            MiddlePress.Text = VirtualKeys.NameForKey(keyset.MiddlePress);
            RightPress.Text = VirtualKeys.NameForKey(keyset.RightPress);
            AnyHold.Text = VirtualKeys.NameForKey(keyset.AnyHold);
            LeftHold.Text = VirtualKeys.NameForKey(keyset.LeftHold);
            MiddleHold.Text = VirtualKeys.NameForKey(keyset.MiddleHold);
            RightHold.Text = VirtualKeys.NameForKey(keyset.RightHold);
        }

        private void UpdateKey (string keyName)
        {
            var keyCode = KeyForm.RecordVirtualKey(this);
            Globals.Config.SetKeyInCurrentKeySet(keyName, keyCode);
            UpdateButtons();
        }

        private void TabKeys_Enter (object sender, EventArgs e)
        {
            if (Globals.KeySetMenuItem != null)
                Globals.KeySetMenuItem.Enabled = false;

            if (FlashSaveText != null)
            {
                FlashTimer.Stop();
                TabKeys.Text = FlashSaveText;
                FlashSaveText = null;
            }

            KeySetCombo.SelectedIndex =
                Globals.Config.GetCurrentKeySetIndex() - 1;
        }

        private void KeySetCombo_SelectedIndexChanged (object sender, EventArgs e)
        {
            Globals.Config.SetCurrentKeySet(KeySetCombo.SelectedIndex + 1);
            UpdateButtons();
        }

        private void LeftPress_Click (object sender, EventArgs e)
            => UpdateKey("LeftPress");

        private void MiddlePress_Click (object sender, EventArgs e)
            => UpdateKey("MiddlePress");

        private void RightPress_Click (object sender, EventArgs e)
            => UpdateKey("RightPress");

        private void AnyHold_Click (object sender, EventArgs e)
            => UpdateKey("AnyHold");

        private void LeftHold_Click (object sender, EventArgs e)
            => UpdateKey("LeftHold");

        private void MiddleHold_Click (object sender, EventArgs e)
            => UpdateKey("MiddleHold");

        private void RightHold_Click (object sender, EventArgs e)
            => UpdateKey("RightHold");

        private void CheckTrayIconClicks_CheckedChanged (object sender, EventArgs e)
        {
            if (!Initializing)
                Globals.Config.SetBoolean("TrayIconClicks", CheckTrayIconClicks.Checked);
        }

        private void CheckRunAsAdmin_CheckedChanged (object sender, EventArgs e)
        {
            if (!Initializing)
                Globals.Config.SetBoolean("RunAsAdmin", CheckRunAsAdmin.Checked);
        }

        private void CheckRunOnStartup_CheckedChanged (object sender, EventArgs e)
        {
            if (!Initializing)
                Globals.Config.SetBoolean("RunOnStartup", CheckRunOnStartup.Checked);
        }

        private void WebLabel_LinkClicked (object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://github.com/spaceflint7/MousePaw",
                    UseShellExecute = true
                });
        }
    }

}
