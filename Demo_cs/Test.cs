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
using System.IO;
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
                set.Indent = true;
                set.IndentChars = "    ";
                set.OmitXmlDeclaration = true;
                set.CloseOutput = true;

                using (XmlWriter w = XmlWriter.Create(fn, set))
                {
                    w.WriteStartElement("root");

                    for (int i = 0; i < 6; i++)
                    {
                        w.WriteStartElement("tc");
                        w.WriteAttributeString("name", "boot");
                        w.WriteString("b" + (i + 1).ToString());
                        w.WriteEndElement();

                        for (int j = 0; j < 3; j++)
                        {
                            w.WriteStartElement("tc");
                            w.WriteAttributeString("name", "test");
                            w.WriteString("t" + (i + 1).ToString() + (j + 1).ToString());
                            w.WriteEndElement();
                        }
                    }

                    w.WriteEndElement();
                }

                string[] content = File.ReadAllLines(fn);
                foreach (var s in content)
                {
                    WriteLine(s);
                }
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
