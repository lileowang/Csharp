// Author:      Li Leo Wang
// Start Date:  2019-05-27
// Description:
//      - C# interactive (REPL) demo
// Notes:
//      - Visual Studio 2017: Open C# REPL 
//              by View --> Other windows --> C# interactive
//      - Copy code without ">" and run it in C# REPL
//          
// Change history:
//      - Refer to GitHub comments related to each source file.
//

// nullable and coalesce
> int? n1 = null;
> n1
null
> int n2 = n1 ?? 10;
> n2
10

// shortcut for console writeline
> Action<object> cw = Console.WriteLine;
> cw($"{nameof(n2)} = {n2}")
n2 = 10

// stopwatch for performance
> using System.Diagnostics;
> var w = new Stopwatch();
> w.Start();
> w.Stop();
> w.ElapsedMilliseconds
3957
