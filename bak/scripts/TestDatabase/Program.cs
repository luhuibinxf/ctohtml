using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 深入分析加密数据 ===");
            Console.WriteLine();

            string encrypted = "kkzI4Gy8No7vpAZuMSLF4rfumYQ03P4PVD1O2DTNq2eqLHxmYb3uXVRWRC1d9NHnFbIUgN4RFN3Qr3FliE17T0u4oO1cwudk3U2Y0Wuxj/0=";
            Console.WriteLine($"加密数据: {encrypted}");
            
            byte[] data = Convert.FromBase64String(encrypted);
            Console.WriteLine($"Base64解码后: {data.Length} 字节");
            Console.WriteLine();

            // 尝试各种自定义加密方式
            Console.WriteLine("--- 尝试自定义加密方式 ---");
            
            // 方式1: 简单的字节偏移
            Console.WriteLine("【方式1】字节偏移解密:");
            for (int offset = 1; offset <= 255; offset++)
            {
                string result = ByteOffsetDecrypt(data, offset);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"  偏移量 {offset}: {result}");
                    return;
                }
            }
            Console.WriteLine("  未找到有效偏移量");
            Console.WriteLine();

            // 方式2: 字节加减交替
            Console.WriteLine("【方式2】字节加减交替解密:");
            for (int key = 1; key <= 100; key++)
            {
                string result = AlternatingAddSubtract(data, key);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"  密钥 {key}: {result}");
                    return;
                }
            }
            Console.WriteLine("  未找到有效密钥");
            Console.WriteLine();

            // 方式3: 按位反转
            Console.WriteLine("【方式3】按位反转解密:");
            string bitReversed = BitReverseDecrypt(data);
            Console.WriteLine($"  结果: {bitReversed}");
            Console.WriteLine($"  是否有效: {IsValidConnectionString(bitReversed)}");
            Console.WriteLine();

            // 方式4: 循环位移
            Console.WriteLine("【方式4】循环位移解密:");
            for (int shift = 1; shift <= 7; shift++)
            {
                string result = RotateRightDecrypt(data, shift);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"  右移 {shift} 位: {result}");
                    return;
                }
                result = RotateLeftDecrypt(data, shift);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"  左移 {shift} 位: {result}");
                    return;
                }
            }
            Console.WriteLine("  未找到有效位移");
            Console.WriteLine();

            // 方式5: 简单替换密码
            Console.WriteLine("【方式5】简单替换解密:");
            string substituted = SubstitutionDecrypt(data);
            Console.WriteLine($"  结果: {substituted}");
            Console.WriteLine($"  是否有效: {IsValidConnectionString(substituted)}");
            Console.WriteLine();

            // 方式6: 尝试所有可能的XOR密钥 (0-255)
            Console.WriteLine("【方式6】暴力破解XOR密钥 (0-255):");
            for (byte key = 0; key < 255; key++)
            {
                string result = XorDecrypt(data, key);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"  XOR密钥 0x{key:X2}: {result}");
                    return;
                }
            }
            Console.WriteLine("  未找到有效XOR密钥");
            Console.WriteLine();

            // 方式7: 双密钥XOR
            Console.WriteLine("【方式7】双密钥XOR解密:");
            for (byte k1 = 0; k1 <= 100; k1++)
            {
                for (byte k2 = 0; k2 <= 100; k2++)
                {
                    string result = DoubleXorDecrypt(data, k1, k2);
                    if (IsValidConnectionString(result))
                    {
                        Console.WriteLine($"  密钥 0x{k1:X2}, 0x{k2:X2}: {result}");
                        return;
                    }
                }
            }
            Console.WriteLine("  未找到有效双密钥");
            Console.WriteLine();

            // 方式8: 尝试字符串反转 + XOR
            Console.WriteLine("【方式8】字符串反转+XOR解密:");
            for (byte key = 0; key < 100; key++)
            {
                byte[] reversed = (byte[])data.Clone();
                Array.Reverse(reversed);
                string result = XorDecrypt(reversed, key);
                if (IsValidConnectionString(result))
                {
                    Console.WriteLine($"  反转+XOR密钥 0x{key:X2}: {result}");
                    return;
                }
            }
            Console.WriteLine("  未找到有效组合");
            Console.WriteLine();

            // 方式9: 尝试常见的连接字符串格式
            Console.WriteLine("【方式9】分析数据特征:");
            AnalyzeDataPatterns(data);

            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
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

        static string AlternatingAddSubtract(byte[] data, int key)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (i % 2 == 0)
                    result[i] = (byte)((data[i] - key + 256) % 256);
                else
                    result[i] = (byte)((data[i] + key) % 256);
            }
            return Encoding.UTF8.GetString(result);
        }

        static string BitReverseDecrypt(byte[] data)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = ReverseBits(data[i]);
            }
            return Encoding.UTF8.GetString(result);
        }

        static byte ReverseBits(byte b)
        {
            byte result = 0;
            for (int i = 0; i < 8; i++)
            {
                result = (byte)((result << 1) | ((b >> i) & 1));
            }
            return result;
        }

        static string RotateRightDecrypt(byte[] data, int shift)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(((data[i] >> shift) | (data[i] << (8 - shift))));
            }
            return Encoding.UTF8.GetString(result);
        }

        static string RotateLeftDecrypt(byte[] data, int shift)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(((data[i] << shift) | (data[i] >> (8 - shift))));
            }
            return Encoding.UTF8.GetString(result);
        }

        static string SubstitutionDecrypt(byte[] data)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(255 - data[i]);
            }
            return Encoding.UTF8.GetString(result);
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

        static void AnalyzeDataPatterns(byte[] data)
        {
            Console.WriteLine($"数据长度: {data.Length} 字节");
            
            // 检查是否有重复模式
            Console.WriteLine("前16字节: " + BitConverter.ToString(data, 0, Math.Min(16, data.Length)));
            Console.WriteLine("后16字节: " + BitConverter.ToString(data, Math.Max(0, data.Length - 16)));
            
            // 检查ASCII可打印字符比例
            int printableCount = 0;
            foreach (byte b in data)
            {
                if (b >= 32 && b <= 126) printableCount++;
            }
            Console.WriteLine($"可打印ASCII字符比例: {printableCount}/{data.Length} ({(printableCount * 100.0 / data.Length):0.00}%)");
            
            // 检查常见连接字符串关键字的字节值
            string[] keywords = { "Server", "Database", "User ID", "Password" };
            Console.WriteLine("关键字字节分析:");
            foreach (string keyword in keywords)
            {
                byte[] kwBytes = Encoding.ASCII.GetBytes(keyword);
                Console.WriteLine($"  {keyword}: {BitConverter.ToString(kwBytes)}");
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
