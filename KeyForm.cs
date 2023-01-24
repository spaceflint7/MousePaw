using System;
using System.Windows.Forms;

namespace MousePaw
{
    public partial class KeyForm : Form
    {
        public static int RecordVirtualKey (Form parent)
        {
            Globals.SavedVirtualKey = -1;
            (new KeyForm()).ShowDialog(parent);
            var key = Globals.SavedVirtualKey;
            Globals.SavedVirtualKey = 0;
            if (key == 0x1B || key < 0)
                key = 0;
            return key;
        }

        public KeyForm ()
        {
            InitializeComponent();
            Timer.Interval = 10;
            Timer.Start();
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void Timer_Tick (object sender, EventArgs e)
        {
            // when a key press is detected (FirstVirtualKey != 0),
            // change the label text, and wait a short while before
            // closing the dialog box, to prevent an accidental
            // second click on the originating button
            if (countdown > 0) {
                --countdown;
                if (countdown == 0) {
                    Timer.Stop();
                    this.Close();
                }
                return;
            }
            if (Globals.SavedVirtualKey != -1) {
                if (Globals.SavedVirtualKey == 0x1B) // Escape
                    this.Close();
                else {
                    countdown = 15;
                    label1.Text = VirtualKeys.NameForKey(Globals.SavedVirtualKey);
                }
            }
        }

        private int countdown;
    }
}