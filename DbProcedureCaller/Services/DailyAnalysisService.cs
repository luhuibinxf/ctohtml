using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using DbProcedureCaller.Core;
using DbProcedureCaller.Config;

namespace DbProcedureCaller.Services
{
    public class DailyAnalysisService
    {
        public DataTable GetAnalysisData(
            string startDate = "",
            string endDate = "",
            string system = "",
            string reporter = "",
            string reviewer = "",
            string technician = "",
            string department = "",
            string category = "",
            string patientType = "",
            string resultStatus = "",
            string groupBy = "",
            string sortBy = "任务数量",
            string sortOrder = "DESC",
            int pageSize = 100,
            int pageIndex = 1)
        {
            DataTable dt = new DataTable();
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空");
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add("请先配置数据库连接");
                return dt;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = 120;
                        int paramIndex = 0;
                        StringBuilder sqlBuilder = BuildQuerySql(startDate, endDate, system, reporter, reviewer, technician, department, category, patientType, resultStatus, groupBy, sortBy, sortOrder, pageSize, pageIndex, cmd, ref paramIndex);
                        cmd.CommandText = sqlBuilder.ToString();

                        LogHelper.LogInfo("执行SQL查询: " + cmd.CommandText.Substring(0, Math.Min(500, cmd.CommandText.Length)));

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }

                    RecordQueryHistory(startDate, endDate, system, reporter, reviewer, technician, department, category, dt.Rows.Count, null);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取统计数据失败");
                RecordQueryHistory(startDate, endDate, system, reporter, reviewer, technician, department, category, 0, ex.Message);
                dt = new DataTable();
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string GetAllOptions(string system = "")
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空");
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\"}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    var systems = new List<Dictionary<string, string>>();
                    var reporters = new List<string>();
                    var reviewers = new List<string>();
                    var technicians = new List<string>();
                    var departments = new List<string>();
                    var categories = new List<string>();
                    var patientTypes = new List<Dictionary<string, string>>();
                    var resultStatus = new List<Dictionary<string, string>>();

                    var systemMap = new Dictionary<string, string>
                    {
                        { "UIS", "超声" },
                        { "RIS", "放射" },
                        { "EIS", "内镜" },
                        { "PIS", "病理" },
                        { "NMS", "核医学" }
                    };

