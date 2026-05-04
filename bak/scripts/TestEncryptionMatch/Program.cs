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
            Console.WriteLine("=== 深度分析配置文件加密方式 ===");
            Console.WriteLine();

            string encryptedConfig = "kkzI4Gy8No7vpAZuMSLF4rfumYQ03P4PVD1O2DTNq2eqLHxmYb3uXVRWRC1d9NHnFbIUgN4RFN3Qr3FliE17T0u4oO1cwudk3U2Y0Wuxj/0=";
            byte[] data = Convert.FromBase64String(encryptedConfig);

            Console.WriteLine("配置文件数据分析:");
            Console.WriteLine($"Base64长度: {encryptedConfig.Length}");
            Console.WriteLine($"解码后字节数: {data.Length}");
            Console.WriteLine($"十六进制: {BitConverter.ToString(data)}");
            Console.WriteLine();

            // 方式1: 直接UTF8解码
            Console.WriteLine("【方式1】直接UTF8解码:");
            string directText = Encoding.UTF8.GetString(data);
            Console.WriteLine($"结果: {directText}");
            Console.WriteLine($"是否有效: {IsValidConnectionString(directText)}");
            Console.WriteLine();

            // 方式2: ASCII解码
            Console.WriteLine("【方式2】ASCII解码:");
            string asciiText = Encoding.ASCII.GetString(data);
            Console.WriteLine($"结果: {asciiText}");
            Console.WriteLine($"是否有效: {IsValidConnectionString(asciiText)}");
            Console.WriteLine();

            // 方式3: 尝试所有单字节XOR密钥
            Console.WriteLine("【方式3】尝试所有单字节XOR密钥 (0-255):");
            for (byte key = 0; key <= 255; key++)
            {
                string result = XorDecrypt(data, key);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"找到XOR密钥 0x{key:X2}: {result}");
                    return;
                }
            }
            Console.WriteLine("未找到有效XOR密钥");
            Console.WriteLine();

            // 方式4: 尝试双字节XOR密钥
            Console.WriteLine("【方式4】尝试双字节XOR密钥:");
            for (byte k1 = 0; k1 <= 50; k1++)
            {
                for (byte k2 = 0; k2 <= 50; k2++)
                {
                    string result = DoubleXorDecrypt(data, k1, k2);
                    if (IsValidConnectionString(result))
                    {
                        Console.WriteLine($"找到双密钥 0x{k1:X2}, 0x{k2:X2}: {result}");
                        return;
                    }
                }
            }
            Console.WriteLine("未找到有效双密钥");
            Console.WriteLine();

            // 方式5: 字节偏移
            Console.WriteLine("【方式5】尝试字节偏移 (1-255):");
            for (int offset = 1; offset <= 255; offset++)
            {
                string result = ByteOffsetDecrypt(data, offset);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"找到偏移量 {offset}: {result}");
                    return;
                }
            }
            Console.WriteLine("未找到有效偏移量");
            Console.WriteLine();

            // 方式6: 尝试反转+XOR
            Console.WriteLine("【方式6】尝试反转+XOR:");
            byte[] reversed = (byte[])data.Clone();
            Array.Reverse(reversed);
            for (byte key = 0; key <= 100; key++)
            {
                string result = XorDecrypt(reversed, key);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"反转+XOR密钥 0x{key:X2}: {result}");
                    return;
                }
            }
            Console.WriteLine("未找到有效组合");
            Console.WriteLine();

            // 方式7: 尝试DES加密
            Console.WriteLine("【方式7】尝试DES解密:");
            byte[][] desKeys = {
                new byte[] {0x12,0x34,0x56,0x78,0x90,0xAB,0xCD,0xEF},
                new byte[] {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01},
                new byte[] {0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF},
                new byte[] {0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48}
            };
            foreach (byte[] key in desKeys)
            {
                try
                {
                    string result = DesDecrypt(data, key, key);
                    if (IsValidConnectionString(result))
                    {
                        Console.WriteLine($"找到DES密钥: {BitConverter.ToString(key)}");
                        Console.WriteLine($"结果: {result}");
                        return;
                    }
                }
                catch { }
            }
            Console.WriteLine("未找到有效DES密钥");
            Console.WriteLine();

            // 方式8: 尝试3DES加密
            Console.WriteLine("【方式8】尝试3DES解密:");
            byte[][] tripleDesKeys = {
                new byte[] {0x12,0x34,0x56,0x78,0x90,0xAB,0xCD,0xEF,0x12,0x34,0x56,0x78,0x90,0xAB,0xCD,0xEF,0x12,0x34,0x56,0x78,0x90,0xAB,0xCD,0xEF},
                new byte[] {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}
            };
            foreach (byte[] key in tripleDesKeys)
            {
                try
                {
                    string result = TripleDesDecrypt(data, key, key);
                    if (IsValidConnectionString(result))
                    {
                        Console.WriteLine($"找到3DES密钥: {BitConverter.ToString(key).Substring(0, 20)}...");
                        Console.WriteLine($"结果: {result}");
                        return;
                    }
                }
                catch { }
            }
            Console.WriteLine("未找到有效3DES密钥");
            Console.WriteLine();

            // 方式9: 尝试RC2加密
            Console.WriteLine("【方式9】尝试RC2解密:");
            byte[][] rc2Keys = {
                new byte[] {0x12,0x34,0x56,0x78,0x90,0xAB,0xCD,0xEF},
                new byte[] {0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07}
            };
            foreach (byte[] key in rc2Keys)
            {
                try
                {
                    string result = Rc2Decrypt(data, key, key);
                    if (IsValidConnectionString(result))
                    {
                        Console.WriteLine($"找到RC2密钥: {BitConverter.ToString(key)}");
                        Console.WriteLine($"结果: {result}");
                        return;
                    }
                }
                catch { }
            }
            Console.WriteLine("未找到有效RC2密钥");
            Console.WriteLine();

            // 方式10: 分析数据特征
            Console.WriteLine("【方式10】数据特征分析:");
            AnalyzeData(data);

            Console.WriteLine();
            Console.WriteLine("未找到匹配的加密方式");
            Console.WriteLine("建议：删除配置文件，让程序重新生成");
            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

        static string XorDecrypt(byte[] data, byte key)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ key);
            }
            return Encoding.UTF8.GetString(result);
        }

        static string DoubleXorDecrypt(byte[] data, byte key1, byte key2)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ key1 ^ key2);
            }
            return Encoding.UTF8.GetString(result);
        }

        static string ByteOffsetDecrypt(byte[] data, int offset)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)((data[i] - offset + 256) % 256);
            }
            return Encoding.UTF8.GetString(result);
        }

        static string DesDecrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (DES des = DES.Create())
            {
                des.Key = key;
                des.IV = iv;
                ICryptoTransform decryptor = des.CreateDecryptor();
                
                using (MemoryStream ms = new MemoryStream(data))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        static string TripleDesDecrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (TripleDES des = TripleDES.Create())
            {
                des.Key = key;
                des.IV = iv;
                ICryptoTransform decryptor = des.CreateDecryptor();
                
                using (MemoryStream ms = new MemoryStream(data))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        static string Rc2Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (RC2 rc2 = RC2.Create())
            {
                rc2.Key = key;
                rc2.IV = iv;
                ICryptoTransform decryptor = rc2.CreateDecryptor();
                
                using (MemoryStream ms = new MemoryStream(data))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        static void AnalyzeData(byte[] data)
        {
            int printableCount = 0;
            foreach (byte b in data)
            {
                if (b >= 32 && b <= 126) printableCount++;
            }
            Console.WriteLine($"可打印ASCII字符: {printableCount}/{data.Length} ({(printableCount * 100.0 / data.Length):0.00}%)");
            
            // 检查是否为压缩数据
            Console.WriteLine($"是否可能为GZIP: {(data.Length >= 2 && data[0] == 0x1F && data[1] == 0x8B ? "是" : "否")}");
            
            // 检查常见魔术字节
            if (data.Length >= 4)
            {
                Console.WriteLine($"前4字节: {BitConverter.ToString(data, 0, 4)}");
                Console.WriteLine($"前4字节(ASCII): {Encoding.ASCII.GetString(data, 0, 4)}");
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
