using System;
using System.Data;
using System.Data.SqlClient;
using DbProcedureCaller.Core;
using DbProcedureCaller.Config;
using DbProcedureCaller.Utils;

namespace DbProcedureCaller.Services
{
    public class TokenService
    {
        /// <summary>
        /// 生成访问Token
        /// </summary>
        public string GenerateToken(int userId, int expireHours = 24)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法生成Token");
                return null;
            }

            try
            {
                string token = SecurityHelper.GenerateAccessToken();
                DateTime expireTime = DateTime.Now.AddHours(expireHours);

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = "INSERT INTO TJFX_ACCESS_TOKEN (TOKEN, USER_ID, EXPIRE_TIME, IS_USED, CREATED_TIME) VALUES (@token, @userId, @expireTime, 0, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@token", token);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@expireTime", expireTime);
                        cmd.ExecuteNonQuery();
                    }
                }

                LogHelper.LogInfo($"Token生成成功，用户ID: {userId}, Token: {token}");
                return token;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "生成Token失败");
                return null;
            }
        }

        /// <summary>
        /// 验证Token并返回用户ID
        /// </summary>
        public int? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法验证Token");
                return null;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = "SELECT ID, USER_ID, EXPIRE_TIME, IS_USED FROM TJFX_ACCESS_TOKEN WHERE TOKEN = @token";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@token", token);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int isUsed = reader["IS_USED"] != DBNull.Value ? Convert.ToInt32(reader["IS_USED"]) : 0;
                                if (isUsed == 1)
                                {
                                    LogHelper.LogWarning($"Token已被使用: {token}");
                                    return null;
                                }

                                DateTime expireTime = reader["EXPIRE_TIME"] != DBNull.Value ? Convert.ToDateTime(reader["EXPIRE_TIME"]) : DateTime.MinValue;
                                if (expireTime < DateTime.Now)
                                {
                                    LogHelper.LogWarning($"Token已过期: {token}");
                                    return null;
                                }

                                int userId = reader["USER_ID"] != DBNull.Value ? Convert.ToInt32(reader["USER_ID"]) : 0;
                                int tokenId = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;

                                // 标记为已使用
                                MarkTokenAsUsed(conn, tokenId);

                                return userId;
                            }
                        }
                    }
                }

                LogHelper.LogWarning($"Token不存在: {token}");
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "验证Token失败");
                return null;
            }
        }

        /// <summary>
        /// 标记Token为已使用
        /// </summary>
        private void MarkTokenAsUsed(SqlConnection conn, int tokenId)
        {
            try
            {
                string sql = "UPDATE TJFX_ACCESS_TOKEN SET IS_USED = 1, USED_TIME = GETDATE() WHERE ID = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", tokenId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "标记Token失败");
            }
        }

        /// <summary>
        /// 根据用户ID获取用户名
        /// </summary>
        public string GetUsernameById(int userId)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                return null;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = "SELECT YHM FROM TJYHB WHERE ID = @id AND SFY = 1";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", userId);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取用户名失败");
                return null;
            }
        }

        /// <summary>
        /// 获取Token列表
        /// </summary>
        public string GetTokensJson()
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法获取Token列表");
                return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT t.ID, t.TOKEN, t.USER_ID, y.YHM as username, 
                               t.EXPIRE_TIME, t.IS_USED, t.CREATED_TIME, t.USED_TIME
                        FROM TJFX_ACCESS_TOKEN t
                        LEFT JOIN TJYHB y ON t.USER_ID = y.ID
                        ORDER BY t.CREATED_TIME DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var tokens = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                var token = new System.Collections.Generic.Dictionary<string, object>();
                                token["id"] = reader["ID"];
                                token["token"] = reader["TOKEN"]?.ToString();
                                token["userId"] = reader["USER_ID"];
                                token["username"] = reader["username"]?.ToString();
                                token["expireTime"] = reader["EXPIRE_TIME"]?.ToString();
                                token["isUsed"] = Convert.ToInt32(reader["IS_USED"]) == 1 ? "是" : "否";
                                token["createdTime"] = reader["CREATED_TIME"]?.ToString();
                                token["usedTime"] = reader["USED_TIME"]?.ToString();
                                tokens.Add(token);
                            }

                            return "{\"success\": true, \"data\": " + Newtonsoft.Json.JsonConvert.SerializeObject(tokens) + "}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取Token列表失败");
                return "{\"success\": false, \"error\": \"" + ex.Message + "\"}";
            }
        }

        /// <summary>
        /// 删除Token
        /// </summary>
        public bool DeleteToken(int tokenId)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                return false;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = "DELETE FROM TJFX_ACCESS_TOKEN WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", tokenId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "删除Token失败");
                return false;
            }
        }
    }
}