                    string sql = @"SELECT DISTINCT SYSTEM_SOURCE_NO as code 
                                   FROM EXAM_TASK 
                                   WHERE SYSTEM_SOURCE_NO IS NOT NULL 
                                   ORDER BY SYSTEM_SOURCE_NO";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string code = reader["code"]?.ToString() ?? "";
                            string name = systemMap.ContainsKey(code) ? systemMap[code] : code;
                            systems.Add(new Dictionary<string, string>
                            {
                                { "code", code },
                                { "name", name }
                            });
                        }
                    }

                    sql = @"SELECT DISTINCT r.REPORTER_NAME as name 
                            FROM EXAM_REPORT r 
                            INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID 
                            WHERE r.REPORTER_NAME IS NOT NULL ";
                    if (!string.IsNullOrEmpty(system))
                    {
                        sql += " AND t.SYSTEM_SOURCE_NO = @System ";
                    }
                    sql += " ORDER BY r.REPORTER_NAME";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(system))
                        {
                            cmd.Parameters.AddWithValue("@System", system);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(name))
                                {
                                    reporters.Add(name);
                                }
                            }
                        }
                    }

                    sql = @"SELECT DISTINCT r.REVIEWER_NAME as name 
                            FROM EXAM_REPORT r 
                            INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID 
                            WHERE r.REVIEWER_NAME IS NOT NULL ";
                    if (!string.IsNullOrEmpty(system))
                    {
                        sql += " AND t.SYSTEM_SOURCE_NO = @System ";
                    }
                    sql += " ORDER BY r.REVIEWER_NAME";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(system))
                        {
                            cmd.Parameters.AddWithValue("@System", system);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(name))
                                {
                                    reviewers.Add(name);
                                }
                            }
                        }
                    }

                    sql = @"SELECT DISTINCT t.TECHNICIAN_NAME as name 
                            FROM EXAM_TASK t 
                            WHERE t.TECHNICIAN_NAME IS NOT NULL ";
                    if (!string.IsNullOrEmpty(system))
                    {
                        sql += " AND t.SYSTEM_SOURCE_NO = @System ";
                    }
                    sql += " ORDER BY t.TECHNICIAN_NAME";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(system))
                        {
                            cmd.Parameters.AddWithValue("@System", system);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(name))
                                {
                                    technicians.Add(name);
                                }
                            }
                        }
                    }

                    sql = @"SELECT DISTINCT t.EXEC_DEPT_NAME as name 
                            FROM EXAM_TASK t 
                            WHERE t.EXEC_DEPT_NAME IS NOT NULL ";
                    if (!string.IsNullOrEmpty(system))
                    {
                        sql += " AND t.SYSTEM_SOURCE_NO = @System ";
                    }
                    sql += " ORDER BY t.EXEC_DEPT_NAME";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(system))
                        {
                            cmd.Parameters.AddWithValue("@System", system);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(name))
                                {
                                    departments.Add(name);
                                }
                            }
                        }
                    }

                    sql = @"SELECT DISTINCT t.EXAM_CATEGORY_NAME as name 
                            FROM EXAM_TASK t 
                            WHERE t.EXAM_CATEGORY_NAME IS NOT NULL ";
                    if (!string.IsNullOrEmpty(system))
                    {
                        sql += " AND t.SYSTEM_SOURCE_NO = @System ";
                    }
                    sql += " ORDER BY t.EXAM_CATEGORY_NAME";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(system))
                        {
                            cmd.Parameters.AddWithValue("@System", system);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(name))
                                {
                                    categories.Add(name);
                                }
                            }
                        }
                    }

                    sql = @"SELECT DISTINCT t.ENCOUNTER_TYPE_NO as code, 
                                  ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO) as name 
                           FROM EXAM_TASK t 
                           LEFT JOIN Pacs_SysDict d ON d.TableName='EXAM_TASK' AND d.FieldName='ENCOUNTER_TYPE_NO' AND d.nValue=CAST(t.ENCOUNTER_TYPE_NO AS INT) 
                           WHERE t.ENCOUNTER_TYPE_NO IS NOT NULL AND t.ENCOUNTER_TYPE_NO != '' ";
                    if (!string.IsNullOrEmpty(system))
                    {
                        sql += " AND t.SYSTEM_SOURCE_NO = @System ";
                    }
                    sql += " ORDER BY t.ENCOUNTER_TYPE_NO";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(system))
                        {
                            cmd.Parameters.AddWithValue("@System", system);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string code = reader["code"]?.ToString() ?? "";
                                string name = reader["name"]?.ToString() ?? "";
                                if (name == code || string.IsNullOrEmpty(name))
                                {
                                    name = GetDefaultPatientTypeName(code);
                                }
                                if (!string.IsNullOrEmpty(code))
                                {
                                    patientTypes.Add(new Dictionary<string, string>
                                    {
                                        { "code", code },
                                        { "name", name }
                                    });
                                }
                            }
                        }
                    }

                    sql = @"SELECT DISTINCT r.NEG_POS_CODE as code 
                            FROM EXAM_REPORT r 
                            WHERE r.NEG_POS_CODE IS NOT NULL AND r.NEG_POS_CODE != '' 
                            ORDER BY r.NEG_POS_CODE";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var statusSet = new HashSet<string>();
                        while (reader.Read())
                        {
                            string code = reader["code"]?.ToString()?.Trim() ?? "";
                            string name = MapNegPosCode(code);
                            if (!string.IsNullOrEmpty(code) && !statusSet.Contains(name))
                            {
                                statusSet.Add(name);
                                resultStatus.Add(new Dictionary<string, string>
                                {
                                    { "code", code },
                                    { "name", name }
                                });
                            }
                        }
                    }

                    return string.Format("{{\"success\":true,\"data\":{{\"systems\":{0},\"reporters\":{1},\"reviewers\":{2},\"technicians\":{3},\"departments\":{4},\"categories\":{5},\"patientTypes\":{6},\"resultStatus\":{7}}}}}",
                        Newtonsoft.Json.JsonConvert.SerializeObject(systems),
                        Newtonsoft.Json.JsonConvert.SerializeObject(reporters),
                        Newtonsoft.Json.JsonConvert.SerializeObject(reviewers),
                        Newtonsoft.Json.JsonConvert.SerializeObject(technicians),
                        Newtonsoft.Json.JsonConvert.SerializeObject(departments),
                        Newtonsoft.Json.JsonConvert.SerializeObject(categories),
                        Newtonsoft.Json.JsonConvert.SerializeObject(patientTypes),
                        Newtonsoft.Json.JsonConvert.SerializeObject(resultStatus));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取所有下拉框选项失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        private List<Dictionary<string, string>> GetDataListWithTimeout(SqlConnection conn, string sql, int timeoutSeconds)
        {
            var list = new List<Dictionary<string, string>>();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandTimeout = timeoutSeconds;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string code = reader["code"]?.ToString()?.Trim() ?? "";
                        string name = reader["name"]?.ToString()?.Trim() ?? "";
                        if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name))
                        {
                            list.Add(new Dictionary<string, string>
                            {
                                { "code", code },
                                { "name", name }
                            });
                        }
                    }
                }
            }
            return list;
        }

        private List<Dictionary<string, string>> GetPatientTypesWithMapping(SqlConnection conn, string system)
        {
            var list = new List<Dictionary<string, string>>();
            try
            {
                string sql = @"SELECT DISTINCT t.ENCOUNTER_TYPE_NO as code, 
                              ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO) as name
                              FROM EXAM_TASK t WITH(NOLOCK)
                              LEFT JOIN Pacs_SysDict d WITH(NOLOCK) ON 
                                d.TableName='EXAM_TASK' AND d.FieldName='ENCOUNTER_TYPE_NO' AND
                                (CAST(d.nValue AS VARCHAR(50)) = t.ENCOUNTER_TYPE_NO)
                              WHERE t.ENCOUNTER_TYPE_NO IS NOT NULL AND t.ENCOUNTER_TYPE_NO != ''
                              AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                              ORDER BY t.ENCOUNTER_TYPE_NO";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@System", system ?? "");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string code = reader["code"]?.ToString()?.Trim() ?? "";
                            string name = reader["name"]?.ToString()?.Trim() ?? "";
                            if (!string.IsNullOrEmpty(code))
                            {
                                if (name == code || string.IsNullOrEmpty(name))
                                {
                                    name = GetDefaultPatientTypeName(code);
                                }
                                list.Add(new Dictionary<string, string>
                                {
                                    { "code", code },
                                    { "name", name }
                                });
                            }
                        }
                    }
                }

                if (list.Count == 0)
                {
                    list = GetDataList(conn, @"SELECT DISTINCT ENCOUNTER_TYPE_NO as code, ENCOUNTER_TYPE_NO as name FROM EXAM_TASK WITH(NOLOCK) WHERE ENCOUNTER_TYPE_NO IS NOT NULL AND ENCOUNTER_TYPE_NO != '' ORDER BY ENCOUNTER_TYPE_NO");
                    foreach (var item in list)
                    {
                        item["name"] = GetDefaultPatientTypeName(item["code"]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取病人类型失败");
                list = GetDataList(conn, @"SELECT DISTINCT ENCOUNTER_TYPE_NO as code, ENCOUNTER_TYPE_NO as name FROM EXAM_TASK WITH(NOLOCK) WHERE ENCOUNTER_TYPE_NO IS NOT NULL AND ENCOUNTER_TYPE_NO != '' ORDER BY ENCOUNTER_TYPE_NO");
                foreach (var item in list)
                {
                    item["name"] = GetDefaultPatientTypeName(item["code"]);
                }
            }
            
            return list;
        }

        private string GetPatientTypeNameFromDict(SqlConnection conn, string code)
        {
            string sql = @"SELECT TOP 1 cValue FROM Pacs_SysDict 
                          WHERE TableName='EXAM_TASK' AND FieldName='ENCOUNTER_TYPE_NO' 
                          AND CAST(nValue AS VARCHAR(50)) = @Code";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Code", code);
                object result = cmd.ExecuteScalar();
                return result?.ToString()?.Trim() ?? code;
            }
        }

        private string GetDefaultPatientTypeName(string code)
        {
            if (string.IsNullOrEmpty(code)) return code;
            
            string trimmed = code.Trim();
            
            var mappings = new Dictionary<string, string>
            {
                { "1", "门诊" },
                { "2", "住院" },
                { "3", "急诊" },
                { "4", "体检" },
                { "138138", "门诊" },
                { "138139", "急诊" },
                { "138140", "体检" },
                { "145235", "住院" },
                { "OPD", "门诊" },
                { "IPD", "住院" },
                { "EMER", "急诊" },
                { "CHECKUP", "体检" }
            };
            
            if (mappings.ContainsKey(trimmed))
            {
                return mappings[trimmed];
            }
            
            return code;
        }

        

        private List<Dictionary<string, string>> GetDataList(SqlConnection conn, string sql)
        {
            var list = new List<Dictionary<string, string>>();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string code = reader["code"]?.ToString()?.Trim() ?? "";
                    string name = reader["name"]?.ToString()?.Trim() ?? "";
                    if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name))
                    {
                        list.Add(new Dictionary<string, string>
                        {
                            { "code", code },
                            { "name", name }
                        });
                    }
                }
            }
            return list;
        }

        private List<Dictionary<string, string>> GetResultStatusOptions(SqlConnection conn)
        {
            var list = new List<Dictionary<string, string>>();
            try
            {
                string sql = @"SELECT DISTINCT r.NEG_POS_CODE as code, 
                              ISNULL(d.cValue, r.NEG_POS_CODE) as name
                              FROM EXAM_REPORT r WITH(NOLOCK)
                              LEFT JOIN Pacs_SysDict d WITH(NOLOCK) ON 
                                d.TableName='EXAM_REPORT' AND d.FieldName='NEG_POS_CODE' AND 
                                d.nValue = CASE WHEN ISNUMERIC(r.NEG_POS_CODE) = 1 THEN CAST(r.NEG_POS_CODE AS INT) ELSE 0 END
                              WHERE r.NEG_POS_CODE IS NOT NULL AND r.NEG_POS_CODE != '' 
                              AND r.NEG_POS_CODE IN ('P', 'N')
                              ORDER BY r.NEG_POS_CODE";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string code = reader["code"]?.ToString()?.Trim() ?? "";
                        string dictName = reader["name"]?.ToString()?.Trim() ?? "";
                        string name = MapNegPosCode(code, dictName);
                        if (!string.IsNullOrEmpty(code))
                        {
                            list.Add(new Dictionary<string, string>
                            {
                                { "code", code },
                                { "name", name }
                            });
                        }
                    }
                }
                
                if (list.Count == 0)
                {
                    list.Add(new Dictionary<string, string> { { "code", "P" }, { "name", "阳性" } });
                    list.Add(new Dictionary<string, string> { { "code", "N" }, { "name", "阴性" } });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取阴阳性选项失败");
                list.Add(new Dictionary<string, string> { { "code", "P" }, { "name", "阳性" } });
                list.Add(new Dictionary<string, string> { { "code", "N" }, { "name", "阴性" } });
            }
            
            if (list.Count == 0)
            {
                list.Add(new Dictionary<string, string> { { "code", "P" }, { "name", "阳性" } });
                list.Add(new Dictionary<string, string> { { "code", "N" }, { "name", "阴性" } });
                list.Add(new Dictionary<string, string> { { "code", "U" }, { "name", "未知" } });
            }
            
            return list;
        }

        private string MapNegPosCode(string code, string dictName = "")
        {
            if (!string.IsNullOrEmpty(dictName) && dictName != code)
            {
                return dictName;
            }
            
            switch (code.ToUpper())
            {
                case "P":
                case "POS":
                case "Y":
                case "阳性":
                case "383927":
                    return "阳性";
                case "N":
                case "NEG":
                case "阴性":
                case "383926":
                    return "阴性";
                default:
                    return code;
            }
        }

        private string TranslateResultStatusToCode(string status)
        {
            if (string.IsNullOrEmpty(status))
                return status;
            
            string[] values = status.Split(',');
            List<string> codes = new List<string>();
            
            foreach (string v in values)
            {
                string trimmed = v.Trim();
                switch (trimmed)
                {
                    case "阳性":
                        codes.Add("P");
                        break;
                    case "阴性":
                        codes.Add("N");
                        break;
                    case "未知":
                        codes.Add("U");
                        break;
                    default:
                        codes.Add(trimmed);
                        break;
                }
            }
            
            return string.Join(",", codes);
        }

        public string GetSystemTypes()
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空");
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT DISTINCT SYSTEM_SOURCE_NO as code, SYSTEM_SOURCE_NO as name 
                                   FROM EXAM_TASK 
                                   WHERE SYSTEM_SOURCE_NO IS NOT NULL 
                                   ORDER BY SYSTEM_SOURCE_NO";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var list = new List<Dictionary<string, string>>();
                        while (reader.Read())
                        {
                            list.Add(new Dictionary<string, string>
                            {
                                { "code", reader["code"]?.ToString() ?? "" },
                                { "name", reader["name"]?.ToString() ?? "" }
                            });
                        }

                        return string.Format("{{\"success\": true, \"data\": {0}}}",
                            Newtonsoft.Json.JsonConvert.SerializeObject(list));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取系统类型失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        public string GetReporters(string system)
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空，无法获取报告医生数据");
                    return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT DISTINCT r.REPORTER_NAME as code, r.REPORTER_NAME as name
                                   FROM EXAM_REPORT r
                                   INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID
                                   WHERE r.REPORTER_NAME IS NOT NULL
                                   AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                                   ORDER BY r.REPORTER_NAME";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var list = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                list.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"]?.ToString() ?? "" },
                                    { "name", reader["name"]?.ToString() ?? "" }
                                });
                            }

                            return string.Format("{{\"success\": true, \"data\": {0}}}",
                                Newtonsoft.Json.JsonConvert.SerializeObject(list));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取报告医生失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        public string GetReviewers(string system)
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空，无法获取审核医生数据");
                    return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT DISTINCT r.REVIEWER_NAME as code, r.REVIEWER_NAME as name
                                   FROM EXAM_REPORT r
                                   INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID
                                   WHERE r.REVIEWER_NAME IS NOT NULL
                                   AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                                   ORDER BY r.REVIEWER_NAME";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var list = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                list.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"]?.ToString() ?? "" },
                                    { "name", reader["name"]?.ToString() ?? "" }
                                });
                            }

                            return string.Format("{{\"success\": true, \"data\": {0}}}",
                                Newtonsoft.Json.JsonConvert.SerializeObject(list));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取审核医生失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        public string GetCategories(string system)
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空，无法获取检查类型数据");
                    return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT DISTINCT t.EXAM_CATEGORY_NAME as code, t.EXAM_CATEGORY_NAME as name
                                   FROM EXAM_TASK t
                                   WHERE t.EXAM_CATEGORY_NAME IS NOT NULL
                                   AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                                   ORDER BY t.EXAM_CATEGORY_NAME";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var list = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                list.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"]?.ToString() ?? "" },
                                    { "name", reader["name"]?.ToString() ?? "" }
                                });
                            }

                            return string.Format("{{\"success\": true, \"data\": {0}}}",
                                Newtonsoft.Json.JsonConvert.SerializeObject(list));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取检查类型失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        public string GetDepartments(string system)
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空，无法获取执行科室数据");
                    return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT DISTINCT t.EXEC_DEPT_NAME as code, t.EXEC_DEPT_NAME as name
                                   FROM EXAM_TASK t
                                   WHERE t.EXEC_DEPT_NAME IS NOT NULL
                                   AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                                   ORDER BY t.EXEC_DEPT_NAME";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var list = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                list.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"]?.ToString() ?? "" },
                                    { "name", reader["name"]?.ToString() ?? "" }
                                });
                            }

                            return string.Format("{{\"success\": true, \"data\": {0}}}",
                                Newtonsoft.Json.JsonConvert.SerializeObject(list));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取执行科室失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        public string GetPatientTypes(string system)
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空，无法获取病人类型数据");
                    return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT DISTINCT t.ENCOUNTER_TYPE_NO as code, 
                                   ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO) as name
                                   FROM EXAM_TASK t
                                   LEFT JOIN Pacs_SysDict d ON d.TableName='EXAM_TASK' AND d.FieldName='ENCOUNTER_TYPE_NO' AND d.nValue=CAST(t.ENCOUNTER_TYPE_NO AS INT)
                                   WHERE t.ENCOUNTER_TYPE_NO IS NOT NULL AND t.ENCOUNTER_TYPE_NO != ''
                                   AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                                   ORDER BY t.ENCOUNTER_TYPE_NO";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var list = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                list.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"]?.ToString() ?? "" },
                                    { "name", reader["name"]?.ToString() ?? "" }
                                });
                            }

                            return string.Format("{{\"success\": true, \"data\": {0}}}",
                                Newtonsoft.Json.JsonConvert.SerializeObject(list));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取病人类型失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        public string GetResultStatus()
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空，无法获取结果状态数据");
                    return "{\"success\": false, \"error\": \"数据库连接未配置\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT DISTINCT NEG_POS_CODE as code
                                   FROM EXAM_REPORT
                                   WHERE NEG_POS_CODE IS NOT NULL AND NEG_POS_CODE != ''
                                   ORDER BY NEG_POS_CODE";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var list = new List<Dictionary<string, string>>();
                        while (reader.Read())
                        {
                            string code = reader["code"]?.ToString()?.Trim() ?? "";
                            if (!string.IsNullOrEmpty(code))
                            {
                                list.Add(new Dictionary<string, string>
                                {
                                    { "code", code },
                                    { "name", MapNegPosCode(code) }
                                });
                            }
                        }

                        return string.Format("{{\"success\": true, \"data\": {0}}}",
                            Newtonsoft.Json.JsonConvert.SerializeObject(list));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取结果状态失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        private StringBuilder BuildQuerySql(
            string startDate,
            string endDate,
            string system,
            string reporter,
            string reviewer,
            string technician,
            string department,
            string category,
            string patientType,
            string resultStatus,
            string groupBy,
            string sortBy,
            string sortOrder,
            int pageSize,
            int pageIndex,
            SqlCommand cmd,
            ref int paramIndex)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"
                SELECT
                    ISNULL(CASE t.SYSTEM_SOURCE_NO 
                        WHEN 'UIS' THEN '超声' 
                        WHEN 'RIS' THEN '放射' 
                        WHEN 'EIS' THEN '内镜' 
                        WHEN 'PIS' THEN '病理'
                        WHEN 'NMS' THEN '核医学'
                        ELSE t.SYSTEM_SOURCE_NO 
                    END, '') AS 系统,
                    ISNULL(r.REPORTER_NAME, '') AS 报告医生,
                    ISNULL(r.REVIEWER_NAME, '') AS 审核医生,
                    ISNULL(t.TECHNICIAN_NAME, '') AS 技师,
                    ISNULL(t.EXEC_DEPT_NAME, '') AS 执行科室,
                    ISNULL(t.EXAM_CATEGORY_NAME, '') AS 检查类型,
                    ISNULL(CASE t.ENCOUNTER_TYPE_NO 
                        WHEN '1' THEN '门诊' 
                        WHEN '2' THEN '住院' 
                        WHEN '3' THEN '急诊' 
                        WHEN '4' THEN '体检'
                        WHEN '138138' THEN '门诊' 
                        WHEN '138139' THEN '急诊'
                        WHEN '138140' THEN '体检' 
                        WHEN '145235' THEN '住院'
                        WHEN 'OPD' THEN '门诊' 
                        WHEN 'IPD' THEN '住院' 
                        WHEN 'EMER' THEN '急诊' 
                        WHEN 'CHECKUP' THEN '体检'
                        ELSE ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO) 
                    END, '') AS 病人类型,
                    ISNULL(CASE r.NEG_POS_CODE WHEN '383927' THEN '阳性' WHEN '383926' THEN '阴性' WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' WHEN 'Y' THEN '阳性' WHEN 'POS' THEN '阳性' WHEN 'NEG' THEN '阴性' ELSE '未知' END, '') AS 结果状态,
                    COUNT(*) AS 任务数量,
                    SUM(CASE WHEN r.NEG_POS_CODE = '383927' THEN 1 ELSE 0 END) AS 阳性数量,
                    SUM(CASE WHEN r.NEG_POS_CODE = '383926' THEN 1 ELSE 0 END) AS 阴性数量,
                    ROUND(CASE WHEN COUNT(*) > 0 THEN SUM(CASE WHEN r.NEG_POS_CODE = '383927' THEN 1.0 ELSE 0.0 END) * 100.0 / COUNT(*) ELSE 0.0 END, 2) AS 阳性率
                FROM EXAM_TASK t WITH(NOLOCK)
                INNER JOIN EXAM_REPORT r WITH(NOLOCK) ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
                LEFT JOIN Pacs_SysDict d WITH(NOLOCK) ON d.TableName='EXAM_TASK' AND d.FieldName='ENCOUNTER_TYPE_NO' 
                    AND (CAST(d.nValue AS VARCHAR(50)) = t.ENCOUNTER_TYPE_NO OR d.cValue = t.ENCOUNTER_TYPE_NO)
                WHERE t.IS_DEL = 0");

            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(startDate))
            {
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                conditions.Add("t.CREATED_AT >= @StartDate");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                conditions.Add("t.CREATED_AT < DATEADD(DAY, 1, @EndDate)");
            }
            if (!string.IsNullOrEmpty(system))
                conditions.Add(BuildInCondition("t.SYSTEM_SOURCE_NO", system, cmd, ref paramIndex));
            if (!string.IsNullOrEmpty(reporter))
                conditions.Add(BuildInCondition("r.REPORTER_NAME", reporter, cmd, ref paramIndex));
            if (!string.IsNullOrEmpty(reviewer))
                conditions.Add(BuildInCondition("r.REVIEWER_NAME", reviewer, cmd, ref paramIndex));
            if (!string.IsNullOrEmpty(technician))
                conditions.Add(BuildInCondition("t.TECHNICIAN_NAME", technician, cmd, ref paramIndex));
            if (!string.IsNullOrEmpty(department))
                conditions.Add(BuildInCondition("t.EXEC_DEPT_NAME", department, cmd, ref paramIndex));
            if (!string.IsNullOrEmpty(category))
                conditions.Add(BuildInCondition("t.EXAM_CATEGORY_NAME", category, cmd, ref paramIndex));
            if (!string.IsNullOrEmpty(patientType))
                conditions.Add(BuildInCondition("t.ENCOUNTER_TYPE_NO", patientType, cmd, ref paramIndex));
            if (!string.IsNullOrEmpty(resultStatus))
                conditions.Add(BuildInCondition("r.NEG_POS_CODE", TranslateResultStatusToCode(resultStatus), cmd, ref paramIndex));

            if (conditions.Count > 0)
                sqlBuilder.Append(" AND " + string.Join(" AND ", conditions));

            sqlBuilder.Append(@"
                GROUP BY
                    ISNULL(CASE t.SYSTEM_SOURCE_NO 
                        WHEN 'UIS' THEN '超声' 
                        WHEN 'RIS' THEN '放射' 
                        WHEN 'EIS' THEN '内镜' 
                        WHEN 'PIS' THEN '病理'
                        WHEN 'NMS' THEN '核医学'
                        ELSE t.SYSTEM_SOURCE_NO 
                    END, ''),
                    ISNULL(r.REPORTER_NAME, ''),
                    ISNULL(r.REVIEWER_NAME, ''),
                    ISNULL(t.TECHNICIAN_NAME, ''),
                    ISNULL(t.EXEC_DEPT_NAME, ''),
                    ISNULL(t.EXAM_CATEGORY_NAME, ''),
                    ISNULL(CASE t.ENCOUNTER_TYPE_NO 
                        WHEN '1' THEN '门诊' 
                        WHEN '2' THEN '住院' 
                        WHEN '3' THEN '急诊' 
                        WHEN '4' THEN '体检'
                        WHEN '138138' THEN '门诊' 
                        WHEN '138139' THEN '急诊'
                        WHEN '138140' THEN '体检' 
                        WHEN '145235' THEN '住院'
                        WHEN 'OPD' THEN '门诊' 
                        WHEN 'IPD' THEN '住院' 
                        WHEN 'EMER' THEN '急诊' 
                        WHEN 'CHECKUP' THEN '体检'
                        ELSE ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO) 
                    END, ''),
                    ISNULL(CASE r.NEG_POS_CODE WHEN '383927' THEN '阳性' WHEN '383926' THEN '阴性' WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' WHEN 'Y' THEN '阳性' WHEN 'POS' THEN '阳性' WHEN 'NEG' THEN '阴性' ELSE '未知' END, '')");

            string validSortBy = ValidateSortField(sortBy);
            string validSortOrder = sortOrder.ToUpper() == "ASC" ? "ASC" : "DESC";
            sqlBuilder.Append(" ORDER BY " + validSortBy + " " + validSortOrder);

            if (pageSize > 0)
            {
                int skip = (pageIndex - 1) * pageSize;
                sqlBuilder.Append(" OFFSET " + skip + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY");
            }

            return sqlBuilder;
        }

        /// <summary>
        /// 构建IN条件查询（参数化版本，防止SQL注入）
        /// 2026-04-29 修改：原版本直接拼接字符串存在SQL注入风险，改为使用参数化查询
        /// </summary>
        /// <param name="field">数据库字段名</param>
        /// <param name="values">值（支持逗号分隔的多个值）</param>
        /// <param name="cmd">SqlCommand对象，用于添加参数</param>
        /// <param name="paramIndex">参数索引引用，用于生成唯一参数名</param>
        /// <returns>生成的条件字符串</returns>
        private string BuildInCondition(string field, string values, SqlCommand cmd, ref int paramIndex)
        {
            if (values.Contains(","))
            {
                string[] valueArray = values.Split(',');
                List<string> paramNames = new List<string>();
                foreach (string v in valueArray)
                {
                    string paramName = "@param" + paramIndex++;
                    cmd.Parameters.AddWithValue(paramName, v.Trim());
                    paramNames.Add(paramName);
                }
                return field + " IN (" + string.Join(",", paramNames) + ")";
            }
            else
            {
                string paramName = "@param" + paramIndex++;
                cmd.Parameters.AddWithValue(paramName, values);
                return field + " = " + paramName;
            }
        }

        private string ValidateSortField(string sortBy)
        {
            string[] validFields = { "系统", "报告医生", "审核医生", "技师", "执行科室", "检查类型", "病人类型", "结果状态", "任务数量", "阳性数量", "阴性数量", "阳性率" };
            return validFields.Contains(sortBy) ? sortBy : "任务数量";
        }

        private void RecordQueryHistory(string startDate, string endDate, string system,
            string reporter, string reviewer, string technician,
            string department, string category,
            int rowCount, string errorMessage)
        {
            string connectionString = ConnectionStrings.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString)) return;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"INSERT INTO DAILY_QUERY_HISTORY
                        (QUERY_DATE, QUERY_TYPE, QUERY_PARAMS, EXECUTION_STATUS, ROW_COUNT, ERROR_MESSAGE)
                        VALUES (@queryDate, @queryType, @queryParams, @executionStatus, @rowCount, @errorMessage)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@queryDate", startDate);
                        cmd.Parameters.AddWithValue("@queryType", "daily");
                        cmd.Parameters.AddWithValue("@queryParams",
                            string.Format("{{\"startDate\": \"{0}\", \"endDate\": \"{1}\", \"system\": \"{2}\", \"reporter\": \"{3}\", \"reviewer\": \"{4}\", \"technician\": \"{5}\", \"department\": \"{6}\", \"category\": \"{7}\"}}",
                                startDate, endDate, system, reporter, reviewer, technician, department, category));
                        cmd.Parameters.AddWithValue("@executionStatus", string.IsNullOrEmpty(errorMessage) ? "成功" : "失败");
                        cmd.Parameters.AddWithValue("@rowCount", rowCount);
                        cmd.Parameters.AddWithValue("@errorMessage", errorMessage ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "记录查询历史失败");
            }
        }

        public DataTable GetDepartmentStatistics(string startDate, string endDate, string system = "")
        {
            DataTable dt = new DataTable();
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空");
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add("请先配置数据库连接");
                return dt;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT
                            t.EXEC_DEPT_NAME AS 执行科室,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '') AS 结果状态,
                            COUNT(*) AS 任务数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'P' THEN 1 ELSE 0 END), 0) AS 阳性数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'N' THEN 1 ELSE 0 END), 0) AS 阴性数量,
                            ISNULL(AVG(CASE WHEN r.NEG_POS_CODE = 'P' THEN 100.0 ELSE 0 END), 0) AS 阳性率
                        FROM EXAM_TASK t
                        LEFT JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
                        WHERE t.IS_DEL = 0
                            AND t.CREATED_AT >= @StartDate
                            AND t.CREATED_AT < DATEADD(DAY, 1, @EndDate)
                            AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                        GROUP BY
                            t.EXEC_DEPT_NAME,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '')
                        ORDER BY 任务数量 DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取科室统计数据失败");
                dt = new DataTable();
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable GetDoctorStatistics(string startDate, string endDate, string system = "", string doctorType = "reporter")
        {
            DataTable dt = new DataTable();
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空");
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add("请先配置数据库连接");
                return dt;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = doctorType == "reviewer" ? @"
                        SELECT
                            ISNULL(r.REVIEWER_NAME, '') AS 医生姓名,
                            '审核医生' AS 医生类型,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '') AS 结果状态,
                            COUNT(*) AS 任务数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'P' THEN 1 ELSE 0 END), 0) AS 阳性数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'N' THEN 1 ELSE 0 END), 0) AS 阴性数量,
                            ISNULL(AVG(CASE WHEN r.NEG_POS_CODE = 'P' THEN 100.0 ELSE 0 END), 0) AS 阳性率
                        FROM EXAM_TASK t
                        LEFT JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
                        WHERE t.IS_DEL = 0
                            AND t.CREATED_AT >= @StartDate
                            AND t.CREATED_AT < DATEADD(DAY, 1, @EndDate)
                            AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                            AND r.REVIEWER_NAME IS NOT NULL
                        GROUP BY
                            r.REVIEWER_NAME,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '')
                        ORDER BY 任务数量 DESC" : @"
                        SELECT
                            ISNULL(r.REPORTER_NAME, '') AS 医生姓名,
                            '报告医生' AS 医生类型,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '') AS 结果状态,
                            COUNT(*) AS 任务数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'P' THEN 1 ELSE 0 END), 0) AS 阳性数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'N' THEN 1 ELSE 0 END), 0) AS 阴性数量,
                            ISNULL(AVG(CASE WHEN r.NEG_POS_CODE = 'P' THEN 100.0 ELSE 0 END), 0) AS 阳性率
                        FROM EXAM_TASK t
                        LEFT JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
                        WHERE t.IS_DEL = 0
                            AND t.CREATED_AT >= @StartDate
                            AND t.CREATED_AT < DATEADD(DAY, 1, @EndDate)
                            AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                            AND r.REPORTER_NAME IS NOT NULL
                        GROUP BY
                            r.REPORTER_NAME,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '')
                        ORDER BY 任务数量 DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取医生统计数据失败");
                dt = new DataTable();
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable GetCategoryStatistics(string startDate, string endDate, string system = "")
        {
            DataTable dt = new DataTable();
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空");
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add("请先配置数据库连接");
                return dt;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT
                            ISNULL(t.EXAM_CATEGORY_NAME, '') AS 检查类型,
                            t.EXEC_DEPT_NAME AS 所属科室,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '') AS 结果状态,
                            COUNT(*) AS 任务数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'P' THEN 1 ELSE 0 END), 0) AS 阳性数量,
                            ISNULL(SUM(CASE WHEN r.NEG_POS_CODE = 'N' THEN 1 ELSE 0 END), 0) AS 阴性数量,
                            ISNULL(AVG(CASE WHEN r.NEG_POS_CODE = 'P' THEN 100.0 ELSE 0 END), 0) AS 阳性率
                        FROM EXAM_TASK t
                        LEFT JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
                        WHERE t.IS_DEL = 0
                            AND t.CREATED_AT >= @StartDate
                            AND t.CREATED_AT < DATEADD(DAY, 1, @EndDate)
                            AND (@System IS NULL OR @System = '' OR t.SYSTEM_SOURCE_NO = @System)
                        GROUP BY
                            t.EXAM_CATEGORY_NAME,
                            t.EXEC_DEPT_NAME,
                            ISNULL(CASE r.NEG_POS_CODE WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' ELSE '未知' END, '')
                        ORDER BY 任务数量 DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取检查类型统计数据失败");
                dt = new DataTable();
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        /// <summary>
        /// 执行放射科工作量统计存储过程
        /// 来源：usp_RadiologyWorkloadStatistics
        /// </summary>
        public DataTable ExecuteRadiologyWorkloadStatistics(string startDate, string endDate, string system = "")
        {
            DataTable dt = new DataTable();
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空");
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add("请先配置数据库连接");
                return dt;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_TJFX_RadiologyWorkloadStatistics", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        LogHelper.LogInfo("执行存储过程: usp_TJFX_RadiologyWorkloadStatistics, 参数: startDate=" + startDate + ", endDate=" + endDate + ", system=" + system);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "执行存储过程 usp_TJFX_RadiologyWorkloadStatistics 失败");
                dt = new DataTable();
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        /// <summary>
        /// 执行放射科工作量统计明细存储过程
        /// 来源：usp_RadiologyWorkloadStatistics_Detail
        /// </summary>
        public DataTable ExecuteRadiologyWorkloadStatisticsDetail(string startDate, string endDate, string system = "")
        {
            DataTable dt = new DataTable();
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogError("数据库连接字符串为空");
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add("请先配置数据库连接");
                return dt;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_TJFX_RadiologyWorkloadStatistics_Detail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        LogHelper.LogInfo("执行存储过程: usp_TJFX_RadiologyWorkloadStatistics_Detail, 参数: startDate=" + startDate + ", endDate=" + endDate + ", system=" + system);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "执行存储过程 usp_TJFX_RadiologyWorkloadStatistics_Detail 失败");
                dt = new DataTable();
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        /// <summary>
        /// 获取查询配置（从数据库读取）
        /// 2026-04-29 修改：移除模拟数据回退，直接从数据库读取真实配置
        /// </summary>
        public string GetQueryConfig()
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空");
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"SELECT 
                        ID, FIELD_NAME, DISPLAY_NAME, IS_VISIBLE, IS_MULTIPLE, 
                        SORT_ORDER, PARENT_FIELD, DEFAULT_VALUE, PLACEHOLDER, IS_ACTIVE
                        FROM tjfx_QUERY_CONFIG
                        WHERE IS_ACTIVE = 1
                        ORDER BY SORT_ORDER";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var list = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            var item = new Dictionary<string, object>();
                            item.Add("id", reader["ID"]);
                            item.Add("fieldName", reader["FIELD_NAME"].ToString());
                            item.Add("displayName", reader["DISPLAY_NAME"].ToString());
                            item.Add("isVisible", reader["IS_VISIBLE"] != DBNull.Value && Convert.ToBoolean(reader["IS_VISIBLE"]));
                            item.Add("isMultiple", reader["IS_MULTIPLE"] != DBNull.Value && Convert.ToBoolean(reader["IS_MULTIPLE"]));
                            item.Add("sortOrder", reader["SORT_ORDER"] != DBNull.Value ? Convert.ToInt32(reader["SORT_ORDER"]) : 0);
                            
                            string parentField = reader["PARENT_FIELD"]?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(parentField))
                            {
                                item.Add("parentField", parentField);
                            }
                            
                            string defaultValue = reader["DEFAULT_VALUE"]?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(defaultValue))
                            {
                                item.Add("defaultValue", defaultValue);
                            }
                            
                            string placeholder = reader["PLACEHOLDER"]?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(placeholder))
                            {
                                item.Add("placeholder", placeholder);
                            }
                            
                            list.Add(item);
                        }

                        return string.Format("{{\"success\": true, \"data\": {0}}}",
                            Newtonsoft.Json.JsonConvert.SerializeObject(list));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取查询配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        /// <summary>
        /// 执行动态查询（从数据库读取真实数据）
        /// 2026-04-29 修改：移除模拟数据回退，直接从数据库读取真实数据
        /// </summary>
        public string ExecuteDynamicQuery(string fieldName, string parentValue = "")
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空");
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    List<Dictionary<string, string>> list;

                    if (fieldName == "patientType")
                    {
                        list = GetPatientTypesWithMapping(conn, parentValue);
                    }
                    else if (fieldName == "resultStatus")
                    {
                        list = GetResultStatusOptions(conn);
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_SafeExecuteDynamicQuery", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FieldName", fieldName);
                            cmd.Parameters.AddWithValue("@ParentValue", parentValue ?? "");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                list = new List<Dictionary<string, string>>();
                                while (reader.Read())
                                {
                                    string code = reader["code"]?.ToString()?.Trim() ?? "";
                                    string name = reader["name"]?.ToString()?.Trim() ?? "";
                                    if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name))
                                    {
                                        list.Add(new Dictionary<string, string>
                                        {
                                            { "code", code },
                                            { "name", name }
                                        });
                                    }
                                }
                            }
                        }
                    }

                    list = list.Where(item => !string.IsNullOrEmpty(item["code"]) && !string.IsNullOrEmpty(item["name"])).ToList();

                    return string.Format("{{\"success\": true, \"data\": {0}}}",
                        Newtonsoft.Json.JsonConvert.SerializeObject(list));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "执行动态查询失败: " + fieldName);
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        /// <summary>
        /// 初始化数据库配置表
        /// 2026-04-29 添加：用于创建配置表和存储过程
        /// </summary>
        public string InitializeConfigTables()
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogHelper.LogError("数据库连接字符串为空");
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\"}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    CreateQueryConfigTable(conn);
                    CreateQueryWhitelistTable(conn);
                    InsertConfigData(conn);
                    InsertWhitelistData(conn);
                    CreateStoredProcedures(conn);
                }

                LogHelper.LogInfo("数据库配置表初始化成功");
                return "{\"success\": true, \"message\": \"数据库配置表初始化成功\"}";
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "初始化数据库配置表失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        private void CreateQueryConfigTable(SqlConnection conn)
        {
            string sql = @"
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tjfx_QUERY_CONFIG' AND xtype='U')
BEGIN
    CREATE TABLE tjfx_QUERY_CONFIG (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        FIELD_NAME NVARCHAR(50) NOT NULL,
        DISPLAY_NAME NVARCHAR(100) NOT NULL,
        QUERY_SQL NVARCHAR(MAX),
        IS_VISIBLE BIT DEFAULT 1,
        IS_MULTIPLE BIT DEFAULT 0,
        SORT_ORDER INT DEFAULT 0,
        PARENT_FIELD NVARCHAR(50),
        DEFAULT_VALUE NVARCHAR(200),
        PLACEHOLDER NVARCHAR(100),
        IS_ACTIVE BIT DEFAULT 1,
        CREATE_TIME DATETIME DEFAULT GETDATE(),
        UPDATE_TIME DATETIME DEFAULT GETDATE()
    )
    CREATE UNIQUE INDEX UX_QUERY_FIELD_NAME ON tjfx_QUERY_CONFIG (FIELD_NAME)
END";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateQueryWhitelistTable(SqlConnection conn)
        {
            string sql = @"
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tjfx_QUERY_WHITELIST' AND xtype='U')
BEGIN
    CREATE TABLE tjfx_QUERY_WHITELIST (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        FIELD_NAME NVARCHAR(50) NOT NULL,
        ALLOWED_SQL NVARCHAR(MAX) NOT NULL,
        DESCRIPTION NVARCHAR(200),
        IS_ACTIVE BIT DEFAULT 1,
        CREATE_TIME DATETIME DEFAULT GETDATE()
    )
END";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertConfigData(SqlConnection conn)
        {
            string[] updateSqls = {
                "UPDATE tjfx_QUERY_CONFIG SET DISPLAY_NAME = '阴阳性', QUERY_SQL = 'SELECT DISTINCT NEG_POS_CODE as code, NEG_POS_CODE as name FROM EXAM_REPORT WHERE NEG_POS_CODE IS NOT NULL ORDER BY NEG_POS_CODE', PLACEHOLDER = '请选择阴阳性' WHERE FIELD_NAME = 'resultStatus'"
            };
            
            foreach (string sql in updateSqls)
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
            }

            string[] insertSqls = {
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'system') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('system', '系统', 'SELECT DISTINCT SYSTEM_SOURCE_NO as code, SYSTEM_SOURCE_NO as name FROM EXAM_TASK WHERE SYSTEM_SOURCE_NO IS NOT NULL ORDER BY SYSTEM_SOURCE_NO', 1, 0, 1, NULL, '请选择系统')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'reporter') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('reporter', '报告医生', 'SELECT DISTINCT r.REPORTER_NAME as code, r.REPORTER_NAME as name FROM EXAM_REPORT r INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID WHERE r.REPORTER_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY r.REPORTER_NAME', 1, 1, 2, 'system', '请选择报告医生')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'reviewer') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('reviewer', '审核医生', 'SELECT DISTINCT r.REVIEWER_NAME as code, r.REVIEWER_NAME as name FROM EXAM_REPORT r INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID WHERE r.REVIEWER_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY r.REVIEWER_NAME', 1, 1, 3, 'system', '请选择审核医生')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'technician') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('technician', '技师', 'SELECT DISTINCT t.TECHNICIAN_NAME as code, t.TECHNICIAN_NAME as name FROM EXAM_TASK t WHERE t.TECHNICIAN_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.TECHNICIAN_NAME', 1, 1, 4, 'system', '请选择技师')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'department') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('department', '执行科室', 'SELECT DISTINCT t.EXEC_DEPT_NAME as code, t.EXEC_DEPT_NAME as name FROM EXAM_TASK t WHERE t.EXEC_DEPT_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.EXEC_DEPT_NAME', 1, 1, 5, 'system', '请选择执行科室')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'category') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('category', '检查类型', 'SELECT DISTINCT t.EXAM_CATEGORY_NAME as code, t.EXAM_CATEGORY_NAME as name FROM EXAM_TASK t WHERE t.EXAM_CATEGORY_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.EXAM_CATEGORY_NAME', 1, 1, 6, 'system', '请选择检查类型')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'patientType') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('patientType', '病人类型', 'SELECT DISTINCT t.ENCOUNTER_TYPE_NO as code, t.ENCOUNTER_TYPE_NO as name FROM EXAM_TASK t WHERE t.ENCOUNTER_TYPE_NO IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.ENCOUNTER_TYPE_NO', 1, 1, 7, 'system', '请选择病人类型')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_CONFIG WHERE FIELD_NAME = 'resultStatus') INSERT INTO tjfx_QUERY_CONFIG (FIELD_NAME, DISPLAY_NAME, QUERY_SQL, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, PLACEHOLDER) VALUES ('resultStatus', '阴阳性', 'SELECT DISTINCT NEG_POS_CODE as code, NEG_POS_CODE as name FROM EXAM_REPORT WHERE NEG_POS_CODE IS NOT NULL ORDER BY NEG_POS_CODE', 1, 1, 8, NULL, '请选择阴阳性')"
            };

            foreach (string sql in insertSqls)
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertWhitelistData(SqlConnection conn)
        {
            string[] insertSqls = {
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'system') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('system', 'SELECT DISTINCT SYSTEM_SOURCE_NO as code, SYSTEM_SOURCE_NO as name FROM EXAM_TASK WHERE SYSTEM_SOURCE_NO IS NOT NULL ORDER BY SYSTEM_SOURCE_NO', '获取系统列表')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'reporter') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('reporter', 'SELECT DISTINCT r.REPORTER_NAME as code, r.REPORTER_NAME as name FROM EXAM_REPORT r INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID WHERE r.REPORTER_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY r.REPORTER_NAME', '获取报告医生列表')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'reviewer') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('reviewer', 'SELECT DISTINCT r.REVIEWER_NAME as code, r.REVIEWER_NAME as name FROM EXAM_REPORT r INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID WHERE r.REVIEWER_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY r.REVIEWER_NAME', '获取审核医生列表')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'technician') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('technician', 'SELECT DISTINCT t.TECHNICIAN_NAME as code, t.TECHNICIAN_NAME as name FROM EXAM_TASK t WHERE t.TECHNICIAN_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.TECHNICIAN_NAME', '获取技师列表')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'department') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('department', 'SELECT DISTINCT t.EXEC_DEPT_NAME as code, t.EXEC_DEPT_NAME as name FROM EXAM_TASK t WHERE t.EXEC_DEPT_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.EXEC_DEPT_NAME', '获取执行科室列表')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'category') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('category', 'SELECT DISTINCT t.EXAM_CATEGORY_NAME as code, t.EXAM_CATEGORY_NAME as name FROM EXAM_TASK t WHERE t.EXAM_CATEGORY_NAME IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.EXAM_CATEGORY_NAME', '获取检查类型列表')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'patientType') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('patientType', 'SELECT DISTINCT t.ENCOUNTER_TYPE_NO as code, t.ENCOUNTER_TYPE_NO as name FROM EXAM_TASK t WHERE t.ENCOUNTER_TYPE_NO IS NOT NULL AND (@PARENT_VALUE = '''' OR t.SYSTEM_SOURCE_NO = @PARENT_VALUE) ORDER BY t.ENCOUNTER_TYPE_NO', '获取病人类型列表')",
                "IF NOT EXISTS (SELECT * FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = 'resultStatus') INSERT INTO tjfx_QUERY_WHITELIST (FIELD_NAME, ALLOWED_SQL, DESCRIPTION) VALUES ('resultStatus', 'SELECT DISTINCT REPORT_RESULT_STATUS as code, REPORT_RESULT_STATUS as name FROM EXAM_REPORT WHERE REPORT_RESULT_STATUS IS NOT NULL ORDER BY REPORT_RESULT_STATUS', '获取结果状态列表')"
            };

            foreach (string sql in insertSqls)
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CreateStoredProcedures(SqlConnection conn)
        {
            string createUspGetQueryConfig = @"
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetQueryConfig')
BEGIN
    EXEC('CREATE PROCEDURE usp_GetQueryConfig AS BEGIN SELECT ID, FIELD_NAME, DISPLAY_NAME, IS_VISIBLE, IS_MULTIPLE, SORT_ORDER, PARENT_FIELD, DEFAULT_VALUE, PLACEHOLDER, IS_ACTIVE FROM tjfx_QUERY_CONFIG WHERE IS_ACTIVE = 1 ORDER BY SORT_ORDER END')
END";
            using (SqlCommand cmd = new SqlCommand(createUspGetQueryConfig, conn))
            {
                cmd.ExecuteNonQuery();
            }

            string createUspSafeExecute = @"
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_SafeExecuteDynamicQuery')
BEGIN
    EXEC('CREATE PROCEDURE usp_SafeExecuteDynamicQuery @FieldName NVARCHAR(50), @ParentValue NVARCHAR(200) = '''' AS BEGIN SET NOCOUNT ON; DECLARE @AllowedSQL NVARCHAR(MAX); SELECT @AllowedSQL = ALLOWED_SQL FROM tjfx_QUERY_WHITELIST WHERE FIELD_NAME = @FieldName AND IS_ACTIVE = 1; IF @AllowedSQL IS NULL BEGIN RAISERROR(''字段 %s 不在白名单中，无法执行查询'', 16, 1, @FieldName); RETURN; END DECLARE @ParamDef NVARCHAR(500); SET @ParamDef = ''@PARENT_VALUE NVARCHAR(200)''; BEGIN TRY EXEC sp_executesql @AllowedSQL, @ParamDef, @PARENT_VALUE = @ParentValue; END TRY BEGIN CATCH DECLARE @ErrorMessage NVARCHAR(4000); DECLARE @ErrorSeverity INT; DECLARE @ErrorState INT; SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE(); RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState); END CATCH END')
