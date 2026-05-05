using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using DbProcedureCaller.Core;
using DbProcedureCaller.Config;

namespace DbProcedureCaller.Services
{
    public class StatConfigService
    {
        private static bool _tableChecked = false;
        private readonly string _connectionString;

        public StatConfigService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StatConfigService()
        {
            _connectionString = ConnectionStrings.GetConnectionString();
        }

        private void EnsureTableExists(SqlConnection conn)
        {
            if (_tableChecked) return;

            try
            {
                string checkTableSql = "SELECT COUNT(*) FROM sys.tables WHERE name = 'TJ_TJFX_CONFIG'";
                using (SqlCommand checkCmd = new SqlCommand(checkTableSql, conn))
                {
                    int tableExists = (int)checkCmd.ExecuteScalar();

                    if (tableExists == 0)
                    {
                        string createTableSql = @"
                            CREATE TABLE TJ_TJFX_CONFIG (
                                ID VARCHAR(50) PRIMARY KEY,
                                NAME VARCHAR(100) NOT NULL,
                                DESCRIPTION VARCHAR(500),
                                ICON VARCHAR(50) DEFAULT 'fa-chart-bar',
                                SORT_ORDER INT DEFAULT 100,
                                TEMPLATE_NAME VARCHAR(100) NOT NULL,
                                PROC_NAME VARCHAR(200) NOT NULL,
                                HAS_DETAIL_PROC BIT DEFAULT 0,
                                DETAIL_PROC_NAME VARCHAR(200),
                                IS_ENABLED BIT DEFAULT 1,
                                PARAMETERS TEXT,
                                COLUMNS TEXT,
                                PERMISSION VARCHAR(100),
                                CREATE_TIME DATETIME DEFAULT GETDATE(),
                                UPDATE_TIME DATETIME DEFAULT GETDATE()
                            )";

                        using (SqlCommand createCmd = new SqlCommand(createTableSql, conn))
                        {
                            createCmd.ExecuteNonQuery();
                            LogHelper.LogInfo("TJ_TJFX_CONFIG表创建成功");
                        }
                    }
                    else
                    {
                        EnsureColumnExists(conn, "TEMPLATE_NAME", "VARCHAR(100) DEFAULT ''");
                        EnsureColumnExists(conn, "HAS_DETAIL_PROC", "BIT DEFAULT 0");
                        EnsureColumnExists(conn, "DETAIL_PROC_NAME", "VARCHAR(200)");
                    }
                }

                _tableChecked = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "创建TJ_TJFX_CONFIG表失败");
            }
        }

