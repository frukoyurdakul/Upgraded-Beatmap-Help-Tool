using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Beatmap_Help_Tool
{
    static class Program
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static MainWindow mainWindow;
        private static bool isControlDown = false;
        private static bool isSDown = false;
        private static bool isEventSent = false;

        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            // _hookID = SetHook(_proc);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            // ***this line is added***
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainWindow = new MainWindow();
            Application.Run(mainWindow);
            // UnhookWindowsHookEx(_hookID);
        }

        // ***also dllimport of that function***
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    Keys vkCode = (Keys)Marshal.ReadInt32(lParam);
                    if (vkCode == Keys.LControlKey || vkCode == Keys.RControlKey)
                        isControlDown = true;
                    else if (vkCode == Keys.S)
                        isSDown = true;
                }
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                    Keys vkCode = (Keys)Marshal.ReadInt32(lParam);
                    if (vkCode == Keys.LControlKey || vkCode == Keys.RControlKey)
                    {
                        isEventSent = false;
                        isControlDown = false;
                    }
                    else if (vkCode == Keys.S)
                    {
                        isSDown = false;
                        isEventSent = false;
                    }
                }

                if (isControlDown && isSDown && !isEventSent)
                {
                    mainWindow.onSaveHotkey();
                    isEventSent = true;
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
