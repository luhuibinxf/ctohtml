using System;
using System.Data;
using System.Data.SqlClient;
using DbProcedureCaller.Core;
using DbProcedureCaller.Config;
using DbProcedureCaller.Utils;

namespace DbProcedureCaller.Services
{
    public class UserService
    {
        private static readonly object _lock = new object();
        private static bool _tableChecked = false;

        public bool ValidateUser(string username, string password)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法验证用户");
                return false;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    EnsureTableExists(conn);
                    
                    // 先获取用户信息
                    string sql = "SELECT YKL, SALT, QX, SFY FROM TJYHB WHERE YHM = @username";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 检查用户是否启用
                                int isActive = reader["SFY"] != DBNull.Value ? Convert.ToInt32(reader["SFY"]) : 1;
                                if (isActive != 1)
                                {
                                    LogHelper.LogWarning($"用户 {username} 已被禁用");
                                    return false;
                                }

                                string storedHashedPassword = reader["YKL"]?.ToString();
                                string salt = reader["SALT"]?.ToString();

                                // 如果没有盐值，说明是老密码（明文），需要迁移
                                if (string.IsNullOrEmpty(salt))
                                {
                                    // 明文密码验证
                                    if (storedHashedPassword == password)
                                    {
                                        // 先保存用户信息
                                        string tempUsername = username;
                                        string tempPassword = password;
                                        // 自动迁移为加密密码（使用独立连接）
                                        MigratePassword(tempUsername, tempPassword);
                                        UpdateLastLoginInfo(tempUsername);
                                        return true;
                                    }
                                    return false;
                                }

