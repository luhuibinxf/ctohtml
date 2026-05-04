using System;
using System.Data.SqlClient;

namespace DbProcedureCaller.Core
{
    public static class DatabaseConnection
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static SqlConnection GetConnection()
        {
            string connectionString = Config.ConnectionStrings.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("未配置数据库连接字符串");
            }
            return GetConnection(connectionString);
        }

        public static bool TestConnection(string connectionString, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool TestConnection(out string errorMessage)
        {
            string connectionString = Config.ConnectionStrings.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                errorMessage = "未配置数据库连接字符串";
                return false;
            }
            return TestConnection(connectionString, out errorMessage);
        }
    }
}