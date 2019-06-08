// Author:      Li Leo Wang
// Start Date:  2019-06-07
// Description:
//      - A test harness for temporary trying out code.
// Notes:
//      - (none)
//
// Change history:
//      - Refer to GitHub comments related to each source file.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Console;

namespace Demo_cs
{
    class Test
    {
        public bool Run(string[] args)
        {
            bool bRet = true;

            string fn = @"c:\temp\test.xml";

            try
            {
                XmlWriterSettings set = new XmlWriterSettings();
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
                bRet = false;
            }

            return bRet;
        }
    }
}
