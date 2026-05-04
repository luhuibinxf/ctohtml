using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace InitializeConfigTables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 初始化配置表 ===");
            Console.WriteLine();

            string configFile = @"d:\AI\tran\config.dat";
            if (!File.Exists(configFile))
            {
                Console.WriteLine("错误：配置文件不存在");
                return;
            }

            string encrypted = File.ReadAllText(configFile);
            string connectionString = DecryptConnectionString(encrypted);

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("错误：无法解密连接字符串");
                return;
            }

            Console.WriteLine($"连接字符串: {connectionString}");
            Console.WriteLine();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("数据库连接成功！");

                    string sqlScript = File.ReadAllText(@"d:\AI\tran\scripts\create_param_config_tables.sql");
                    string[] sqlCommands = sqlScript.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string cmdText in sqlCommands)
                    {
                        if (!string.IsNullOrWhiteSpace(cmdText))
                        {
                            using (SqlCommand cmd = new SqlCommand(cmdText.Trim(), conn))
                            {
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("执行SQL成功");
                            }
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("=== 配置表初始化完成！===");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误：{ex.Message}");
            }
        }

        static string DecryptConnectionString(string encrypted)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(encrypted);
                byte[] key = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byte[] iv = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}