using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DbProcedureCaller.Core;

namespace DbProcedureCaller.Config
{
    public static class ConnectionStrings
    {
        private static string _connectionString = string.Empty;
        private static readonly object _lock = new object();
        
        private static readonly byte[][] _possibleKeys = new byte[][]
        {
            new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF },
            new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F },
            new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
            new byte[] { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50 },
            new byte[] { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78, 0x89, 0x9A, 0xAB, 0xBC, 0xCD, 0xDE, 0xEF, 0xF0 },
            new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 },
        };

        public static string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(_connectionString))
                return _connectionString;

            lock (_lock)
            {
                if (!string.IsNullOrEmpty(_connectionString))
                    return _connectionString;

                string configFile = GetConfigFilePath();

                if (File.Exists(configFile))
                {
                    try
                    {
                        string content = File.ReadAllText(configFile).Trim();
                        LogHelper.LogInfo($"配置文件路径: {configFile}");
                        LogHelper.LogInfo($"配置文件内容长度: {content.Length}");

                        string decrypted = TryDecrypt(content);
                        if (!string.IsNullOrEmpty(decrypted) && IsValidConnectionString(decrypted))
                        {
                            _connectionString = decrypted;
                            LogHelper.LogInfo("配置文件解密成功");
                            return _connectionString;
                        }

                        if (IsValidConnectionString(content))
                        {
                            _connectionString = content;
                            LogHelper.LogInfo("配置文件为明文，加载成功");
                            return _connectionString;
                        }

                        LogHelper.LogInfo("配置文件解密失败或内容无效，弹出配置窗口");
                        _connectionString = PromptForConnectionString();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError($"加载配置文件失败: {ex.Message}");
                        _connectionString = PromptForConnectionString();
                    }
                }
                else
                {
                    LogHelper.LogInfo($"配置文件不存在: {configFile}");
                    _connectionString = PromptForConnectionString();
                }

                return _connectionString ?? string.Empty;
            }
        }

        private static string TryDecrypt(string encryptedContent)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(encryptedContent);
                LogHelper.LogInfo($"Base64解码后字节数: {cipherBytes.Length}");

                foreach (byte[] key in _possibleKeys)
                {
                    try
                    {
                        string result = AesDecrypt(cipherBytes, key, key);
                        if (IsValidConnectionString(result))
                        {
                            LogHelper.LogInfo($"使用密钥 {BitConverter.ToString(key).Substring(0, 20)}... 解密成功");
                            return result;
                        }
                    }
                    catch { }
                }

                LogHelper.LogInfo("尝试所有密钥解密均失败");
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"解密过程异常: {ex.Message}");
                return string.Empty;
            }
        }

        private static string AesDecrypt(byte[] cipherBytes, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private static string AesEncrypt(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private static string GetConfigFilePath()
        {
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.dat");
            if (!File.Exists(configFile))
            {
                configFile = @"d:\AI\tran\config.dat";
                LogHelper.LogInfo($"尝试备用路径: {configFile}");
            }
            if (!File.Exists(configFile))
            {
                string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                configFile = Path.Combine(exeDir, "..", "..", "..", "..", "config.dat");
                configFile = Path.GetFullPath(configFile);
                LogHelper.LogInfo($"尝试相对路径: {configFile}");
            }
            return configFile;
        }

        public static string PromptForConnectionString()
        {
            string result = string.Empty;

            try
            {
                string configFile = GetConfigFilePath();
                if (File.Exists(configFile))
                {
                    result = File.ReadAllText(configFile).Trim();
                    if (!string.IsNullOrEmpty(result))
                    {
                        try
                        {
                            byte[] data = Convert.FromBase64String(result);
                            result = Encoding.UTF8.GetString(data);
                        }
                        catch
                        {
                        }
                    }
                }
                
                if (string.IsNullOrEmpty(result))
                {
                    result = "Server=127.0.0.1;Database=WiNEX_PACS;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;";
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"加载配置失败: {ex.Message}");
                result = "Server=127.0.0.1;Database=WiNEX_PACS;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;";
            }

            return result;
        }

        private static void SaveConnectionString(string connectionString)
        {
            try
            {
                string configFile = GetConfigFilePath();
                byte[] key = _possibleKeys[0];
                string encrypted = AesEncrypt(connectionString, key, key);
                File.WriteAllText(configFile, encrypted);
                LogHelper.LogInfo("数据库连接字符串已加密保存到: " + configFile);
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"保存连接字符串失败: {ex.Message}");
                try
                {
                    string configFile = GetConfigFilePath();
                    File.WriteAllText(configFile, connectionString);
                    LogHelper.LogInfo("加密保存失败，已使用明文保存到: " + configFile);
                }
                catch { }
            }
        }

        public static bool HasConnectionString()
        {
            return !string.IsNullOrEmpty(GetConnectionString());
        }

        public static void ClearCache()
        {
            lock (_lock)
            {
                _connectionString = string.Empty;
            }
        }

        public static void ResetToDefault()
        {
            lock (_lock)
            {
                _connectionString = string.Empty;
                string defaultConnectionString = "Server=127.0.0.1;Database=WiNEX_PACS;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;";
                SaveConnectionString(defaultConnectionString);
            }
        }

        private static bool IsValidConnectionString(string connectionString)
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
    }

    public static class AppConstants
    {
        public const string DefaultAdminUser = "lhbdb";
        public const string DefaultAdminPassword = "241023";
        public const string DefaultPort = "8081";
    }
}
