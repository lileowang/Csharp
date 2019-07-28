// Author:      Li Leo Wang
// Start Date:  2019-07-27
// Description:
//      - Display cursor coordinates
// Notes:
//      - (none)
//
// Change history:
//      - Refer to GitHub comments related to each source file.
//

using System;
using System.Threading;
using System.Windows.Forms;
using static System.Console;

namespace Show_cursor_coordinates
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Press Escape to stop");

            while (!(KeyAvailable && ReadKey(true).Key == ConsoleKey.Escape))
            {
                WriteLine("x = " + Cursor.Position.X + ", y = " + Cursor.Position.Y);
                Thread.Sleep(1000);
            }

            WriteLine("Press any key to quit");
            ReadKey();
        }
    }
}
