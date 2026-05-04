using System;
using System.Data;
using System.Data.SqlClient;
using DbProcedureCaller.Core;
using DbProcedureCaller.Config;

namespace DbProcedureCaller.Services
{
    public class PermissionService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        public string GetRolesJson()
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法获取角色列表");
                return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = "SELECT ID, ROLE_NAME, PERMISSIONS, DESCRIPTION, IS_ACTIVE FROM TJFX_ROLE ORDER BY ID";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var roles = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                var role = new System.Collections.Generic.Dictionary<string, object>();
                                role["id"] = reader["ID"];
                                role["roleName"] = reader["ROLE_NAME"]?.ToString();
                                role["permissions"] = reader["PERMISSIONS"]?.ToString();
                                role["description"] = reader["DESCRIPTION"]?.ToString();
                                role["isActive"] = Convert.ToInt32(reader["IS_ACTIVE"]) == 1 ? "是" : "否";
                                roles.Add(role);
                            }

                            return "{\"success\": true, \"data\": " + Newtonsoft.Json.JsonConvert.SerializeObject(roles) + "}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取角色列表失败");
                return "{\"success\": false, \"error\": \"" + ex.Message + "\"}";
            }
        }

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        public string GetUserRolesJson(int userId)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法获取用户角色");
                return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT r.ID, r.ROLE_NAME, r.DESCRIPTION
                        FROM TJFX_ROLE r
                        INNER JOIN TJFX_USER_ROLE ur ON r.ID = ur.ROLE_ID
                        WHERE ur.USER_ID = @userId AND r.IS_ACTIVE = 1";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var roles = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                var role = new System.Collections.Generic.Dictionary<string, object>();
                                role["id"] = reader["ID"];
                                role["roleName"] = reader["ROLE_NAME"]?.ToString();
                                role["description"] = reader["DESCRIPTION"]?.ToString();
                                roles.Add(role);
                            }

                            return "{\"success\": true, \"data\": " + Newtonsoft.Json.JsonConvert.SerializeObject(roles) + "}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取用户角色失败");
                return "{\"success\": false, \"error\": \"" + ex.Message + "\"}";
            }
        }

        /// <summary>
        /// 检查用户是否有特定权限
        /// </summary>
        public bool HasPermission(string username, string permission)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法检查权限");
                return false;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT r.PERMISSIONS
                        FROM TJYHB y
                        INNER JOIN TJFX_USER_ROLE ur ON y.ID = ur.USER_ID
                        INNER JOIN TJFX_ROLE r ON ur.ROLE_ID = r.ID
                        WHERE y.YHM = @username AND y.SFY = 1 AND r.IS_ACTIVE = 1";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string permissionsJson = reader["PERMISSIONS"]?.ToString();
                                if (!string.IsNullOrEmpty(permissionsJson))
                                {
                                    var permissions = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(permissionsJson);
                                    foreach (string p in permissions)
                                    {
                                        if (p == permission)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "检查权限失败");
            }

            return false;
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        public bool AssignRoleToUser(int userId, int roleId)
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
                    // 先检查是否已存在
                    string checkSql = "SELECT COUNT(*) FROM TJFX_USER_ROLE WHERE USER_ID = @userId AND ROLE_ID = @roleId";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@userId", userId);
                        checkCmd.Parameters.AddWithValue("@roleId", roleId);
                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            return true; // 已存在
                        }
                    }

                    string sql = "INSERT INTO TJFX_USER_ROLE (USER_ID, ROLE_ID, CREATED_TIME) VALUES (@userId, @roleId, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@roleId", roleId);
                        cmd.ExecuteNonQuery();
                        LogHelper.LogInfo($"为用户 {userId} 分配角色 {roleId} 成功");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "分配角色失败");
                return false;
            }
        }

        /// <summary>
        /// 移除用户角色
        /// </summary>
        public bool RemoveRoleFromUser(int userId, int roleId)
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
                    string sql = "DELETE FROM TJFX_USER_ROLE WHERE USER_ID = @userId AND ROLE_ID = @roleId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@roleId", roleId);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            LogHelper.LogInfo($"为用户 {userId} 移除角色 {roleId} 成功");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "移除角色失败");
            }

            return false;
        }

        /// <summary>
        /// 获取菜单配置
        /// </summary>
        public string GetMenusJson(string username = null)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法获取菜单");
                return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT ID, MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, IS_ACTIVE, ICON
                        FROM TJFX_MENU_CONFIG 
                        WHERE IS_ACTIVE = 1
                        ORDER BY PARENT_ID, SORT_ORDER";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var menus = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                var menu = new System.Collections.Generic.Dictionary<string, object>();
                                menu["id"] = reader["ID"];
                                menu["menuName"] = reader["MENU_NAME"]?.ToString();
                                menu["menuUrl"] = reader["MENU_URL"]?.ToString();
                                menu["parentId"] = reader["PARENT_ID"] != DBNull.Value ? Convert.ToInt32(reader["PARENT_ID"]) : (int?)null;
                                menu["permissionLevel"] = reader["PERMISSION_LEVEL"];
                                menu["sortOrder"] = reader["SORT_ORDER"];
                                menu["icon"] = reader["ICON"]?.ToString();
                                menus.Add(menu);
                            }

                            return "{\"success\": true, \"data\": " + Newtonsoft.Json.JsonConvert.SerializeObject(menus) + "}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取菜单失败");
                return "{\"success\": false, \"error\": \"" + ex.Message + "\"}";
            }
        }

        public string GetMenusByUserJson(string username)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法获取菜单");
                return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT DISTINCT mc.ID, mc.MENU_NAME, mc.MENU_URL, mc.PARENT_ID, mc.PERMISSION_LEVEL, mc.SORT_ORDER, mc.ICON
                        FROM TJFX_MENU_CONFIG mc
                        LEFT JOIN (
                            SELECT r.PERMISSIONS
                            FROM TJYHB y
                            INNER JOIN TJFX_USER_ROLE ur ON y.ID = ur.USER_ID
                            INNER JOIN TJFX_ROLE r ON ur.ROLE_ID = r.ID
                            WHERE y.YHM = @username AND y.SFY = 1 AND r.IS_ACTIVE = 1
                        ) rp ON 1=1
                        WHERE mc.IS_ACTIVE = 1
                        AND (mc.PERMISSION_LEVEL = 0 OR mc.PERMISSION_LEVEL IS NULL)
                        ORDER BY mc.PARENT_ID, mc.SORT_ORDER";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var menus = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                var menu = new System.Collections.Generic.Dictionary<string, object>();
                                menu["id"] = reader["ID"];
                                menu["menuName"] = reader["MENU_NAME"]?.ToString();
                                menu["menuUrl"] = reader["MENU_URL"]?.ToString();
                                menu["parentId"] = reader["PARENT_ID"] != DBNull.Value ? Convert.ToInt32(reader["PARENT_ID"]) : (int?)null;
                                menu["permissionLevel"] = reader["PERMISSION_LEVEL"];
                                menu["sortOrder"] = reader["SORT_ORDER"];
                                menu["icon"] = reader["ICON"]?.ToString();
                                menus.Add(menu);
                            }

                            return "{\"success\": true, \"data\": " + Newtonsoft.Json.JsonConvert.SerializeObject(menus) + "}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取用户菜单失败");
                return "{\"success\": false, \"error\": \"" + ex.Message + "\"}";
            }
        }

        public bool HasAnalysisPermission(string username, string analysisType)
        {
            return HasPermission(username, "data_analysis");
        }
    }
}
