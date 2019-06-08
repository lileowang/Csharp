// Author:      Li Leo Wang
// Start Date:  2019-05-09
// Description:
//      - Demo project for C#  features
// Notes:
//      - The individual features are implemented in separated classes.
// Change history:
//      - Refer to GitHub comments related to each source file.
//

using System;
using static System.Console;

namespace Demo_cs
{
    class Program
    {
        static int Main(string[] args)
        {
            bool bRet = true;

            Test test = new Test();
            bRet = test.Run(args);

            //Test_xml_writer test_xml_writer = new Test_xml_writer();
            //bRet = test_xml_writer.Run(args);

            //Test_regex test_regex = new Test_regex();
            //bRet = test_regex.Run(args);

            //Test_sql_connection test_Sql_Connection = new Test_sql_connection();
            //bRet = test_Sql_Connection.Run(args);

            WriteLine($"{bRet}");
            ReadKey();
            return 0;
        }
    }
}
