using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace TestConsole.Test
{
    public class ADONETStudy
    {
        public static void DoTest()
        {
            Console.WriteLine("ADONETStudy Start.");

            // 准备工作 (连接字符串，SQL字符串)
            string connectionStr = string.Empty;
            OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder();
            builder.DataSource = "spatial";
            builder.UserID = "lemon2";
            builder.Password = "lemon2";
            connectionStr = builder.ConnectionString;

            string sql = "select * from anyt_bak where rownum=1";
            
            // 创建连接，进行数据查询
            using (OracleConnection connection = new OracleConnection(connectionStr))
            {
                // 创建Command对象
                OracleCommand command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                try  // 打开连接，读取数据
                {
                    connection.Open();
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("RecordID:" + reader["id"]);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:" + e.Message);
                }
            }
            Console.ReadKey();
        }
    }
}
