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

// lambda for delegate
> Func<int, int, bool> eq = (x, y) => x == y;
> eq(1, 1)
true
> eq(1, 2)
false

// lambda for regex
> using System.Text.RegularExpressions;
> var s = "if 2+2 is 4 then 1+2+3+4 is 10";
> s
"if 2+2 is 4 then 1+2+3+4 is 10"
> var r = Regex.Replace(s, @"(\d+)\+(\d+)", m =>
. {
.     var a = Int32.Parse(m.Groups[1].ToString());
.     var b = Int32.Parse(m.Groups[2].ToString());
.     return (a + b).ToString();
. });
> r
"if 4 is 4 then 3+7 is 10"

// LINQ for filter
> var a = new int[] { 1, 2, 3, 4, 5 };
> var b = a.Where(i => i > 2).ToList();
> b
List<int>(3) { 3, 4, 5 }
> b.Count()
3
> b.Average()
4

// LINQ for reverse
> var c = "Hello world!";
> var d = new string(c.ToCharArray().Reverse().ToArray());
> d
"!dlrow olleH"
