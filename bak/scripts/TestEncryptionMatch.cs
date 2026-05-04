using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestEncryptionMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 验证当前加密方式 ===");
            Console.WriteLine();

            // 当前代码使用的密钥
            byte[] key = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] iv = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            Console.WriteLine("当前代码使用的密钥:");
            Console.WriteLine($"  Key: {BitConverter.ToString(key)}");
            Console.WriteLine($"  IV:  {BitConverter.ToString(iv)}");
            Console.WriteLine();

            // 配置文件中的加密数据
            string encryptedConfig = "kkzI4Gy8No7vpAZuMSLF4rfumYQ03P4PVD1O2DTNq2eqLHxmYb3uXVRWRC1d9NHnFbIUgN4RFN3Qr3FliE17T0u4oO1cwudk3U2Y0Wuxj/0=";
            Console.WriteLine("配置文件中的加密数据:");
            Console.WriteLine($"  {encryptedConfig}");
            Console.WriteLine();

            // 尝试用当前密钥解密
            Console.WriteLine("--- 尝试用当前密钥解密 ---");
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(encryptedConfig);
                
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (var msDecrypt = new MemoryStream(cipherBytes))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        string result = srDecrypt.ReadToEnd();
                        Console.WriteLine($"解密结果: {result}");
                        Console.WriteLine($"是否为有效连接字符串: {IsValidConnectionString(result)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"解密失败: {ex.Message}");
            }
            Console.WriteLine();

            // 尝试加密一个测试连接字符串，看看是否匹配配置文件
            Console.WriteLine("--- 尝试加密测试连接字符串 ---");
            string testConnection = "Server=localhost;Database=TestDB;User ID=sa;Password=123456;Integrated Security=False;TrustServerCertificate=True;";
            Console.WriteLine($"测试连接字符串: {testConnection}");
            
            string encryptedTest = EncryptWithAes(testConnection, key, iv);
            Console.WriteLine($"加密结果: {encryptedTest}");
            Console.WriteLine($"与配置文件数据是否相同: {encryptedTest == encryptedConfig}");
            Console.WriteLine();

            // 检查长度
            byte[] configBytes = Convert.FromBase64String(encryptedConfig);
            byte[] testBytes = Convert.FromBase64String(encryptedTest);
            Console.WriteLine($"配置文件数据长度: {configBytes.Length} 字节");
            Console.WriteLine($"测试加密长度: {testBytes.Length} 字节");
            Console.WriteLine();

            // 尝试不同的填充模式
            Console.WriteLine("--- 尝试不同的填充模式 ---");
            PaddingMode[] modes = { PaddingMode.PKCS7, PaddingMode.Zeros, PaddingMode.None, PaddingMode.ANSIX923, PaddingMode.ISO10126 };
            foreach (var mode in modes)
            {
                try
                {
                    byte[] cipherBytes = Convert.FromBase64String(encryptedConfig);
                    
                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = key;
                        aesAlg.IV = iv;
                        aesAlg.Padding = mode;
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (var msDecrypt = new MemoryStream(cipherBytes))
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            string result = srDecrypt.ReadToEnd();
                            if (IsValidConnectionString(result))
                            {
                                Console.WriteLine($"填充模式 {mode}: 找到有效连接字符串!");
                                Console.WriteLine($"  结果: {result}");
                                return;
                            }
                        }
                    }
                }
                catch { }
            }
            Console.WriteLine("未找到有效填充模式");
            Console.WriteLine();

            // 尝试不同的密钥
            Console.WriteLine("--- 尝试常见密钥 ---");
            byte[][] commonKeys = {
                new byte[] {0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F},
                new byte[] {0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF},
                new byte[] {0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x49,0x4A,0x4B,0x4C,0x4D,0x4E,0x4F,0x50},
                new byte[] {0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64},
                new byte[] {0x11,0x22,0x33,0x44,0x55,0x66,0x77,0x88,0x99,0xAA,0xBB,0xCC,0xDD,0xEE,0xFF,0x00},
                new byte[] {0x01,0x12,0x23,0x34,0x45,0x56,0x67,0x78,0x89,0x9A,0xAB,0xBC,0xCD,0xDE,0xEF,0xF0}
            };
            
            foreach (byte[] testKey in commonKeys)
            {
                try
                {
                    byte[] cipherBytes = Convert.FromBase64String(encryptedConfig);
                    
                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = testKey;
                        aesAlg.IV = testKey;
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (var msDecrypt = new MemoryStream(cipherBytes))
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            string result = srDecrypt.ReadToEnd();
                            if (IsValidConnectionString(result))
                            {
                                Console.WriteLine($"找到有效密钥!");
                                Console.WriteLine($"  密钥: {BitConverter.ToString(testKey)}");
                                Console.WriteLine($"  结果: {result}");
                                return;
                            }
                        }
                    }
                }
                catch { }
            }
            Console.WriteLine("未找到有效密钥");

            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

        static string EncryptWithAes(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        static bool IsValidConnectionString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            bool hasServer = str.IndexOf("Server", StringComparison.OrdinalIgnoreCase) >= 0 ||
                           str.IndexOf("Data Source", StringComparison.OrdinalIgnoreCase) >= 0;
            bool hasDatabase = str.IndexOf("Database", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             str.IndexOf("Initial Catalog", StringComparison.OrdinalIgnoreCase) >= 0;

            return hasServer && hasDatabase;
        }
    }
}
