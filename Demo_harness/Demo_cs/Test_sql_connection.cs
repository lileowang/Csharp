// Author:      Li Leo Wang
// Start Date:  2019-05-23
// Description:
//      - Demo SqlConnection
// Notes:
//      - Prepare database table: testfsn01 and script file
//      - Prepare script file: c:\temp\test01.sql
//          select fsn, timeadded
//          from testfsn01
//          
// Change history:
//      - Refer to GitHub comments related to each source file.
//

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using static System.Console;

namespace Demo_cs
{
    class Test_sql_connection
    {
        public bool Run(string[] args)
        {
            bool bRet = true;
            string fn = @"C:\Temp\test01.sql";

            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString =
                        @"server = .\mfgt_station;" +
                        @"database = lwsqlserver;" +
                        @"trusted_connection = true;";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        File.ReadAllText(fn)
                        , conn
                        );
                    dt.Load(cmd.ExecuteReader());
                    foreach (DataRow r in dt.Rows)
                    {
                        WriteLine("fsn: {0}, time: {1}"
                            , r["fsn"].ToString()
                            , DateTime.Parse(r["timeadded"].ToString())
                            );
                    }
                }
            }
            catch (System.Exception e)
            {
                WriteLine(e.Message);
                bRet = false;
            }

            return bRet;
        }
    }
}