        public string SaveConfig(string jsonData)
        {
            try
            {
                dynamic config = JsonConvert.DeserializeObject(jsonData);

                using (SqlConnection conn = DatabaseConnection.GetConnection(_connectionString))
                {
                    EnsureTableExists(conn);

                    string id = config.id != null ? config.id.ToString() : Guid.NewGuid().ToString();

                    string sql = @"
                        MERGE INTO TJ_TJFX_CONFIG AS Target
                        USING (VALUES (@ID, @NAME, @DESCRIPTION, @ICON, @SORT_ORDER, @TEMPLATE_NAME, @PROC_NAME, @HAS_DETAIL_PROC, @DETAIL_PROC_NAME, @IS_ENABLED, @PARAMETERS, @COLUMNS, @PERMISSION, @UPDATE_TIME))
                        AS Source (ID, NAME, DESCRIPTION, ICON, SORT_ORDER, TEMPLATE_NAME, PROC_NAME, HAS_DETAIL_PROC, DETAIL_PROC_NAME, IS_ENABLED, PARAMETERS, COLUMNS, PERMISSION, UPDATE_TIME)
                        ON Target.ID = Source.ID
                        WHEN MATCHED THEN
                            UPDATE SET 
                                NAME = Source.NAME,
                                DESCRIPTION = Source.DESCRIPTION,
                                ICON = Source.ICON,
                                SORT_ORDER = Source.SORT_ORDER,
                                TEMPLATE_NAME = Source.TEMPLATE_NAME,
                                PROC_NAME = Source.PROC_NAME,
                                HAS_DETAIL_PROC = Source.HAS_DETAIL_PROC,
                                DETAIL_PROC_NAME = Source.DETAIL_PROC_NAME,
                                IS_ENABLED = Source.IS_ENABLED,
                                PARAMETERS = Source.PARAMETERS,
                                COLUMNS = Source.COLUMNS,
                                PERMISSION = Source.PERMISSION,
                                UPDATE_TIME = Source.UPDATE_TIME
                        WHEN NOT MATCHED THEN
                            INSERT (ID, NAME, DESCRIPTION, ICON, SORT_ORDER, TEMPLATE_NAME, PROC_NAME, HAS_DETAIL_PROC, DETAIL_PROC_NAME, IS_ENABLED, PARAMETERS, COLUMNS, PERMISSION, CREATE_TIME, UPDATE_TIME)
                            VALUES (@ID, @NAME, @DESCRIPTION, @ICON, @SORT_ORDER, @TEMPLATE_NAME, @PROC_NAME, @HAS_DETAIL_PROC, @DETAIL_PROC_NAME, @IS_ENABLED, @PARAMETERS, @COLUMNS, @PERMISSION, GETDATE(), @UPDATE_TIME);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@NAME", config.name?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@DESCRIPTION", config.description?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@ICON", config.icon?.ToString() ?? "fa-chart-bar");
                        cmd.Parameters.AddWithValue("@SORT_ORDER", config.sortOrder != null ? (int)config.sortOrder : 100);
                        cmd.Parameters.AddWithValue("@TEMPLATE_NAME", config.templateName?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@PROC_NAME", config.procName?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@HAS_DETAIL_PROC", config.hasDetailProc != null && (bool)config.hasDetailProc);
                        cmd.Parameters.AddWithValue("@DETAIL_PROC_NAME", config.detailProcName?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@IS_ENABLED", config.isActive != null && (bool)config.isActive);
                        cmd.Parameters.AddWithValue("@PARAMETERS", config.parameters != null ? JsonConvert.SerializeObject(config.parameters) : "[]");
                        cmd.Parameters.AddWithValue("@COLUMNS", config.columns != null ? JsonConvert.SerializeObject(config.columns) : "[]");
                        cmd.Parameters.AddWithValue("@PERMISSION", config.permission?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@UPDATE_TIME", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }

                    LogHelper.LogInfo($"统计配置保存成功: {config.name}");
                    return "{\"success\": true, \"message\": \"配置保存成功\", \"id\": \"" + id + "\"}";
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "保存统计配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        public string GetConfigs(string permission = "")
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(_connectionString))
                {
                    EnsureTableExists(conn);

                    string sql = "SELECT * FROM TJ_TJFX_CONFIG WHERE IS_ENABLED = 1";
                    if (!string.IsNullOrEmpty(permission))
                    {
                        sql += " AND (PERMISSION = '' OR PERMISSION IS NULL OR PERMISSION = @PERMISSION)";
                    }
                    sql += " ORDER BY SORT_ORDER ASC, CREATE_TIME DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(permission))
                        {
                            cmd.Parameters.AddWithValue("@PERMISSION", permission);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<dynamic> configs = new List<dynamic>();
                            while (reader.Read())
                            {
                                dynamic config = new System.Dynamic.ExpandoObject();
                                config.id = reader["ID"].ToString();
                                config.name = reader["NAME"].ToString();
                                config.description = reader["DESCRIPTION"].ToString();
                                config.icon = reader["ICON"].ToString();
                                config.sortOrder = reader["SORT_ORDER"] != DBNull.Value ? (int)reader["SORT_ORDER"] : 100;
                                config.templateName = reader["TEMPLATE_NAME"].ToString();
                                config.procName = reader["PROC_NAME"].ToString();
                                config.mainProcName = reader["PROC_NAME"].ToString().Replace("_Detail", "");
                                config.hasDetailProc = reader["HAS_DETAIL_PROC"] != DBNull.Value && (bool)reader["HAS_DETAIL_PROC"];
                                config.detailProcName = reader["DETAIL_PROC_NAME"]?.ToString() ?? "";
                                config.parameters = reader["PARAMETERS"] != DBNull.Value ? reader["PARAMETERS"].ToString() : "[]";
                                config.columns = reader["COLUMNS"] != DBNull.Value ? reader["COLUMNS"].ToString() : "[]";
                                config.permission = reader["PERMISSION"] != DBNull.Value ? reader["PERMISSION"].ToString() : "";
                                config.createTime = reader["CREATE_TIME"] != DBNull.Value ? ((DateTime)reader["CREATE_TIME"]).ToString("yyyy-MM-dd HH:mm:ss") : "";
                                config.updateTime = reader["UPDATE_TIME"] != DBNull.Value ? ((DateTime)reader["UPDATE_TIME"]).ToString("yyyy-MM-dd HH:mm:ss") : "";

                                configs.Add(config);
                            }

                            return "{\"success\": true, \"data\": " + JsonConvert.SerializeObject(configs) + "}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取统计配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        private List<string> _upgradeMessages = new List<string>();

        private void EnsureColumnExists(SqlConnection conn, string columnName, string columnType)
        {
            try
            {
                string checkColumnSql = @"
                    SELECT COUNT(*) 
                    FROM sys.columns 
                    WHERE object_id = OBJECT_ID('TJ_TJFX_CONFIG') 
                    AND name = @ColumnName";
                
                using (SqlCommand checkCmd = new SqlCommand(checkColumnSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@ColumnName", columnName);
                    int columnExists = (int)checkCmd.ExecuteScalar();
                    
                    if (columnExists == 0)
                    {
                        string addColumnSql = $"ALTER TABLE TJ_TJFX_CONFIG ADD {columnName} {columnType}";
                        using (SqlCommand addCmd = new SqlCommand(addColumnSql, conn))
                        {
                            addCmd.ExecuteNonQuery();
                            string msg = $"已自动升级数据库表，添加新字段: {columnName}";
                            LogHelper.LogInfo(msg);
                            _upgradeMessages.Add(msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, $"添加列 {columnName} 失败");
            }
        }

        public string GetUpgradeMessages()
        {
            if (_upgradeMessages.Count == 0)
                return "{\"success\": true, \"data\": []}";
            
            string result = "{\"success\": true, \"data\": [" + 
                string.Join(",", _upgradeMessages.Select(m => $"\"{m.Replace("\"", "'")}\"")) + 
                "]}";
            _upgradeMessages.Clear();
            return result;
        }

        public string DeleteConfig(string configId)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(_connectionString))
                {
                    EnsureTableExists(conn);

                    string sql = "DELETE FROM TJ_TJFX_CONFIG WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", configId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            LogHelper.LogInfo($"统计配置删除成功: {configId}");
                            return "{\"success\": true, \"message\": \"删除成功\"}";
                        }
                        else
                        {
                            return "{\"success\": false, \"error\": \"配置不存在\"}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "删除统计配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        public string GetConfigById(string configId)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(_connectionString))
                {
                    EnsureTableExists(conn);

                    LogHelper.LogInfo($"GetConfigById - 尝试查询配置, configId: '{configId}', 长度: {configId.Length}");

                    string sql = "SELECT * FROM TJ_TJFX_CONFIG WHERE (ID = @ID OR NAME = @NAME) AND IS_ENABLED = 1";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", configId);
                        cmd.Parameters.AddWithValue("@NAME", configId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dynamic config = new System.Dynamic.ExpandoObject();
                                config.id = reader["ID"].ToString();
                                config.name = reader["NAME"].ToString();
                                config.description = reader["DESCRIPTION"].ToString();
                                config.icon = reader["ICON"].ToString();
                                config.sortOrder = reader["SORT_ORDER"] != DBNull.Value ? (int)reader["SORT_ORDER"] : 100;
                                config.templateName = reader["TEMPLATE_NAME"].ToString();
                                config.procName = reader["PROC_NAME"].ToString();
                                config.mainProcName = reader["PROC_NAME"].ToString().Replace("_Detail", "");
                                config.hasDetailProc = reader["HAS_DETAIL_PROC"] != DBNull.Value && (bool)reader["HAS_DETAIL_PROC"];
                                config.detailProcName = reader["DETAIL_PROC_NAME"]?.ToString() ?? "";
                                config.parameters = reader["PARAMETERS"] != DBNull.Value ? reader["PARAMETERS"].ToString() : "[]";
                                config.columns = reader["COLUMNS"] != DBNull.Value ? reader["COLUMNS"].ToString() : "[]";
                                config.permission = reader["PERMISSION"] != DBNull.Value ? reader["PERMISSION"].ToString() : "";

                                string result = "{\"success\": true, \"data\": " + JsonConvert.SerializeObject(config) + "}";
                                LogHelper.LogInfo($"GetConfigById成功 - configId: {configId}, parameters: {config.parameters}");
                                return result;
                            }
                            else
                            {
                                LogHelper.LogInfo($"GetConfigById失败 - configId: '{configId}', 原因: 配置不存在");
                                return "{\"success\": false, \"error\": \"配置不存在\"}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取统计配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        public string SaveParamConfig(string configId, string parametersJson)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(_connectionString))
                {
                    EnsureTableExists(conn);

                    string sql = "UPDATE TJ_TJFX_CONFIG SET PARAMETERS = @PARAMETERS, UPDATE_TIME = GETDATE() WHERE ID = @ID OR NAME = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", configId);
                        cmd.Parameters.AddWithValue("@PARAMETERS", parametersJson ?? "[]");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            LogHelper.LogInfo($"参数配置保存成功: {configId}");
                            return "{\"success\": true, \"message\": \"参数配置保存成功\"}";
                        }
                        else
                        {
                            return "{\"success\": false, \"error\": \"配置不存在\"}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "保存参数配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }
    }
}