END";
            using (SqlCommand cmd = new SqlCommand(createUspSafeExecute, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public string GetHospitalName()
        {
            string connectionString = ConnectionStrings.GetConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                LogHelper.LogInfo("数据库连接字符串为空，使用默认医院名称");
                return "XX医院";
            }

            LogHelper.LogInfo("尝试从数据库获取医院名称...");

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    LogHelper.LogInfo("数据库连接成功");

                    string sql = @"SELECT TOP 1 CONFIG_VALUE FROM tjfx_SYS_CONFIG WHERE CONFIG_KEY = 'HOSPITAL_NAME' AND IS_DEL = 0";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            string hospitalName = result.ToString();
                            LogHelper.LogInfo("从tjfx_SYS_CONFIG获取医院名称: " + hospitalName);
                            return hospitalName;
                        }
                        LogHelper.LogInfo("tjfx_SYS_CONFIG表中未找到医院名称配置");
                    }

                    sql = @"SELECT TOP 1 HOSPITAL_NAME FROM EXAM_TASK WHERE HOSPITAL_NAME IS NOT NULL";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            string hospitalName = result.ToString();
                            LogHelper.LogInfo("从EXAM_TASK获取医院名称: " + hospitalName);
                            return hospitalName;
                        }
                        LogHelper.LogInfo("EXAM_TASK表中未找到医院名称配置");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取医院名称失败");
                LogHelper.LogError("数据库连接错误: " + ex.Message);
            }

            LogHelper.LogInfo("未找到医院名称配置，使用默认值");
            return "XX医院";
        }

        public string SearchStoredProcedures(string procName)
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\", \"data\": []}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    string sql = @"
                        SELECT 
                            p.name as name,
                            ISNULL(ep.value, '') as description
                        FROM sys.procedures p
                        LEFT JOIN sys.extended_properties ep ON p.object_id = ep.major_id AND ep.minor_id = 0 AND ep.name = 'MS_Description'
                        WHERE p.name LIKE @ProcName + '%'
                        ORDER BY p.name";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcName", procName);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var list = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                list.Add(new Dictionary<string, string>
                                {
                                    { "name", reader["name"].ToString() },
                                    { "description", reader["description"].ToString() }
                                });
                            }
                            return string.Format("{{\"success\": true, \"data\": {0}}}",
                                Newtonsoft.Json.JsonConvert.SerializeObject(list));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "搜索存储过程失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\", \"data\": []}";
            }
        }

        public string GetStoredProcedureMetadata(string procName)
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\"}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    var parameters = new List<Dictionary<string, object>>();
                    var columns = new List<Dictionary<string, object>>();

                    string paramSql = @"
                        SELECT 
                            p.name as name,
                            t.name as type,
                            CASE WHEN p.has_default_value = 1 THEN ISNULL(CAST(p.default_value AS VARCHAR(100)), '') ELSE '' END as defaultValue,
                            CASE WHEN p.is_nullable = 0 AND p.has_default_value = 0 THEN 1 ELSE 0 END as isRequired
                        FROM sys.parameters p
                        JOIN sys.types t ON p.system_type_id = t.system_type_id
                        WHERE p.object_id = OBJECT_ID(@ProcName)
                        ORDER BY p.parameter_id";

                    using (SqlCommand cmd = new SqlCommand(paramSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProcName", procName);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                parameters.Add(new Dictionary<string, object>
                                {
                                    { "name", reader["name"].ToString() },
                                    { "type", reader["type"].ToString() },
                                    { "defaultValue", reader["defaultValue"].ToString() },
                                    { "isRequired", Convert.ToBoolean(reader["isRequired"]) }
                                });
                            }
                        }
                    }

                    string testSql = $"SET FMTONLY ON; EXEC {procName}";
                    foreach (var param in parameters)
                    {
                        string paramName = param["name"].ToString();
                        testSql += $" {paramName}=NULL,";
                    }
                    if (parameters.Count > 0)
                    {
                        testSql = testSql.TrimEnd(',');
                    }
                    testSql += "; SET FMTONLY OFF";

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(testSql, conn))
                        {
                            cmd.CommandTimeout = 30;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    foreach (DataColumn col in reader.GetSchemaTable().Columns)
                                    {
                                        columns.Add(new Dictionary<string, object>
                                        {
                                            { "name", col.ColumnName },
                                            { "type", col.DataType.Name },
                                            { "width", 120 },
                                            { "sort", "none" },
                                            { "visible", true }
                                        });
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        columns.Clear();
                    }

                    if (columns.Count == 0)
                    {
                        string altSql = $"SELECT TOP 0 * FROM OPENROWSET('SQLNCLI', 'Server=localhost;Trusted_Connection=yes', 'SET FMTONLY ON; EXEC {procName}')";
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(altSql, conn))
                            {
                                cmd.CommandTimeout = 30;
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        foreach (DataColumn col in reader.GetSchemaTable().Columns)
                                        {
                                            columns.Add(new Dictionary<string, object>
                                            {
                                                { "name", col.ColumnName },
                                                { "type", col.DataType.Name },
                                                { "width", 120 },
                                                { "sort", "none" },
                                                { "visible", true }
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }

                    return string.Format("{{\"success\": true, \"parameters\": {0}, \"columns\": {1}}}",
                        Newtonsoft.Json.JsonConvert.SerializeObject(parameters),
                        Newtonsoft.Json.JsonConvert.SerializeObject(columns));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取存储过程元数据失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        public string ExecuteStoredProcedure(string jsonData)
        {
            try
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                string procName = data.ContainsKey("procName") ? data["procName"].ToString() : "";
                var parameters = data.ContainsKey("parameters") ? 
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(data["parameters"].ToString()) : 
                    new Dictionary<string, object>();

                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    return "{\"success\": false, \"error\": \"请先配置数据库连接\"}";
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        foreach (var param in parameters)
                        {
                            object value = param.Value;
                            if (value == null || value.ToString() == "")
                            {
                                cmd.Parameters.AddWithValue(param.Key, DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(param.Key, value);
                            }
                        }

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        return ConvertDataTableToJsonSimple(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "执行存储过程失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        private string ConvertDataTableToJsonSimple(DataTable dt)
        {
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    object value = dr[col] != DBNull.Value ? dr[col] : null;
                    row.Add(col.ColumnName, value);
                }
                rows.Add(row);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(rows);
        }

        public string SaveStoredProcConfig(string jsonData)
        {
            try
            {
                string configFile = System.IO.Path.Combine(AppContext.BaseDirectory, "proc_configs.dat");
                
                List<dynamic> configs = new List<dynamic>();
                if (System.IO.File.Exists(configFile))
                {
                    string existing = System.IO.File.ReadAllText(configFile);
                    if (!string.IsNullOrEmpty(existing))
                    {
                        configs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(existing);
                    }
                }

                var config = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonData);
                config.id = Guid.NewGuid().ToString();
                config.createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                config.updateTime = config.createTime;

                configs.Add(config);

                System.IO.File.WriteAllText(configFile, Newtonsoft.Json.JsonConvert.SerializeObject(configs, Newtonsoft.Json.Formatting.Indented));

                return "{\"success\": true, \"message\": \"配置保存成功\", \"id\": \"" + config.id + "\"}";
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "保存存储过程配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        public string GetStoredProcConfigs()
        {
            try
            {
                string configFile = System.IO.Path.Combine(AppContext.BaseDirectory, "proc_configs.dat");
                
                if (!System.IO.File.Exists(configFile))
                {
                    return "{\"success\": true, \"data\": []}";
                }

                string content = System.IO.File.ReadAllText(configFile);
                return "{\"success\": true, \"data\": " + content + "}";
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取存储过程配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }

        public string DeleteStoredProcConfig(string configId)
        {
            try
            {
                string configFile = System.IO.Path.Combine(AppContext.BaseDirectory, "proc_configs.dat");
                
                if (!System.IO.File.Exists(configFile))
                {
                    return "{\"success\": false, \"error\": \"配置文件不存在\"}";
                }

                string content = System.IO.File.ReadAllText(configFile);
                List<dynamic> configs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(content);
                
                int initialCount = configs.Count;
                configs.RemoveAll(c => c.id == configId);
                
                if (configs.Count == initialCount)
                {
                    return "{\"success\": false, \"error\": \"未找到要删除的配置\"}";
                }

                System.IO.File.WriteAllText(configFile, Newtonsoft.Json.JsonConvert.SerializeObject(configs, Newtonsoft.Json.Formatting.Indented));

                return "{\"success\": true, \"message\": \"配置删除成功\"}";
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "删除存储过程配置失败");
                return "{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}";
            }
        }
    }
}
