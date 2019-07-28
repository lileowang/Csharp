// Author:      Li Leo Wang
// Start Date:  2019-05-10
// Description:
//      - Demo how to use regex match collection
// Notes:
//      - Copy "Test_boost_regex.cpp" from Demo_cpp project to c:\temp, 
//        which contains:
//          string s =
//              "start: \n"
//                  "256 bytes from 11:22:33:44:55:66 in 12.34 ms \n"
//                  "256 bytes from 11:22:33:44:55:66 in 12.34 ms \n"
//                  "256 bytes from 11:22:33:44:55:66 in 12.34 ms \n"
//                  "256 bytes from 11:22:33:44:55:66 in 12.34 ms \n"
//                  "end.\n";
//          
// Change history:
//      - Refer to GitHub comments related to each source file.
//

using System;
using System.IO;
using System.Text.RegularExpressions;
using static System.Console;

namespace Demo_cs
{
    public class Test_regex
    {
        public bool Run()
        {
            bool bRet = true;

            // hard-coded file name for quick testing 
            string fn = @"C:\Temp\Test_boost_regex.cpp";
            string[] content = File.ReadAllLines(fn);
            string p =
                @"(\d+) bytes from " +
                @"(\w{2}:\w{2}:\w{2}:\w{2}:\w{2}:\w{2}) in " +
                @"(\d+\.\d+) ms";

            foreach (var s in content)
            {
                //WriteLine(s);

                MatchCollection matches = Regex.Matches(s, p);
                foreach (Match m in matches)
                {
                    // display in reverse order
                    WriteLine("{0} {1} {2}"
                        , m.Groups[3].Value
                        , m.Groups[2].Value
                        , m.Groups[1].Value
                        );
                }
            }


            return bRet;
        }
    }
}
