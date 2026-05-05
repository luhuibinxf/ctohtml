using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

string connectionString = File.ReadAllText(@"d:\AI\tran\config.dat").Trim();

Console.WriteLine("连接字符串: " + connectionString);

using (SqlConnection conn = new SqlConnection(connectionString))
{
    conn.Open();
    Console.WriteLine("数据库连接成功！");
    
    string sql = "SELECT ID, NAME, PARAMETERS, UPDATE_TIME FROM TJ_TJFX_CONFIG WHERE PARAMETERS IS NOT NULL AND PARAMETERS <> '[]'";
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {
                Console.WriteLine("\n找到以下带有参数配置的记录：");
                while (reader.Read())
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("ID: " + reader["ID"]);
                    Console.WriteLine("名称: " + reader["NAME"]);
                    Console.WriteLine("参数配置: " + reader["PARAMETERS"]);
                    Console.WriteLine("更新时间: " + reader["UPDATE_TIME"]);
                }
            }
            else
            {
                Console.WriteLine("\n未找到带有参数配置的记录！");
                
                string allSql = "SELECT ID, NAME, PARAMETERS FROM TJ_TJFX_CONFIG";
                using (SqlCommand allCmd = new SqlCommand(allSql, conn))
                {
                    using (SqlDataReader allReader = allCmd.ExecuteReader())
                    {
                        Console.WriteLine("\n所有配置记录：");
                        while (allReader.Read())
                        {
                            Console.WriteLine("ID: " + allReader["ID"] + ", 名称: " + allReader["NAME"] + ", 参数: " + (allReader["PARAMETERS"] ?? "NULL"));
                        }
                    }
                }
            }
        }
    }
}

Console.WriteLine("\n按任意键退出...");
Console.ReadKey();