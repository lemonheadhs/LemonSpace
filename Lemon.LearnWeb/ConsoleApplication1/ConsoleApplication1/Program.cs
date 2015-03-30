using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Learn_ADONET();




            //ArrayList list = new ArrayList();

            //int[] arrInt = new int[] { 1, 2, 3 };

            //bool test = arrInt is Array;

            //Array.Reverse(arrInt);

            //Array.Sort(arrInt); // 从左到右升序排列

            //list.Add("1");
            //list.Add("2");

            //int count = list.Count;
            //count = list.Add("3");
        }

        static void Learn_ADONET()
        {
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
