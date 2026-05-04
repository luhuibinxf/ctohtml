using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace TestDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 数据库连接测试 ===");
            Console.WriteLine();

            // 测试读取配置文件
            string configFile = @"d:\AI\tran\config.dat";
            Console.WriteLine($"配置文件路径: {configFile}");
            Console.WriteLine($"配置文件存在: {File.Exists(configFile)}");

            if (File.Exists(configFile))
            {
                string encrypted = File.ReadAllText(configFile);
                Console.WriteLine($"加密内容长度: {encrypted.Length}");
                Console.WriteLine($"加密内容: {encrypted}");
                Console.WriteLine();

                // 尝试各种解密方式
                Console.WriteLine("--- 尝试解密 ---");
                
                // 方式1: 直接Base64解码
                try
                {
                    byte[] data = Convert.FromBase64String(encrypted);
                    string result = System.Text.Encoding.UTF8.GetString(data);
                    Console.WriteLine($"Base64解码结果: {result}");
                    Console.WriteLine($"是否为有效连接字符串: {IsValidConnectionString(result)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Base64解码失败: {ex.Message}");
                }

                Console.WriteLine();

                // 方式2: AES解密（使用默认密钥）
                try
                {
                    byte[] cipherBytes = Convert.FromBase64String(encrypted);
                    byte[] key = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                    byte[] iv = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = key;
                        aesAlg.IV = iv;
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (var msDecrypt = new MemoryStream(cipherBytes))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    string result = srDecrypt.ReadToEnd();
                                    Console.WriteLine($"AES解密结果: {result}");
                                    Console.WriteLine($"是否为有效连接字符串: {IsValidConnectionString(result)}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"AES解密失败: {ex.Message}");
                }

                Console.WriteLine();
            }

            // 测试数据库连接
            Console.WriteLine("--- 测试数据库连接 ---");
            
            // 使用默认测试连接字符串
            string testConnStr = "Server=localhost;Database=TestDB;User ID=sa;Password=123456;Integrated Security=False;TrustServerCertificate=True;";
            TestConnection(testConnStr);

            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

        static bool IsValidConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return false;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                return !string.IsNullOrEmpty(builder.DataSource) && !string.IsNullOrEmpty(builder.InitialCatalog);
            }
            catch
            {
                return false;
            }
        }

        static void TestConnection(string connectionString)
        {
            Console.WriteLine($"连接字符串: {connectionString}");
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Console.WriteLine("正在连接数据库...");
                    conn.Open();
                    Console.WriteLine("数据库连接成功！");
                    Console.WriteLine($"服务器版本: {conn.ServerVersion}");
                    Console.WriteLine($"数据库名称: {conn.Database}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库连接失败: {ex.Message}");
            }
        }
    }
}
