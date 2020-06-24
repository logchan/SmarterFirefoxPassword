using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SmarterFirefoxPassword {
    public static class WinApi {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError=true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        
        public static string GetForegroundWindowText() {
            var handle = GetForegroundWindow();
            var length = GetWindowTextLength(handle);
            var sb = new StringBuilder(length + 1);
            GetWindowText(handle, sb, sb.Capacity);
            return sb.ToString();
        }

        public static string GetForegroundWindowFile() {
            var handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out var pid);
            var proc = Process.GetProcessById((int) pid);
            return proc.MainModule.FileName;
        }
    }
}
