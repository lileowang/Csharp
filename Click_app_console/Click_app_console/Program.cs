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

namespace Click_app_console
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

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
        static void Main(string[] args)
        {
            Program p = new Program();

            if (args.Length == 4)
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
                WriteLine("Like this:");
                WriteLine("Click_app_console.exe 150 850 10 3");

            }
        }

        bool Run(string[] args)
        {
            bool bRet = true;
            int x = int.Parse(args[0].ToString());
            int y = int.Parse(args[1].ToString());
            int interval_sec = int.Parse(args[2].ToString());
            int loops = int.Parse(args[3].ToString());

            for (int i = 0; i < loops; i++)
            {
                WriteLine($"Click {i} at: {DateTime.Now}");
                Click_at_cursor(x, y);
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
    }
}
