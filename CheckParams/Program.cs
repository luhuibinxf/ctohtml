using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CheckParams
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = File.ReadAllText(@"d:\AI\tran\config.dat").Trim();
                Console.WriteLine("连接字符串: " + connectionString);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("数据库连接成功！");
                    
                    string sql = "SELECT ID, NAME, PARAMETERS, UPDATE_TIME, IS_ENABLED FROM TJ_TJFX_CONFIG WHERE PARAMETERS IS NOT NULL AND LEN(CAST(PARAMETERS AS VARCHAR(MAX))) > 2";
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
                                    Console.WriteLine("ID: '" + reader["ID"] + "' (长度: " + reader["ID"].ToString().Length + ")");
                                    Console.WriteLine("名称: '" + reader["NAME"] + "' (长度: " + reader["NAME"].ToString().Length + ")");
                                    string paramsValue = reader["PARAMETERS"]?.ToString() ?? "NULL";
                                    Console.WriteLine("参数配置: " + paramsValue);
                                    Console.WriteLine("更新时间: " + reader["UPDATE_TIME"]);
                                    Console.WriteLine("是否启用: " + reader["IS_ENABLED"]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n未找到带有参数配置的记录！");
                                
                                string allSql = "SELECT ID, NAME, PARAMETERS, IS_ENABLED FROM TJ_TJFX_CONFIG";
                                using (SqlCommand allCmd = new SqlCommand(allSql, conn))
                                {
                                    using (SqlDataReader allReader = allCmd.ExecuteReader())
                                    {
                                        Console.WriteLine("\n所有配置记录：");
                                        while (allReader.Read())
                                        {
                                            string paramsValue = allReader["PARAMETERS"]?.ToString() ?? "NULL";
                                            Console.WriteLine("ID: '" + allReader["ID"] + "', 名称: '" + allReader["NAME"] + "', 参数: " + paramsValue + ", 启用: " + allReader["IS_ENABLED"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    Console.WriteLine("\n测试查询：");
                    string testName = "影像中心工作量";
                    string testSql = "SELECT * FROM TJ_TJFX_CONFIG WHERE NAME = @NAME AND IS_ENABLED = 1";
                    using (SqlCommand testCmd = new SqlCommand(testSql, conn))
                    {
                        testCmd.Parameters.AddWithValue("@NAME", testName);
                        using (SqlDataReader testReader = testCmd.ExecuteReader())
                        {
                            if (testReader.Read())
                            {
                                Console.WriteLine("通过名称查询成功！");
                                Console.WriteLine("ID: " + testReader["ID"]);
                                Console.WriteLine("NAME: " + testReader["NAME"]);
                            }
                            else
                            {
                                Console.WriteLine("通过名称查询失败！");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误: " + ex.Message);
            }
            
            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }
    }
}