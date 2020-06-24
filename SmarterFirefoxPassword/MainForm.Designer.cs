namespace SmarterFirefoxPassword {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.readCardBtn = new System.Windows.Forms.Button();
            this.writeCardBtn = new System.Windows.Forms.Button();
            this.pwdBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewPwdChk = new System.Windows.Forms.CheckBox();
            this.msgLbl = new System.Windows.Forms.Label();
            this.wndLbl = new System.Windows.Forms.Label();
            this.checkWindowTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.wndFileLbl = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.unlockWriteChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // readCardBtn
            // 
            this.readCardBtn.Location = new System.Drawing.Point(210, 81);
            this.readCardBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.readCardBtn.Name = "readCardBtn";
            this.readCardBtn.Size = new System.Drawing.Size(182, 49);
            this.readCardBtn.TabIndex = 0;
            this.readCardBtn.Text = "Read Card";
            this.readCardBtn.UseVisualStyleBackColor = true;
            this.readCardBtn.Click += new System.EventHandler(this.readCardBtn_Click);
            // 
            // writeCardBtn
            // 
            this.writeCardBtn.Enabled = false;
            this.writeCardBtn.Location = new System.Drawing.Point(19, 81);
            this.writeCardBtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.writeCardBtn.Name = "writeCardBtn";
            this.writeCardBtn.Size = new System.Drawing.Size(182, 49);
            this.writeCardBtn.TabIndex = 1;
            this.writeCardBtn.Text = "Write Card";
            this.writeCardBtn.UseVisualStyleBackColor = true;
            this.writeCardBtn.Click += new System.EventHandler(this.writeCardBtn_Click);
            // 
            // pwdBox
            // 
            this.pwdBox.Location = new System.Drawing.Point(177, 35);
            this.pwdBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pwdBox.MaxLength = 32;
            this.pwdBox.Name = "pwdBox";
            this.pwdBox.PasswordChar = '*';
            this.pwdBox.Size = new System.Drawing.Size(527, 35);
            this.pwdBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Password";
            // 
            // viewPwdChk
            // 
            this.viewPwdChk.AutoSize = true;
            this.viewPwdChk.Location = new System.Drawing.Point(717, 36);
            this.viewPwdChk.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.viewPwdChk.Name = "viewPwdChk";
            this.viewPwdChk.Size = new System.Drawing.Size(112, 33);
            this.viewPwdChk.TabIndex = 4;
            this.viewPwdChk.Text = "Visible";
            this.viewPwdChk.UseVisualStyleBackColor = true;
            this.viewPwdChk.CheckedChanged += new System.EventHandler(this.viewPwdChk_CheckedChanged);
            // 
            // msgLbl
            // 
            this.msgLbl.AutoSize = true;
            this.msgLbl.Location = new System.Drawing.Point(401, 96);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(0, 29);
            this.msgLbl.TabIndex = 5;
            // 
            // wndLbl
            // 
            this.wndLbl.AutoSize = true;
            this.wndLbl.Location = new System.Drawing.Point(172, 192);
            this.wndLbl.Name = "wndLbl";
            this.wndLbl.Size = new System.Drawing.Size(0, 29);
            this.wndLbl.TabIndex = 6;
            // 
            // checkWindowTimer
            // 
            this.checkWindowTimer.Enabled = true;
            this.checkWindowTimer.Interval = 1000;
            this.checkWindowTimer.Tick += new System.EventHandler(this.checkWindowTimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "FG title:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 29);
            this.label3.TabIndex = 8;
            this.label3.Text = "FG file:";
            // 
            // wndFileLbl
            // 
            this.wndFileLbl.AutoSize = true;
            this.wndFileLbl.Location = new System.Drawing.Point(172, 235);
            this.wndFileLbl.Name = "wndFileLbl";
            this.wndFileLbl.Size = new System.Drawing.Size(0, 29);
            this.wndFileLbl.TabIndex = 9;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Smarter Firefox Password";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // unlockWriteChk
            // 
            this.unlockWriteChk.AutoSize = true;
            this.unlockWriteChk.Location = new System.Drawing.Point(19, 137);
            this.unlockWriteChk.Name = "unlockWriteChk";
            this.unlockWriteChk.Size = new System.Drawing.Size(282, 33);
            this.unlockWriteChk.TabIndex = 10;
            this.unlockWriteChk.Text = "Unlock the write button";
            this.unlockWriteChk.UseVisualStyleBackColor = true;
            this.unlockWriteChk.CheckedChanged += new System.EventHandler(this.unlockWriteChk_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 311);
            this.Controls.Add(this.unlockWriteChk);
            this.Controls.Add(this.wndFileLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.wndLbl);
            this.Controls.Add(this.msgLbl);
            this.Controls.Add(this.viewPwdChk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pwdBox);
            this.Controls.Add(this.writeCardBtn);
            this.Controls.Add(this.readCardBtn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smarter Firefox Password";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readCardBtn;
        private System.Windows.Forms.Button writeCardBtn;
        private System.Windows.Forms.TextBox pwdBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox viewPwdChk;
        private System.Windows.Forms.Label msgLbl;
        private System.Windows.Forms.Label wndLbl;
        private System.Windows.Forms.Timer checkWindowTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label wndFileLbl;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox unlockWriteChk;
    }
}

