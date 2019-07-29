// Author:      Li Leo Wang
// Start Date:  2019-07-28
// Description:
//      - Simulate mouse click at specified cursor position
// Notes:
//      - (none)
//
// Change history:
//      - Refer to GitHub comments related to each source file.
//

using System;
using static System.Console;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Click_app_console
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName,
           string lpWindowName);

        // bring from minimized state
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // http://pinvoke.net/default.aspx/user32.mouse_event
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x01000;

        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_XBUTTONDOWN = 0x20B;
        private const int WM_XBUTTONUP = 0x20C;
        private const int WM_XBUTTONDBLCLK = 0x20D;
        private const int WM_MOUSEHWHEEL = 0x20E;

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_RESTORE = 9;

        static void Main(string[] args)
        {
            Program p = new Program();

            if (args.Length == 5)
            {
                p.Run(args);
            }
            else
            {
                WriteLine("Run with four arguments:");
                WriteLine("(1) Cursor position X: e.g. 150");
                WriteLine("(2) Cursor position Y: e.g. 850");
                WriteLine("(3) Interval in seconds: e.g. 10");
                WriteLine("(4) Loops: e.g. 3");
                WriteLine("(5) Application ID (PID): e.g. 20704");
                WriteLine("Like this:");
                WriteLine("Click_app_console.exe 150 850 10 3 20704");

            }
        }

        bool Run(string[] args)
        {
            bool bRet = true;
            int x = int.Parse(args[0]);
            int y = int.Parse(args[1]);
            int interval_sec = int.Parse(args[2]);
            int loops = int.Parse(args[3]);
            //string appName = args[4];
            int pid = int.Parse(args[4]);

            for (int i = 0; i < loops; i++)
            {
                WriteLine($"Click {i} at: {DateTime.Now}");
                //Click_at_cursor(x, y);
                //Click_at_cursor_2(x, y, appName);
                Click_at_cursor_3(x, y, pid);
                Thread.Sleep(interval_sec * 1000);
            }

            return bRet;
        }

        void Click_at_cursor(int x, int y)
        {
            SetCursorPos(x, y);
            Thread.Sleep(1000);
            mouse_event(MOUSEEVENTF_ABSOLUTE, (uint)x, (uint)y, 0, 0);

            //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //Thread.Sleep(50);
            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        void Click_at_cursor_2(int x, int y, string appName)
        {
            // Paint: mspaint
            // notepad++
            foreach (Process p in Process.GetProcessesByName(appName))
            {
                // Get a handle
                IntPtr handle = p.MainWindowHandle;

                if (handle == IntPtr.Zero)
                {
                    MessageBox.Show("Program is not running.");
                    return;
                }

                ShowWindow(handle, SW_RESTORE);
                SetForegroundWindow(handle);

                //IntPtr lParam = (IntPtr)((y << 16) | x);
                IntPtr lParam = (IntPtr)Make_LPARAM(x, y);
                IntPtr wParam = IntPtr.Zero;

                // Does not work for Notepad++
                //SendMessage(handle, WM_LBUTTONDOWN, wParam, lParam);
                //SendMessage(handle, WM_LBUTTONUP, wParam, lParam);

                SetCursorPos(x, y);
                Thread.Sleep(1000);

                mouse_event(MOUSEEVENTF_ABSOLUTE, (uint)x, (uint)y, 0, 0);

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                Thread.Sleep(50);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            }
        }

        void Click_at_cursor_3(int x, int y, int pid)
        {
            Process p = Process.GetProcessById(pid);
            // Get a handle
            IntPtr handle = p.MainWindowHandle;

            if (handle == IntPtr.Zero)
            {
                MessageBox.Show("Program is not running.");
                return;
            }
            ShowWindow(handle, SW_SHOWNORMAL);
            SetForegroundWindow(handle);

            //IntPtr lParam = (IntPtr)((y << 16) | x);
            IntPtr lParam = (IntPtr)Make_LPARAM(x, y);
            IntPtr wParam = IntPtr.Zero;

            // Does not work for Notepad++, Visual Studio
            //SendMessage(handle, WM_LBUTTONDOWN, wParam, lParam);
            //SendMessage(handle, WM_LBUTTONUP, wParam, lParam);

            // Works for Notepad++, Visual Studio
            // But not for WPF test program: 
            // - Reason: this program happens to require "run as administrator"
            // - Fix: run as administrator

            SetCursorPos(x, y);
            Thread.Sleep(1000);

            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, 0);
            Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);

            // not used
            //mouse_event(MOUSEEVENTF_ABSOLUTE, (uint)x, (uint)y, 0, 0);
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

        }

        private int Make_LPARAM(int p, int p_2)
        {
            return ((p_2 << 16) | (p & 0xFFFF));
        }
    }
}
