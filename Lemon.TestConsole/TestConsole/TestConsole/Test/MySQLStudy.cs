using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace TestConsole.Test
{
    public class MySQLStudy
    {
        public static void DoTest()
        {
            Console.WriteLine("MySQLStudy Start.");

            MySqlConnectionStringBuilder sbuilder = new MySqlConnectionStringBuilder();
            sbuilder.Server = "127.0.0.1";// 本机
            sbuilder.Database = "lemonTest";
            sbuilder.UserID = "visitor";// "root";
            sbuilder.Password = "visitor";// "superlemon";
            string connectionString = sbuilder.ConnectionString;

            string sql = "select * from test";//"use lemonTest; select * from test";
            DataTable dt = new DataTable();

            using (var connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = System.Data.CommandType.Text;

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                adapter.Fill(dt);
            }
        }
    }
}
