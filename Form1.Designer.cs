
namespace MousePaw
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LeftPress = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MiddlePress = new System.Windows.Forms.Button();
            this.RightPress = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AnyHold = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.LeftHold = new System.Windows.Forms.Button();
            this.MiddleHold = new System.Windows.Forms.Button();
            this.RightHold = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.KeySetCombo = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabGeneral = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.CheckRunOnStartup = new System.Windows.Forms.CheckBox();
            this.CheckRunAsAdmin = new System.Windows.Forms.CheckBox();
            this.CheckTrayIconClicks = new System.Windows.Forms.CheckBox();
            this.TabKeys = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TabAbout = new System.Windows.Forms.TabPage();
            this.WebLabel = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.TabGeneral.SuspendLayout();
            this.TabKeys.SuspendLayout();
            this.TabAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPress
            // 
            this.LeftPress.Location = new System.Drawing.Point(18, 123);
            this.LeftPress.Margin = new System.Windows.Forms.Padding(5);
            this.LeftPress.Name = "LeftPress";
            this.LeftPress.Size = new System.Drawing.Size(150, 50);
            this.LeftPress.TabIndex = 3;
            this.LeftPress.UseMnemonic = false;
            this.LeftPress.UseVisualStyleBackColor = true;
            this.LeftPress.Click += new System.EventHandler(this.LeftPress_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(620, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "The keys below correspond to the left, middle and right mouse buttons, respective" +
    "ly.  Press or hold a key to simulate the corresponding mouse button action.";
            // 
            // MiddlePress
            // 
            this.MiddlePress.Location = new System.Drawing.Point(207, 123);
            this.MiddlePress.Margin = new System.Windows.Forms.Padding(5);
            this.MiddlePress.Name = "MiddlePress";
            this.MiddlePress.Size = new System.Drawing.Size(150, 50);
            this.MiddlePress.TabIndex = 4;
            this.MiddlePress.UseMnemonic = false;
            this.MiddlePress.UseVisualStyleBackColor = true;
            this.MiddlePress.Click += new System.EventHandler(this.MiddlePress_Click);
            // 
            // RightPress
            // 
            this.RightPress.Location = new System.Drawing.Point(399, 123);
            this.RightPress.Margin = new System.Windows.Forms.Padding(5);
            this.RightPress.Name = "RightPress";
            this.RightPress.Size = new System.Drawing.Size(150, 50);
            this.RightPress.TabIndex = 5;
            this.RightPress.UseMnemonic = false;
            this.RightPress.UseVisualStyleBackColor = true;
            this.RightPress.Click += new System.EventHandler(this.RightPress_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 355);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(615, 75);
            this.label2.TabIndex = 4;
            this.label2.Text = "Alternatively, press any of the three keys below to simulate holding down the cor" +
    "responding mouse button.  Press the key a second time to simulate releasing the " +
    "mouse button.";
            // 
            // AnyHold
            // 
            this.AnyHold.Location = new System.Drawing.Point(18, 279);
            this.AnyHold.Margin = new System.Windows.Forms.Padding(5);
            this.AnyHold.Name = "AnyHold";
            this.AnyHold.Size = new System.Drawing.Size(150, 50);
            this.AnyHold.TabIndex = 6;
            this.AnyHold.UseMnemonic = false;
            this.AnyHold.UseVisualStyleBackColor = true;
            this.AnyHold.Click += new System.EventHandler(this.AnyHold_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 196);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(603, 75);
            this.label3.TabIndex = 6;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // LeftHold
            // 
            this.LeftHold.Location = new System.Drawing.Point(18, 435);
            this.LeftHold.Margin = new System.Windows.Forms.Padding(5);
            this.LeftHold.Name = "LeftHold";
            this.LeftHold.Size = new System.Drawing.Size(150, 50);
            this.LeftHold.TabIndex = 7;
            this.LeftHold.UseMnemonic = false;
            this.LeftHold.UseVisualStyleBackColor = true;
            this.LeftHold.Click += new System.EventHandler(this.LeftHold_Click);
            // 
            // MiddleHold
            // 
            this.MiddleHold.Location = new System.Drawing.Point(207, 435);
            this.MiddleHold.Margin = new System.Windows.Forms.Padding(5);
            this.MiddleHold.Name = "MiddleHold";
            this.MiddleHold.Size = new System.Drawing.Size(150, 50);
            this.MiddleHold.TabIndex = 8;
            this.MiddleHold.UseMnemonic = false;
            this.MiddleHold.UseVisualStyleBackColor = true;
            this.MiddleHold.Click += new System.EventHandler(this.MiddleHold_Click);
            // 
            // RightHold
            // 
            this.RightHold.Location = new System.Drawing.Point(399, 435);
            this.RightHold.Margin = new System.Windows.Forms.Padding(5);
            this.RightHold.Name = "RightHold";
            this.RightHold.Size = new System.Drawing.Size(150, 50);
            this.RightHold.TabIndex = 9;
            this.RightHold.UseMnemonic = false;
            this.RightHold.UseVisualStyleBackColor = true;
            this.RightHold.Click += new System.EventHandler(this.RightHold_Click);
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Location = new System.Drawing.Point(14, 489);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(658, 101);
            this.label4.TabIndex = 10;
            // 
            // KeySetCombo
            // 
            this.KeySetCombo.AllowDrop = true;
            this.KeySetCombo.DropDownHeight = 120;
            this.KeySetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeySetCombo.FormattingEnabled = true;
            this.KeySetCombo.IntegralHeight = false;
            this.KeySetCombo.ItemHeight = 20;
            this.KeySetCombo.Items.AddRange(new object[] {
            "Key Set #1",
            "Key Set #2",
            "Key Set #3"});
            this.KeySetCombo.Location = new System.Drawing.Point(207, 18);
            this.KeySetCombo.Margin = new System.Windows.Forms.Padding(5);
            this.KeySetCombo.Name = "KeySetCombo";
            this.KeySetCombo.Size = new System.Drawing.Size(342, 28);
            this.KeySetCombo.TabIndex = 2;
            this.KeySetCombo.SelectedIndexChanged += new System.EventHandler(this.KeySetCombo_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabGeneral);
            this.tabControl1.Controls.Add(this.TabKeys);
            this.tabControl1.Controls.Add(this.TabAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(225, 25);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 556);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // TabGeneral
            // 
            this.TabGeneral.Controls.Add(this.label7);
            this.TabGeneral.Controls.Add(this.CheckRunOnStartup);
            this.TabGeneral.Controls.Add(this.CheckRunAsAdmin);
            this.TabGeneral.Controls.Add(this.CheckTrayIconClicks);
            this.TabGeneral.Location = new System.Drawing.Point(4, 29);
            this.TabGeneral.Margin = new System.Windows.Forms.Padding(0);
            this.TabGeneral.Name = "TabGeneral";
            this.TabGeneral.Size = new System.Drawing.Size(670, 523);
            this.TabGeneral.TabIndex = 1;
            this.TabGeneral.Text = "General";
            this.TabGeneral.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(649, 78);
            this.label7.TabIndex = 6;
            this.label7.Text = "Closing this window will minimize it to the icon in the tray notification area.";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckRunOnStartup
            // 
            this.CheckRunOnStartup.AutoSize = true;
            this.CheckRunOnStartup.Location = new System.Drawing.Point(25, 165);
            this.CheckRunOnStartup.Name = "CheckRunOnStartup";
            this.CheckRunOnStartup.Size = new System.Drawing.Size(393, 24);
            this.CheckRunOnStartup.TabIndex = 5;
            this.CheckRunOnStartup.Text = "Run this program automatically at Windows startup";
            this.CheckRunOnStartup.UseVisualStyleBackColor = true;
            this.CheckRunOnStartup.CheckedChanged += new System.EventHandler(this.CheckRunOnStartup_CheckedChanged);
            // 
            // CheckRunAsAdmin
            // 
            this.CheckRunAsAdmin.Location = new System.Drawing.Point(25, 75);
            this.CheckRunAsAdmin.Name = "CheckRunAsAdmin";
            this.CheckRunAsAdmin.Size = new System.Drawing.Size(595, 60);
            this.CheckRunAsAdmin.TabIndex = 4;
            this.CheckRunAsAdmin.Text = "Run as administrator, which makes it possible to send mouse events to programs ru" +
    "nning as administrator";
            this.CheckRunAsAdmin.UseVisualStyleBackColor = true;
            this.CheckRunAsAdmin.CheckedChanged += new System.EventHandler(this.CheckRunAsAdmin_CheckedChanged);
            // 
            // CheckTrayIconClicks
            // 
            this.CheckTrayIconClicks.AutoSize = true;
            this.CheckTrayIconClicks.Location = new System.Drawing.Point(25, 30);
            this.CheckTrayIconClicks.Name = "CheckTrayIconClicks";
            this.CheckTrayIconClicks.Size = new System.Drawing.Size(419, 24);
            this.CheckTrayIconClicks.TabIndex = 2;
            this.CheckTrayIconClicks.Text = "Allow tray icon clicks to suspend and resume operation";
            this.CheckTrayIconClicks.UseVisualStyleBackColor = true;
            this.CheckTrayIconClicks.CheckedChanged += new System.EventHandler(this.CheckTrayIconClicks_CheckedChanged);
            // 
            // TabKeys
            // 
            this.TabKeys.Controls.Add(this.label8);
            this.TabKeys.Controls.Add(this.label6);
            this.TabKeys.Controls.Add(this.KeySetCombo);
            this.TabKeys.Controls.Add(this.label1);
            this.TabKeys.Controls.Add(this.LeftPress);
            this.TabKeys.Controls.Add(this.RightHold);
            this.TabKeys.Controls.Add(this.MiddlePress);
            this.TabKeys.Controls.Add(this.MiddleHold);
            this.TabKeys.Controls.Add(this.RightPress);
            this.TabKeys.Controls.Add(this.LeftHold);
            this.TabKeys.Controls.Add(this.label3);
            this.TabKeys.Controls.Add(this.label2);
            this.TabKeys.Controls.Add(this.AnyHold);
            this.TabKeys.Location = new System.Drawing.Point(4, 29);
            this.TabKeys.Margin = new System.Windows.Forms.Padding(0);
            this.TabKeys.Name = "TabKeys";
            this.TabKeys.Size = new System.Drawing.Size(670, 523);
            this.TabKeys.TabIndex = 0;
            this.TabKeys.Text = "Keys";
            this.TabKeys.UseVisualStyleBackColor = true;
            this.TabKeys.Enter += new System.EventHandler(this.TabKeys_Enter);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(311, 279);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(342, 50);
            this.label8.TabIndex = 14;
            this.label8.Text = "Note: You may leave some keys unset.";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Selected key set";
            // 
            // TabAbout
            // 
            this.TabAbout.Controls.Add(this.WebLabel);
            this.TabAbout.Controls.Add(this.label5);
            this.TabAbout.Location = new System.Drawing.Point(4, 29);
            this.TabAbout.Margin = new System.Windows.Forms.Padding(0);
            this.TabAbout.Name = "TabAbout";
            this.TabAbout.Size = new System.Drawing.Size(670, 523);
            this.TabAbout.TabIndex = 2;
            this.TabAbout.Text = "About";
            this.TabAbout.UseVisualStyleBackColor = true;
            // 
            // WebLabel
            // 
            this.WebLabel.AutoSize = true;
            this.WebLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WebLabel.Location = new System.Drawing.Point(105, 275);
            this.WebLabel.Name = "WebLabel";
            this.WebLabel.Size = new System.Drawing.Size(416, 29);
            this.WebLabel.TabIndex = 2;
            this.WebLabel.TabStop = true;
            this.WebLabel.Text = "Visit the MousePaw project on GitHub";
            this.WebLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebLabel_LinkClicked);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(620, 70);
            this.label5.TabIndex = 0;
            this.label5.Text = "MousePaw is a free and open source program\r\nthat simulates mouse button gestures." +
    "";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 556);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "MousePaw Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.TabGeneral.ResumeLayout(false);
            this.TabGeneral.PerformLayout();
            this.TabKeys.ResumeLayout(false);
            this.TabKeys.PerformLayout();
            this.TabAbout.ResumeLayout(false);
            this.TabAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LeftPress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MiddlePress;
        private System.Windows.Forms.Button RightPress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AnyHold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button LeftHold;
        private System.Windows.Forms.Button MiddleHold;
        private System.Windows.Forms.Button RightHold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox KeySetCombo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabKeys;
        private System.Windows.Forms.TabPage TabGeneral;
        private System.Windows.Forms.TabPage TabAbout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel WebLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox CheckRunOnStartup;
        private System.Windows.Forms.CheckBox CheckRunAsAdmin;
        private System.Windows.Forms.CheckBox CheckTrayIconClicks;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

