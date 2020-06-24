using SmartCardClr;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmarterFirefoxPassword {
    public partial class MainForm : Form {
        // TODO make configurable
        private const string TargetTitle = "Password Required - Mozilla Firefox";
        private const string TargetFile = @"C:\Program Files\Mozilla Firefox\firefox.exe";

        private const string Signature = "sfp:";

        public MainForm() {
            InitializeComponent();
        }

        private void viewPwdChk_CheckedChanged(object sender, EventArgs e) {
            pwdBox.PasswordChar = viewPwdChk.Checked ? '\0' : '*';
        }

        private void readCardBtn_Click(object sender, EventArgs e) {
            if (TryReadData(out var s)) {
                pwdBox.Text = s;
                Success("Password read from card");
            }
        }

        private bool TryReadData(out string result) {
            result = TryCardOperation(card => {
                var data = card.ReadData();
                return ConvertData(data);
            });
            return result != null;
        }

        private string ConvertData(byte[] data) {
            var sb = new StringBuilder();
            if (data.Length < Signature.Length) {
                throw new Exception("Data is too short");
            }

            for (var i = 0; i < Signature.Length; ++i) {
                if (data[i] != Signature[i]) {
                    throw new Exception("Signature not found");
                }
            }
            for (var i = Signature.Length; i < data.Length; ++i) {
                var ch = (Char) data[i];
                if (Char.IsControl(ch)) {
                    continue;
                }

                sb.Append(ch);
            }

            return sb.ToString();
        }

        private bool TryConvertData(string s, out byte[] data) {
            data = null;

            var result = new List<byte>(s.Length + Signature.Length);
            foreach (var c in Signature) {
                result.Add((byte) c);
            }
            foreach (var c in s) {
                var b = (int) c;
                if (b <= 0x1F || b >= 0x7F) {
                    Alert("Invalid characters in password");
                    return false;
                }
                result.Add((byte) b);
            }

            data = result.ToArray();
            return true;
        }

        private void SetMsg(string msg, Color color) {
            msgLbl.Text = msg;
            msgLbl.ForeColor = color;
        }

        private void ClearMsg() {
            msgLbl.Text = "";
        }

        private void Success(string msg) {
            SetMsg(msg, Color.Green);
        }

        private void Alert(string msg) {
            SetMsg(msg, Color.Brown);
        }

        private void writeCardBtn_Click(object sender, EventArgs e) {
            if (!TryConvertData(pwdBox.Text, out var data)) {
                return;
            }

            TryCardOperation(card => {
                card.WriteData(data);
                Success("Password written to card");
                return new object();
            });
        }

        private T TryCardOperation<T>(Func<SmartCardContext, T> func) where T: class {
            try {
                ClearMsg();

                using (var card = new SmartCardContext()) {
                    var readers = card.GetReaders();
                    if (readers.Length == 0) {
                        Alert("No reader is found");
                        return null;
                    }

                    // TODO: I have one reader only
                    card.OpenReader(readers[0]);
                    var rtn = func(card);
                    card.CloseReader();

                    return rtn;
                }
            }
            catch (Exception ex) {
                Alert($"Exception: {ex.Message}, check reader and card");
                return null;
            }
        }

        private void checkWindowTimer_Tick(object sender, EventArgs e) {
            var title = WinApi.GetForegroundWindowText();
            wndLbl.Text = title;

            var titleMatches = title == TargetTitle;
            wndLbl.ForeColor = titleMatches ? Color.Green : Color.Black;
            if (!titleMatches) {
                wndFileLbl.Text = "";
                return;
            }

            var file = WinApi.GetForegroundWindowFile();
            wndFileLbl.Text = file;

            var fileMatches = file == TargetFile;
            wndFileLbl.ForeColor = fileMatches ? Color.Green : Color.Red;

            if (!fileMatches) {
                return;
            }

            if (!TryReadData(out var pwd)) {
                return;
            }

            SendKeys.Send(pwd);
            SendKeys.Send("\n");
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Show();
            Activate();
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized) {
                ShowInTaskbar = false;
                Hide();
            }
        }

        private void unlockWriteChk_CheckedChanged(object sender, EventArgs e) {
            writeCardBtn.Enabled = unlockWriteChk.Checked;

        }
    }
}
