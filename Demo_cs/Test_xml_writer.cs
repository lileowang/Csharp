// Author:      Li Leo Wang
// Start Date:  2019-06-07
// Description:
//      - Demo creating XML for XSL demo
// Notes:
//      - Prepare database table: testfsn01 and script file
//      - Prepare script file: c:\temp\test01.sql
//          select fsn, timeadded
//          from testfsn01
//          
//
// Sample of output:
//<root>
//    <tc name="boot">b1</tc>
//    <tc name="test">t11</tc>
//    <tc name="test">t12</tc>
//    <tc name="test">t13</tc>
//    <tc name="boot">b2</tc>
//    <tc name="test">t21</tc>
//    <tc name="test">t22</tc>
//    <tc name="test">t23</tc>
//    <tc name="boot">b3</tc>
//    <tc name="test">t31</tc>
//    <tc name="test">t32</tc>
//    <tc name="test">t33</tc>
//    <tc name="boot">b4</tc>
//    <tc name="test">t41</tc>
//    <tc name="test">t42</tc>
//    <tc name="test">t43</tc>
//    <tc name="boot">b5</tc>
//    <tc name="test">t51</tc>
//    <tc name="test">t52</tc>
//    <tc name="test">t53</tc>
//    <tc name="boot">b6</tc>
//    <tc name="test">t61</tc>
//    <tc name="test">t62</tc>
//    <tc name="test">t63</tc>
//</root>
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
    class Test_xml_writer
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