                                // 加密密码验证
                                bool isValid = SecurityHelper.VerifyPassword(password, salt, storedHashedPassword);
                                if (isValid)
                                {
                                    // 使用独立连接更新登录信息
                                    UpdateLastLoginInfo(username);
                                }
                                return isValid;
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "验证用户失败");
                LogHelper.LogError($"数据库连接失败，请检查数据库配置或联系管理员处理。错误信息: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 迁移老密码为加密密码
        /// </summary>
        private void MigratePassword(string username, string password)
        {
            string connectionString = ConnectionStrings.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
                return;
                
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string salt = SecurityHelper.GenerateSalt();
                    string hashedPassword = SecurityHelper.HashPassword(password, salt);
                    
                    string sql = "UPDATE TJYHB SET YKL = @hashedPassword, SALT = @salt WHERE YHM = @username";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.ExecuteNonQuery();
                        LogHelper.LogInfo($"用户 {username} 密码迁移成功");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "密码迁移失败");
            }
        }

        /// <summary>
        /// 更新最后登录信息
        /// </summary>
        private void UpdateLastLoginInfo(string username)
        {
            string connectionString = ConnectionStrings.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
                return;
                
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = "UPDATE TJYHB SET LAST_LOGIN_TIME = GETDATE(), LAST_LOGIN_IP = @ip WHERE YHM = @username";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ip", "local");
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "更新登录信息失败");
            }
        }

        private void EnsureTableExists(SqlConnection conn)
        {
            if (_tableChecked)
            {
                return;
            }
            
            lock (_lock)
            {
                if (_tableChecked)
                {
                    return;
                }
                
                try
                {
                    string checkTableSql = "SELECT COUNT(*) FROM sys.tables WHERE name = 'TJYHB'";
                    using (SqlCommand checkCmd = new SqlCommand(checkTableSql, conn))
                    {
                        int tableExists = (int)checkCmd.ExecuteScalar();
                        
                        if (tableExists == 0)
                        {
                            LogHelper.LogInfo("TJYHB表不存在，正在创建...");
                            
                            string createTableSql = @"
                                CREATE TABLE TJYHB (
                                    ID INT PRIMARY KEY,
                                    YHM VARCHAR(50) NOT NULL UNIQUE,
                                    YKL VARCHAR(256) NOT NULL,
                                    SALT VARCHAR(64),
                                    QX INT DEFAULT 0,
                                    SFY INT DEFAULT 1,
                                    LAST_LOGIN_TIME DATETIME,
                                    LAST_LOGIN_IP VARCHAR(50)
                                )";
                            using (SqlCommand createCmd = new SqlCommand(createTableSql, conn))
                            {
                                createCmd.ExecuteNonQuery();
                                LogHelper.LogInfo("TJYHB表创建成功");
                            }

                            // 创建默认管理员用户，使用加密密码
                            string salt = SecurityHelper.GenerateSalt();
                            string hashedPassword = SecurityHelper.HashPassword("241023", salt);
                            string insertDefaultUserSql = "INSERT INTO TJYHB (ID, YHM, YKL, SALT, QX, SFY) VALUES (1, 'lhbdb', @hashedPassword, @salt, 1, 1)";
                            using (SqlCommand insertCmd = new SqlCommand(insertDefaultUserSql, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                                insertCmd.Parameters.AddWithValue("@salt", salt);
                                insertCmd.ExecuteNonQuery();
                                LogHelper.LogInfo("默认管理员用户创建成功");
                            }
                        }
                        else
                        {
                            LogHelper.LogInfo("TJYHB表已存在，检查并添加缺失的列...");
                            
                            AddMissingColumn(conn, "YHM", "VARCHAR(50) NOT NULL");
                            AddMissingColumn(conn, "YKL", "VARCHAR(256) NOT NULL");
                            AddMissingColumn(conn, "SALT", "VARCHAR(64)");
                            AddMissingColumn(conn, "QX", "INT DEFAULT 0");
                            AddMissingColumn(conn, "SFY", "INT DEFAULT 1");
                            AddMissingColumn(conn, "LAST_LOGIN_TIME", "DATETIME");
                            AddMissingColumn(conn, "LAST_LOGIN_IP", "VARCHAR(50)");
                            
                            LogHelper.LogInfo("TJYHB表结构检查完成");
                            
                            string checkUniqueSql = "SELECT COUNT(*) FROM sys.indexes WHERE name = 'UQ_TJYHB_YHM' AND object_id = OBJECT_ID('TJYHB')";
                            using (SqlCommand checkUniqueCmd = new SqlCommand(checkUniqueSql, conn))
                            {
                                int hasUnique = (int)checkUniqueCmd.ExecuteScalar();
                                if (hasUnique == 0)
                                {
                                    try
                                    {
                                        string addUniqueSql = "ALTER TABLE TJYHB ADD CONSTRAINT UQ_TJYHB_YHM UNIQUE (YHM)";
                                        using (SqlCommand addUniqueCmd = new SqlCommand(addUniqueSql, conn))
                                        {
                                            addUniqueCmd.ExecuteNonQuery();
                                            LogHelper.LogInfo("已添加YHM唯一约束");
                                        }
                                    }
                                    catch { }
                                }
                            }
                            
                            string checkUserSql = "SELECT COUNT(*) FROM TJYHB WHERE YHM = 'lhbdb'";
                            using (SqlCommand checkUserCmd = new SqlCommand(checkUserSql, conn))
                            {
                                int userExists = (int)checkUserCmd.ExecuteScalar();
                                if (userExists == 0)
                                {
                                    string salt = SecurityHelper.GenerateSalt();
                                    string hashedPassword = SecurityHelper.HashPassword("241023", salt);
                                    string insertDefaultUserSql = "INSERT INTO TJYHB (ID, YHM, YKL, SALT, QX, SFY) VALUES (1, 'lhbdb', @hashedPassword, @salt, 1, 1)";
                                    using (SqlCommand insertCmd = new SqlCommand(insertDefaultUserSql, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                                        insertCmd.Parameters.AddWithValue("@salt", salt);
                                        try
                                        {
                                            insertCmd.ExecuteNonQuery();
                                            LogHelper.LogInfo("默认管理员用户不存在，已创建");
                                        }
                                        catch (Exception ex)
                                        {
                                            LogHelper.LogInfo($"插入默认用户失败（可能ID已存在）: {ex.Message}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogError($"初始化数据库表失败: {ex.Message}");
                }

                _tableChecked = true;
            }
        }

        private void AddMissingColumn(SqlConnection conn, string columnName, string columnType)
        {
            string checkColumnSql = @"
                SELECT COUNT(*) 
                FROM sys.columns 
                WHERE object_id = OBJECT_ID('TJYHB') 
                AND name = @columnName";
            using (SqlCommand cmd = new SqlCommand(checkColumnSql, conn))
            {
                cmd.Parameters.AddWithValue("@columnName", columnName);
                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    string addColumnSql = $"ALTER TABLE TJYHB ADD {columnName} {columnType}";
                    using (SqlCommand addCmd = new SqlCommand(addColumnSql, conn))
                    {
                        addCmd.ExecuteNonQuery();
                        LogHelper.LogInfo($"已添加缺失列: {columnName}");
                    }
                }
            }
        }

        public bool ValidateUserWithConnectionCheck(string username, string password)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法验证用户");
                return false;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    EnsureTableExists(conn);
                    
                    string sql = "SELECT COUNT(*) FROM TJYHB WHERE YHM = @username AND YKL = @password AND SFY = 1";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            LogHelper.LogError("用户名或密码错误");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "验证用户失败");
                LogHelper.LogError($"数据库连接失败，请检查数据库配置。错误信息: {ex.Message}");
                return false;
            }
        }

        public bool IsAdminUser(string username)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法检查管理员权限");
                return false;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    EnsureTableExists(conn);
                    
                    string sql = "SELECT QX FROM TJYHB WHERE YHM = @username";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return (int)result == 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "检查管理员权限失败");
            }

            return false;
        }

        public string GetUsersJson()
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法获取用户列表");
                return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    EnsureTableExists(conn);
                    
                    string sql = "SELECT ID, YHM as username, CASE WHEN QX = 1 THEN '管理员' ELSE '普通用户' END as role, CASE WHEN SFY = 1 THEN '启用' ELSE '禁用' END as status FROM TJYHB";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> users = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                var user = new System.Collections.Generic.Dictionary<string, object>();
                                user["id"] = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0;
                                user["username"] = reader["username"] != DBNull.Value ? reader["username"].ToString() : "";
                                user["role"] = reader["role"] != DBNull.Value ? reader["role"].ToString() : "普通用户";
                                user["status"] = reader["status"] != DBNull.Value ? reader["status"].ToString() : "禁用";
                                users.Add(user);
                            }
                            string result = "{\"success\": true, \"data\": " + Newtonsoft.Json.JsonConvert.SerializeObject(users) + "}";
                            LogHelper.LogInfo($"返回用户列表: {result}");
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取用户列表失败");
                return "{\"success\": false, \"error\": \"" + ex.Message + "\"}";
            }
        }

        public string GetUserJson(int userId)
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空，无法获取用户信息");
                return "{\"success\": false, \"error\": \"数据库连接未配置\"}";
            }

            LogHelper.LogInfo($"获取用户信息: userId={userId}");

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    EnsureTableExists(conn);
                    
                    string sql = "SELECT ID, YHM as username, QX as role, SFY as status FROM TJYHB WHERE ID = @UserId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        
                        LogHelper.LogInfo($"执行SQL: {sql}, 参数: UserId={userId}");
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LogHelper.LogInfo("找到用户记录");
                                var user = new System.Collections.Generic.Dictionary<string, object>();
                                user["id"] = reader["ID"];
                                user["username"] = reader["username"];
                                user["role"] = Convert.ToInt32(reader["role"]) == 1 ? "管理员" : "普通用户";
                                user["status"] = Convert.ToInt32(reader["status"]) == 1 ? "启用" : "禁用";
                                string result = "{\"success\": true, \"data\": " + Newtonsoft.Json.JsonConvert.SerializeObject(user) + "}";
                                LogHelper.LogInfo($"返回用户数据: {result}");
                                return result;
                            }
                            else
                            {
                                LogHelper.LogInfo("未找到用户记录");
                                return "{\"success\": false, \"error\": \"用户不存在\"}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取用户信息失败");
                return "{\"success\": false, \"error\": \"" + ex.Message + "\"}";
            }
        }

        public bool AddUser(int id, string username, string password, string role, string status)
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
                    EnsureTableExists(conn);
                    
                    // 如果ID为0，自动生成唯一ID
                    if (id <= 0)
                    {
                        string maxIdSql = "SELECT ISNULL(MAX(ID), 0) FROM TJYHB";
                        using (SqlCommand maxCmd = new SqlCommand(maxIdSql, conn))
                        {
                            id = (int)maxCmd.ExecuteScalar() + 1;
                        }
                    }

                    string checkSql = "SELECT COUNT(*) FROM TJYHB WHERE ID = @id OR YHM = @username";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", id);
                        checkCmd.Parameters.AddWithValue("@username", username);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            return false;
                        }
                    }

                    // 生成密码盐值和加密密码
                    string salt = SecurityHelper.GenerateSalt();
                    string hashedPassword = SecurityHelper.HashPassword(password, salt);

                    string sql = "INSERT INTO TJYHB (ID, YHM, YKL, SALT, QX, SFY) VALUES (@id, @username, @hashedPassword, @salt, @role, @status)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@role", role == "管理员" ? 1 : 0);
                        cmd.Parameters.AddWithValue("@status", status == "启用" ? 1 : 0);
                        cmd.ExecuteNonQuery();
                        LogHelper.LogInfo($"用户添加成功: {username}, ID={id}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "添加用户失败");
                return false;
            }
        }

        public bool UpdateUser(int originalId, int newId, string username, string password, string role, string status)
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
                    EnsureTableExists(conn);
                    
                    string checkAdminSql = "SELECT YHM FROM TJYHB WHERE ID = @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkAdminSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", originalId);
                        object result = checkCmd.ExecuteScalar();
                        if (result != null && result.ToString() == AppConstants.DefaultAdminUser)
                        {
                            return false;
                        }
                    }

                    // 如果ID发生变化，检查新ID是否已被使用
                    if (originalId != newId)
                    {
                        string checkNewIdSql = "SELECT COUNT(*) FROM TJYHB WHERE ID = @newId";
                        using (SqlCommand checkNewIdCmd = new SqlCommand(checkNewIdSql, conn))
                        {
                            checkNewIdCmd.Parameters.AddWithValue("@newId", newId);
                            int count = (int)checkNewIdCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                return false;
                            }
                        }
                    }

                    SqlCommand cmd;
                    string sql;

                    if (string.IsNullOrEmpty(password))
                    {
                        if (originalId != newId)
                        {
                            sql = "UPDATE TJYHB SET ID = @newId, YHM = @username, QX = @role, SFY = @status WHERE ID = @originalId";
                        }
                        else
                        {
                            sql = "UPDATE TJYHB SET YHM = @username, QX = @role, SFY = @status WHERE ID = @originalId";
                        }
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@originalId", originalId);
                        if (originalId != newId)
                        {
                            cmd.Parameters.AddWithValue("@newId", newId);
                        }
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@role", role == "管理员" ? 1 : 0);
                        cmd.Parameters.AddWithValue("@status", status == "启用" ? 1 : 0);
                    }
                    else
                    {
                        // 更新密码时重新生成盐值和加密密码
                        string salt = SecurityHelper.GenerateSalt();
                        string hashedPassword = SecurityHelper.HashPassword(password, salt);
                        
                        if (originalId != newId)
                        {
                            sql = "UPDATE TJYHB SET ID = @newId, YHM = @username, YKL = @hashedPassword, SALT = @salt, QX = @role, SFY = @status WHERE ID = @originalId";
                        }
                        else
                        {
                            sql = "UPDATE TJYHB SET YHM = @username, YKL = @hashedPassword, SALT = @salt, QX = @role, SFY = @status WHERE ID = @originalId";
                        }
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@originalId", originalId);
                        if (originalId != newId)
                        {
                            cmd.Parameters.AddWithValue("@newId", newId);
                        }
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@role", role == "管理员" ? 1 : 0);
                        cmd.Parameters.AddWithValue("@status", status == "启用" ? 1 : 0);
                    }

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        LogHelper.LogInfo($"用户更新成功: 原ID={originalId}, 新ID={newId}");
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "更新用户失败");
                return false;
            }
        }

        public bool DeleteUser(int id)
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
                    EnsureTableExists(conn);
                    
                    string checkSql = "SELECT YHM FROM TJYHB WHERE ID = @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", id);
                        object result = checkCmd.ExecuteScalar();
                        if (result != null && result.ToString() == AppConstants.DefaultAdminUser)
                        {
                            return false;
                        }
                    }

                    string sql = "DELETE FROM TJYHB WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            LogHelper.LogInfo($"用户删除成功: ID={id}");
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "删除用户失败");
                return false;
            }
        }
    }
}
