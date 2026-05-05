using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using DbProcedureCaller.Core;
using DbProcedureCaller.Config;

namespace DbProcedureCaller.Services
{
    public class SystemConfigService
    {
        private static bool _tableChecked = false;
        private readonly string _connectionString;

        public SystemConfigService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SystemConfigService()
        {
            _connectionString = ConnectionStrings.GetConnectionString();
        }

        private void EnsureTableExists(SqlConnection conn)
        {
            if (_tableChecked) return;

            try
            {
                string checkTableSql = "SELECT COUNT(*) FROM sys.tables WHERE name = 'SYSTEM_CONFIG'";
                using (SqlCommand checkCmd = new SqlCommand(checkTableSql, conn))
                {
                    int tableExists = (int)checkCmd.ExecuteScalar();

                    if (tableExists == 0)
                    {
                        string createTableSql = @"
                            CREATE TABLE SYSTEM_CONFIG (
                                ID VARCHAR(50) PRIMARY KEY,
                                CONFIG_KEY VARCHAR(100) NOT NULL UNIQUE,
                                CONFIG_VALUE TEXT,
                                CONFIG_TYPE VARCHAR(20) DEFAULT 'STRING',
                                DESCRIPTION VARCHAR(500),
                                IS_ENABLED BIT DEFAULT 1,
                                CREATE_TIME DATETIME DEFAULT GETDATE(),
                                UPDATE_TIME DATETIME DEFAULT GETDATE()
                            )";

                        using (SqlCommand createCmd = new SqlCommand(createTableSql, conn))
                        {
                            createCmd.ExecuteNonQuery();
                            LogHelper.LogInfo("SYSTEM_CONFIG表创建成功");
                        }

                        InitializeDefaultConfigs(conn);
                    }
                }

                _tableChecked = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "创建SYSTEM_CONFIG表失败");
            }
        }

        private void InitializeDefaultConfigs(SqlConnection conn)
        {
            List<SystemConfigItem> defaultConfigs = new List<SystemConfigItem>
            {
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "CACHE_ENABLED", ConfigValue = "true", ConfigType = "BOOLEAN", Description = "是否启用前端缓存" },
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "CACHE_EXPIRE_MINUTES", ConfigValue = "5", ConfigType = "INTEGER", Description = "缓存有效期（分钟）" },
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "DEFAULT_REFRESH_INTERVAL", ConfigValue = "30", ConfigType = "INTEGER", Description = "默认自动刷新间隔（秒）" },
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "DEFAULT_CHART_TYPE", ConfigValue = "bar", ConfigType = "STRING", Description = "默认图表类型" },
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "CHART_ANIMATION_DURATION", ConfigValue = "800", ConfigType = "INTEGER", Description = "图表动画时长（毫秒）" },
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "AUTO_REFRESH_ENABLED", ConfigValue = "false", ConfigType = "BOOLEAN", Description = "是否默认启用自动刷新" },
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "DATA_SYNC_INTERVAL", ConfigValue = "60", ConfigType = "INTEGER", Description = "数据同步间隔（分钟）" },
                new SystemConfigItem { Id = Guid.NewGuid().ToString(), ConfigKey = "MAX_RESULTS_LIMIT", ConfigValue = "1000", ConfigType = "INTEGER", Description = "最大查询结果限制" }
            };

            foreach (var config in defaultConfigs)
            {
                string insertSql = @"
                    INSERT INTO SYSTEM_CONFIG (ID, CONFIG_KEY, CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION, IS_ENABLED, CREATE_TIME, UPDATE_TIME)
                    VALUES (@Id, @ConfigKey, @ConfigValue, @ConfigType, @Description, 1, GETDATE(), GETDATE())";

                using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", config.Id);
                    cmd.Parameters.AddWithValue("@ConfigKey", config.ConfigKey);
                    cmd.Parameters.AddWithValue("@ConfigValue", config.ConfigValue);
                    cmd.Parameters.AddWithValue("@ConfigType", config.ConfigType);
                    cmd.Parameters.AddWithValue("@Description", config.Description ?? "");
                    cmd.ExecuteNonQuery();
                }
            }

            LogHelper.LogInfo("默认系统配置初始化成功");
        }

        public List<SystemConfigItem> GetAllConfigs()
        {
            List<SystemConfigItem> configs = new List<SystemConfigItem>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    EnsureTableExists(conn);

                    string sql = "SELECT ID, CONFIG_KEY, CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION, IS_ENABLED, CREATE_TIME, UPDATE_TIME FROM SYSTEM_CONFIG WHERE IS_ENABLED = 1 ORDER BY CONFIG_KEY";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                configs.Add(new SystemConfigItem
                                {
                                    Id = reader["ID"].ToString(),
                                    ConfigKey = reader["CONFIG_KEY"].ToString(),
                                    ConfigValue = reader["CONFIG_VALUE"].ToString(),
                                    ConfigType = reader["CONFIG_TYPE"].ToString(),
                                    Description = reader["DESCRIPTION"].ToString(),
                                    IsEnabled = reader["IS_ENABLED"] != DBNull.Value && (bool)reader["IS_ENABLED"],
                                    CreateTime = reader["CREATE_TIME"] != DBNull.Value ? (DateTime)reader["CREATE_TIME"] : DateTime.MinValue,
                                    UpdateTime = reader["UPDATE_TIME"] != DBNull.Value ? (DateTime)reader["UPDATE_TIME"] : DateTime.MinValue
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取系统配置失败");
            }

            return configs;
        }

        public SystemConfigItem GetConfigByKey(string configKey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    EnsureTableExists(conn);

                    string sql = "SELECT ID, CONFIG_KEY, CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION, IS_ENABLED, CREATE_TIME, UPDATE_TIME FROM SYSTEM_CONFIG WHERE CONFIG_KEY = @ConfigKey AND IS_ENABLED = 1";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ConfigKey", configKey);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new SystemConfigItem
                                {
                                    Id = reader["ID"].ToString(),
                                    ConfigKey = reader["CONFIG_KEY"].ToString(),
                                    ConfigValue = reader["CONFIG_VALUE"].ToString(),
                                    ConfigType = reader["CONFIG_TYPE"].ToString(),
                                    Description = reader["DESCRIPTION"].ToString(),
                                    IsEnabled = reader["IS_ENABLED"] != DBNull.Value && (bool)reader["IS_ENABLED"],
                                    CreateTime = reader["CREATE_TIME"] != DBNull.Value ? (DateTime)reader["CREATE_TIME"] : DateTime.MinValue,
                                    UpdateTime = reader["UPDATE_TIME"] != DBNull.Value ? (DateTime)reader["UPDATE_TIME"] : DateTime.MinValue
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取系统配置失败: " + configKey);
            }

            return null;
        }

        public string GetConfigValue(string configKey, string defaultValue = "")
        {
            var config = GetConfigByKey(configKey);
            return config?.ConfigValue ?? defaultValue;
        }

        public bool GetConfigValueBoolean(string configKey, bool defaultValue = false)
        {
            var value = GetConfigValue(configKey);
            if (string.IsNullOrEmpty(value)) return defaultValue;
            return value.Equals("true", StringComparison.OrdinalIgnoreCase) || value == "1";
        }

        public int GetConfigValueInt(string configKey, int defaultValue = 0)
        {
            var value = GetConfigValue(configKey);
            if (string.IsNullOrEmpty(value)) return defaultValue;
            if (int.TryParse(value, out int result)) return result;
            return defaultValue;
        }

        public bool UpdateConfig(string configKey, string configValue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    EnsureTableExists(conn);

                    string sql = "UPDATE SYSTEM_CONFIG SET CONFIG_VALUE = @ConfigValue, UPDATE_TIME = GETDATE() WHERE CONFIG_KEY = @ConfigKey";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ConfigKey", configKey);
                        cmd.Parameters.AddWithValue("@ConfigValue", configValue);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            LogHelper.LogInfo("系统配置更新成功: " + configKey);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "更新系统配置失败: " + configKey);
            }

            return false;
        }

        public bool UpdateConfigs(Dictionary<string, string> configs)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    EnsureTableExists(conn);

                    foreach (var kvp in configs)
                    {
                        string sql = "UPDATE SYSTEM_CONFIG SET CONFIG_VALUE = @ConfigValue, UPDATE_TIME = GETDATE() WHERE CONFIG_KEY = @ConfigKey";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ConfigKey", kvp.Key);
                            cmd.Parameters.AddWithValue("@ConfigValue", kvp.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LogHelper.LogInfo("批量更新系统配置成功");
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "批量更新系统配置失败");
                return false;
            }
        }

        public bool AddConfig(SystemConfigItem config)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    EnsureTableExists(conn);

                    string sql = @"
                        INSERT INTO SYSTEM_CONFIG (ID, CONFIG_KEY, CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION, IS_ENABLED, CREATE_TIME, UPDATE_TIME)
                        VALUES (@Id, @ConfigKey, @ConfigValue, @ConfigType, @Description, @IsEnabled, GETDATE(), GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", config.Id ?? Guid.NewGuid().ToString());
                        cmd.Parameters.AddWithValue("@ConfigKey", config.ConfigKey);
                        cmd.Parameters.AddWithValue("@ConfigValue", config.ConfigValue);
                        cmd.Parameters.AddWithValue("@ConfigType", config.ConfigType ?? "STRING");
                        cmd.Parameters.AddWithValue("@Description", config.Description ?? "");
                        cmd.Parameters.AddWithValue("@IsEnabled", config.IsEnabled ? 1 : 0);

                        cmd.ExecuteNonQuery();
                        LogHelper.LogInfo("系统配置添加成功: " + config.ConfigKey);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "添加系统配置失败: " + config.ConfigKey);
                return false;
            }
        }

        public bool DeleteConfig(string configKey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    EnsureTableExists(conn);

                    string sql = "UPDATE SYSTEM_CONFIG SET IS_ENABLED = 0, UPDATE_TIME = GETDATE() WHERE CONFIG_KEY = @ConfigKey";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ConfigKey", configKey);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            LogHelper.LogInfo("系统配置删除成功: " + configKey);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "删除系统配置失败: " + configKey);
            }

            return false;
        }
    }

    public class SystemConfigItem
    {
        public string Id { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigType { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}