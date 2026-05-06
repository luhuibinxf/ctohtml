using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using DbProcedureCaller.Config;
using DbProcedureCaller.Core;
using DbProcedureCaller.Services;

namespace DbProcedureCaller.API
{
    public class ApiHandler
    {
        private UserService _userService;
        private DailyAnalysisService _dailyAnalysisService;
        private TokenService _tokenService;
        private PermissionService _permissionService;
        private StatConfigService _statConfigService;
        private SystemConfigService _systemConfigService;
        public static string RunningPort { get; set; } = "12345";

        public ApiHandler()
        {
            _userService = new UserService();
            _dailyAnalysisService = new DailyAnalysisService();
            _tokenService = new TokenService();
            _permissionService = new PermissionService();
            _statConfigService = new StatConfigService();
            _systemConfigService = new SystemConfigService();
        }

        public byte[] HandleRequest(string url, string httpMethod, Stream inputStream)
        {
            LogHelper.LogInfo($"API请求: {httpMethod} {url}");

            try
            {
                // Token登录
                if (url.StartsWith("/token-login") && httpMethod == "GET")
                {
                    return HandleTokenLogin(url);
                }
                // Token管理
                else if (url == "/get-tokens" && httpMethod == "GET")
                {
                    return HandleGetTokens();
                }
                else if (url == "/generate-token" && httpMethod == "POST")
                {
                    return HandleGenerateToken(inputStream);
                }
                else if (url == "/delete-token" && httpMethod == "POST")
                {
                    return HandleDeleteToken(inputStream);
                }
                
                if (url.StartsWith("/login") && httpMethod == "POST")
                {
                    return HandleLogin(inputStream);
                }
                else if (url == "/get-users" && httpMethod == "GET")
                {
                    return HandleGetUsers();
                }
                else if (url.StartsWith("/get-user") && httpMethod == "GET")
                {
                    return HandleGetUser(url);
                }
                else if (url.StartsWith("/check-admin") && httpMethod == "GET")
                {
                    return HandleCheckAdmin(url);
                }
                else if (url == "/add-user" && httpMethod == "POST")
                {
                    return HandleAddUser(inputStream);
                }
                else if (url.StartsWith("/update-user") && httpMethod == "POST")
                {
                    return HandleUpdateUser(url, inputStream);
                }
                else if (url.StartsWith("/delete-user") && httpMethod == "POST")
                {
                    return HandleDeleteUser(url, inputStream);
                }
                else if (url == "/daily-analysis" && httpMethod == "POST")
                {
                    return HandleDailyAnalysis(inputStream);
                }
                else if (url == "/department-statistics" && httpMethod == "POST")
                {
                    return HandleDepartmentStatistics(inputStream);
                }
                else if (url == "/doctor-statistics" && httpMethod == "POST")
                {
                    return HandleDoctorStatistics(inputStream);
                }
                else if (url == "/category-statistics" && httpMethod == "POST")
                {
                    return HandleCategoryStatistics(inputStream);
                }
                else if (url == "/execute-radiology-workload-summary" && httpMethod == "POST")
                {
                    return HandleExecuteRadiologyWorkloadSummary(inputStream);
                }
                else if (url == "/execute-radiology-workload-detail" && httpMethod == "POST")
                {
                    return HandleExecuteRadiologyWorkloadDetail(inputStream);
                }
                else if (url == "/get-query-config" && httpMethod == "GET")
                {
                    return HandleGetQueryConfig();
                }
                else if (url.StartsWith("/execute-dynamic-query"))
                {
                    return HandleExecuteDynamicQuery(url);
                }
                else if (url == "/get-hospital-info" && httpMethod == "GET")
                {
                    return HandleGetHospitalInfo();
                }
                else if (url == "/test-db-data" && httpMethod == "GET")
                {
                    return HandleTestDbData();
                }
                else if (url == "/test-db-connection" && httpMethod == "POST")
                {
                    return HandleTestDbConnection(inputStream);
                }
                else if (url == "/get-db-config" && httpMethod == "GET")
                {
                    return HandleGetDbConfig();
                }
                else if (url == "/update-db-config" && httpMethod == "POST")
                {
                    return HandleUpdateDbConfig(inputStream);
                }
                else if (url == "/get-port" && httpMethod == "GET")
                {
                    return HandleGetPort();
                }
                else if (url == "/set-port" && httpMethod == "POST")
                {
                    return HandleSetPort(inputStream);
                }
                else if (url == "/init-db" && httpMethod == "POST")
                {
                    return HandleInitDb();
                }
                else if (url.StartsWith("/get-all-options") && httpMethod == "GET")
                {
                    return HandleGetAllOptions(url);
                }
                else if (url == "/get-system-types" && httpMethod == "GET")
                {
                    return HandleGetSystemTypes();
                }
                else if (url.StartsWith("/get-reporters") && httpMethod == "GET")
                {
                    return HandleGetReporters(url);
                }
                else if (url.StartsWith("/get-reviewers") && httpMethod == "GET")
                {
                    return HandleGetReviewers(url);
                }
                else if (url.StartsWith("/get-categories") && httpMethod == "GET")
                {
                    return HandleGetCategories(url);
                }
                else if (url.StartsWith("/get-departments") && httpMethod == "GET")
                {
                    return HandleGetDepartments(url);
                }
                else if (url.StartsWith("/get-patient-types") && httpMethod == "GET")
                {
                    return HandleGetPatientTypes(url);
                }
                else if (url.StartsWith("/get-result-status") && httpMethod == "GET")
                {
                    return HandleGetResultStatus();
                }
                else if (url == "/get-roles" && httpMethod == "GET")
                {
                    return HandleGetRoles();
                }
                else if (url.StartsWith("/get-user-roles") && httpMethod == "GET")
                {
                    return HandleGetUserRoles(url);
                }
                else if (url == "/assign-role" && httpMethod == "POST")
                {
                    return HandleAssignRole(inputStream);
                }
                else if (url == "/remove-role" && httpMethod == "POST")
                {
                    return HandleRemoveRole(inputStream);
                }
                else if (url == "/get-menus" && httpMethod == "GET")
                {
                    return HandleGetMenus();
                }
                else if (url.StartsWith("/check-permission") && httpMethod == "GET")
                {
                    return HandleCheckPermission(url);
                }
                else if (url.StartsWith("/get-user-menus") && httpMethod == "GET")
                {
                    return HandleGetUserMenus(url);
                }
                else if (url.StartsWith("/check-analysis-permission") && httpMethod == "GET")
                {
                    return HandleCheckAnalysisPermission(url);
                }
                else if (url == "/shutdown" && httpMethod == "POST")
                {
                    return HandleShutdown();
                }
                else if (url == "/reset-config" && httpMethod == "POST")
                {
                    return HandleResetConfig();
                }
                else if (url == "/get-db-config" && httpMethod == "GET")
                {
                    return HandleGetDbConfig();
                }
                else if (url == "/search-stored-proc" && httpMethod == "POST")
                {
                    return HandleSearchStoredProc(inputStream);
                }
                else if (url == "/get-proc-metadata" && httpMethod == "POST")
                {
                    return HandleGetProcMetadata(inputStream);
                }
                else if (url == "/execute-stored-proc" && httpMethod == "POST")
                {
                    return HandleExecuteStoredProc(inputStream);
                }
                else if (url == "/save-proc-config" && httpMethod == "POST")
                {
                    return HandleSaveProcConfig(inputStream);
                }
                else if (url == "/get-proc-configs" && httpMethod == "GET")
                {
                    return HandleGetProcConfigs();
                }
                else if (url.StartsWith("/get-proc-config") && httpMethod == "GET")
                {
                    return HandleGetProcConfig(url);
                }
                else if (url.StartsWith("/delete-proc-config") && httpMethod == "POST")
                {
                    return HandleDeleteProcConfig(url, inputStream);
                }
                else if (url == "/get-upgrade-messages" && httpMethod == "GET")
                {
                    return HandleGetUpgradeMessages();
                }
                else if (url == "/save-param-config" && httpMethod == "POST")
                {
                    return HandleSaveParamConfig(inputStream);
                }
                else if (url == "/get-system-configs" && httpMethod == "GET")
                {
                    return HandleGetSystemConfigs();
                }
                else if (url.StartsWith("/get-system-config") && httpMethod == "GET")
                {
                    return HandleGetSystemConfig(url);
                }
                else if (url == "/update-system-config" && httpMethod == "POST")
                {
                    return HandleUpdateSystemConfig(inputStream);
                }
                else if (url == "/update-system-configs" && httpMethod == "POST")
                {
                    return HandleUpdateSystemConfigs(inputStream);
                }
                else if (url == "/system-configs" && httpMethod == "GET")
                {
                    return HandleGetSystemConfigs();
                }
                else if (url.StartsWith("/api/system-configs") && httpMethod == "GET")
                {
                    return HandleGetSystemConfigs();
                }
                else if (url.StartsWith("/api/menu-config-list") && httpMethod == "GET")
                {
                    return HandleGetMenuConfigList();
                }
                else if (url == "/cache-configs" && httpMethod == "GET")
                {
                    return HandleGetSystemConfigs();
                }
                else if (url == "/add-system-config" && httpMethod == "POST")
                {
                    return HandleAddSystemConfig(inputStream);
                }
                else if (url.StartsWith("/delete-system-config") && httpMethod == "POST")
                {
                    return HandleDeleteSystemConfig(url, inputStream);
                }
                else if (url == "/parse-sql-proc" && httpMethod == "POST")
                {
                    return HandleParseSqlProc(inputStream);
                }
                else
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"未知的API端点\"}");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "处理API请求失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleLogin(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string username = ExtractValue(postData, "username");
                string password = ExtractValue(postData, "password");

                LogHelper.LogInfo($"登录尝试: 用户名={username}");
                LogHelper.LogInfo($"登录尝试: 密码长度={password?.Length ?? 0}");

                try
                {
                    if (_userService.ValidateUser(username, password))
                    {
                        bool isAdmin = _userService.IsAdminUser(username);
                        LogHelper.LogInfo($"登录成功: 用户名={username}, 是管理员={isAdmin}");
                        return Encoding.UTF8.GetBytes("{\"success\": true, \"isAdmin\": " + isAdmin.ToString().ToLower() + "}");
                    }
                    else
                    {
                        LogHelper.LogInfo($"登录失败: 用户名={username}, 原因: 用户名或密码错误");
                        return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"用户名或密码错误\"}");
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogError($"登录异常: 用户名={username}, 错误: {ex.Message}");
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"登录异常: " + HttpUtility.HtmlEncode(ex.Message) + "\"}");
                }
            }
        }

        private byte[] HandleGetUsers()
        {
            string json = _userService.GetUsersJson();
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] HandleGetUser(string url)
        {
            string userIdStr = ExtractUrlParam(url, "id");
            int userId = int.TryParse(userIdStr, out int id) ? id : 0;
            
            string json = _userService.GetUserJson(userId);
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] HandleCheckAdmin(string url)
        {
            string username = ExtractUrlParam(url, "username");
            
            LogHelper.LogInfo($"检查用户是否是管理员: {username}");

            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": true, \"isAdmin\": false, \"message\": \"用户名为空\"}");
                }
                
                bool isAdmin = _userService.IsAdminUser(username);
                LogHelper.LogInfo($"用户 {username} 的管理员检查结果: {isAdmin}");
                return Encoding.UTF8.GetBytes($"{{\"success\": true, \"isAdmin\": {isAdmin.ToString().ToLower()}, \"username\": \"{HttpUtility.HtmlEncode(username)}\"}}");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "检查管理员权限失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleAddUser(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string idStr = ExtractValue(postData, "id");
                int id = string.IsNullOrEmpty(idStr) ? 0 : (int.TryParse(idStr, out int parsedId) ? parsedId : 0);
                string username = ExtractValue(postData, "username");
                string password = ExtractValue(postData, "password");
                string role = ExtractValue(postData, "role");
                string status = ExtractValue(postData, "status");

                bool success = _userService.AddUser(id, username, password, role, status);
                if (success)
                {
                    return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"用户添加成功\"}");
                }
                else
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"用户ID或用户名已存在\"}");
                }
            }
        }

        private byte[] HandleUpdateUser(string url, Stream inputStream)
        {
            // 从URL获取原始ID（用于定位要更新的记录）
            string originalIdStr = ExtractUrlParam(url, "id");
            int originalId = int.TryParse(originalIdStr, out int parsedId) ? parsedId : 0;
            
            if (originalId <= 0)
            {
                return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"用户ID无效\"}");
            }
            
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string newIdStr = ExtractValue(postData, "id");
                int newId = string.IsNullOrEmpty(newIdStr) ? originalId : (int.TryParse(newIdStr, out int parsedNewId) ? parsedNewId : originalId);
                
                string username = ExtractValue(postData, "username");
                string password = ExtractValue(postData, "password");
                string role = ExtractValue(postData, "role");
                string status = ExtractValue(postData, "status");

                bool success = _userService.UpdateUser(originalId, newId, username, password, role, status);
                if (success)
                {
                    return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"用户更新成功\"}");
                }
                else
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"用户更新失败或管理员用户不可修改\"}");
                }
            }
        }

        private byte[] HandleDeleteUser(string url, Stream inputStream)
        {
            string idStr = ExtractUrlParam(url, "id");
            int id = int.TryParse(idStr, out int parsedId) ? parsedId : 0;
            
            if (id <= 0)
            {
                return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"用户ID无效\"}");
            }

            bool success = _userService.DeleteUser(id);
            if (success)
            {
                return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"用户删除成功\"}");
            }
            else
            {
                return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"用户删除失败，管理员用户不可删除\"}");
            }
        }

        private byte[] HandleDailyAnalysis(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string startDate = ExtractValue(postData, "startDate");
                string endDate = ExtractValue(postData, "endDate");
                string system = ExtractValue(postData, "system");
                string reporter = ExtractValue(postData, "reporter");
                string reviewer = ExtractValue(postData, "reviewer");
                string technician = ExtractValue(postData, "technician");
                string department = ExtractValue(postData, "department");
                string category = ExtractValue(postData, "category");
                string patientType = ExtractValue(postData, "patientType");
                string resultStatus = ExtractValue(postData, "resultStatus");
                string groupBy = ExtractValue(postData, "groupBy");
                string sortBy = ExtractValue(postData, "sortBy");
                string sortOrder = ExtractValue(postData, "sortOrder");
                int pageSize = int.TryParse(ExtractValue(postData, "pageSize"), out int ps) ? ps : 0;
                int pageIndex = int.TryParse(ExtractValue(postData, "pageIndex"), out int pi) ? pi : 1;

                LogHelper.LogInfo($"每日分析请求: startDate={startDate}, endDate={endDate}, system={system}, reporter={reporter}, reviewer={reviewer}, technician={technician}, department={department}, category={category}, patientType={patientType}, resultStatus={resultStatus}, groupBy={groupBy}, sortBy={sortBy}, sortOrder={sortOrder}, pageSize={pageSize}, pageIndex={pageIndex}");

                DataTable result = _dailyAnalysisService.GetAnalysisData(
                    startDate, endDate, system, reporter, reviewer, technician, department, category, patientType, resultStatus,
                    groupBy, sortBy, sortOrder, pageSize, pageIndex);

                var fieldMapping = new Dictionary<string, string>
                {
                    { "执行科室", "department" },
                    { "科室", "department" },
                    { "检查类型", "category" },
                    { "报告医生", "reporter" },
                    { "审核医生", "reviewer" },
                    { "系统", "system" },
                    { "检查系统", "system" },
                    { "技师", "technician" },
                    { "病人类型", "patientType" },
                    { "结果状态", "resultStatus" },
                    { "阴阳性", "resultStatus" },
                    { "任务数量", "total" },
                    { "检查总次数", "total" },
                    { "阳性数量", "positive" },
                    { "阳性数", "positive" },
                    { "阴性数量", "negative" },
                    { "阴性数", "negative" },
                    { "阳性率", "positiveRate" }
                };

                var rows = new List<Dictionary<string, object>>();
                foreach (DataRow dr in result.Rows)
                {
                    var row = new Dictionary<string, object>();
                    foreach (DataColumn col in result.Columns)
                    {
                        string fieldName = col.ColumnName;
                        if (fieldMapping.ContainsKey(fieldName))
                        {
                            fieldName = fieldMapping[fieldName];
                        }

                        object value = dr[col] != DBNull.Value ? dr[col] : null;
                        if (value is string)
                        {
                            row.Add(fieldName, HttpUtility.HtmlEncode(value.ToString()));
                        }
                        else if (value is decimal || value is int)
                        {
                            row.Add(fieldName, Convert.ToDouble(value));
                        }
                        else
                        {
                            row.Add(fieldName, value);
                        }
                    }
                    if (!row.ContainsKey("total")) row["total"] = 0;
                    if (!row.ContainsKey("positive")) row["positive"] = 0;
                    if (!row.ContainsKey("negative")) row["negative"] = 0;
                    if (!row.ContainsKey("positiveRate")) row["positiveRate"] = 0;
                    rows.Add(row);
                }

                var summary = new Dictionary<string, object>();
                if (rows.Count > 0)
                {
                    decimal totalCount = 0;
                    decimal positiveCount = 0;
                    foreach (var row in rows)
                    {
                        if (row.ContainsKey("total") && row["total"] != null)
                        {
                            totalCount += Convert.ToDecimal(row["total"]);
                        }
                        else if (row.ContainsKey("任务数量") && row["任务数量"] != null)
                        {
                            totalCount += Convert.ToDecimal(row["任务数量"]);
                        }
                        if (row.ContainsKey("positive") && row["positive"] != null)
                        {
                            positiveCount += Convert.ToDecimal(row["positive"]);
                        }
                        else if (row.ContainsKey("阳性数") && row["阳性数"] != null)
                        {
                            positiveCount += Convert.ToDecimal(row["阳性数"]);
                        }
                    }
                    summary["totalCount"] = totalCount;
                    summary["positiveCount"] = positiveCount;
                    summary["negativeCount"] = totalCount - positiveCount;
                    summary["positiveRate"] = totalCount > 0 ? (double)(positiveCount / totalCount) : 0;
                }

                var response = new
                {
                    success = true,
                    data = new
                    {
                        summary = summary,
                        details = rows
                    }
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                return Encoding.UTF8.GetBytes(json);
            }
        }

        private byte[] HandleDepartmentStatistics(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string startDate = ExtractValue(postData, "startDate");
                string endDate = ExtractValue(postData, "endDate");
                string system = ExtractValue(postData, "system");

                LogHelper.LogInfo($"科室统计请求: startDate={startDate}, endDate={endDate}, system={system}");

                DataTable result = _dailyAnalysisService.GetDepartmentStatistics(startDate, endDate, system);

                string json = ConvertDataTableToJson(result);
                return Encoding.UTF8.GetBytes(json);
            }
        }

        private byte[] HandleDoctorStatistics(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string startDate = ExtractValue(postData, "startDate");
                string endDate = ExtractValue(postData, "endDate");
                string system = ExtractValue(postData, "system");
                string doctorType = ExtractValue(postData, "doctorType");

                LogHelper.LogInfo($"医生统计请求: startDate={startDate}, endDate={endDate}, system={system}, doctorType={doctorType}");

                DataTable result = _dailyAnalysisService.GetDoctorStatistics(startDate, endDate, system, doctorType);

                string json = ConvertDataTableToJson(result);
                return Encoding.UTF8.GetBytes(json);
            }
        }

        private byte[] HandleCategoryStatistics(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string startDate = ExtractValue(postData, "startDate");
                string endDate = ExtractValue(postData, "endDate");
                string system = ExtractValue(postData, "system");

                LogHelper.LogInfo($"检查类型统计请求: startDate={startDate}, endDate={endDate}, system={system}");

                DataTable result = _dailyAnalysisService.GetCategoryStatistics(startDate, endDate, system);

                string json = ConvertDataTableToJson(result);
                return Encoding.UTF8.GetBytes(json);
            }
        }

        private byte[] HandleExecuteRadiologyWorkloadSummary(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string startDate = ExtractValue(postData, "startDate");
                string endDate = ExtractValue(postData, "endDate");
                string system = ExtractValue(postData, "system");

                LogHelper.LogInfo($"执行放射科工作量统计汇总: startDate={startDate}, endDate={endDate}, system={system}");

                DataTable result = _dailyAnalysisService.ExecuteRadiologyWorkloadStatistics(startDate, endDate, system);

                string json = ConvertDataTableToJson(result);
                return Encoding.UTF8.GetBytes(json);
            }
        }

        private byte[] HandleExecuteRadiologyWorkloadDetail(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string startDate = ExtractValue(postData, "startDate");
                string endDate = ExtractValue(postData, "endDate");
                string system = ExtractValue(postData, "system");

                LogHelper.LogInfo($"执行放射科工作量统计明细: startDate={startDate}, endDate={endDate}, system={system}");

                DataTable result = _dailyAnalysisService.ExecuteRadiologyWorkloadStatisticsDetail(startDate, endDate, system);

                string json = ConvertDataTableToJson(result);
                return Encoding.UTF8.GetBytes(json);
            }
        }

        private byte[] HandleGetQueryConfig()
        {
            string json = _dailyAnalysisService.GetQueryConfig();
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] HandleExecuteDynamicQuery(string url)
        {
            string fieldName = "";
            string parentValue = "";
            
            if (url.Contains("?"))
            {
                string queryString = url.Substring(url.IndexOf("?") + 1);
                string[] parameters = queryString.Split('&');
                foreach (string param in parameters)
                {
                    string[] keyValue = param.Split('=');
                    if (keyValue.Length == 2)
                    {
                        if (keyValue[0] == "fieldName")
                            fieldName = System.Net.WebUtility.UrlDecode(keyValue[1]);
                        else if (keyValue[0] == "parentValue")
                            parentValue = System.Net.WebUtility.UrlDecode(keyValue[1]);
                    }
                }
            }

            LogHelper.LogInfo($"动态查询请求: fieldName={fieldName}, parentValue={parentValue}");
            string json = _dailyAnalysisService.ExecuteDynamicQuery(fieldName, parentValue);
            return Encoding.UTF8.GetBytes(json);
        }

        private string ExtractValue(string data, string key)
        {
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(key))
                return "";

            if (data.TrimStart().StartsWith("{"))
            {
                string pattern = string.Format("\"{0}\":", key);
                int startSearch = data.IndexOf(pattern);
                if (startSearch >= 0)
                {
                    startSearch += pattern.Length;
                    startSearch = data.IndexOfAny(new char[] { '"', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '{', '[', 't', 'f', 'n' }, startSearch);
                    if (startSearch >= 0)
                    {
                        if (data[startSearch] == '"')
                        {
                            startSearch++;
                            int endIndex = data.IndexOf('"', startSearch);
                            if (endIndex > startSearch)
                            {
                                return data.Substring(startSearch, endIndex - startSearch);
                            }
                        }
                        else if (data[startSearch] == '[')
                        {
                            int endIndex = FindMatchingBracket(data, startSearch, '[', ']');
                            if (endIndex > startSearch)
                            {
                                return data.Substring(startSearch, endIndex - startSearch + 1);
                            }
                        }
                        else if (data[startSearch] == '{')
                        {
                            int endIndex = FindMatchingBracket(data, startSearch, '{', '}');
                            if (endIndex > startSearch)
                            {
                                return data.Substring(startSearch, endIndex - startSearch + 1);
                            }
                        }
                        else
                        {
                            int endIndex = data.IndexOfAny(new char[] { ',', '}', ']', ' ', '\n', '\r' }, startSearch);
                            if (endIndex > startSearch)
                            {
                                return data.Substring(startSearch, endIndex - startSearch).Trim();
                            }
                            else if (endIndex == -1)
                            {
                                return data.Substring(startSearch).Trim();
                            }
                        }
                    }
                }
            }
            else
            {
                string formPattern = key + "=";
                int formStart = data.IndexOf(formPattern);
                if (formStart >= 0)
                {
                    formStart += formPattern.Length;
                    int formEnd = data.IndexOf('&', formStart);
                    if (formEnd > formStart)
                    {
                        return System.Net.WebUtility.UrlDecode(data.Substring(formStart, formEnd - formStart));
                    }
                    else if (formEnd == -1)
                    {
                        return System.Net.WebUtility.UrlDecode(data.Substring(formStart));
                    }
                }
            }
            return "";
        }
        
        private int FindMatchingBracket(string data, int startIndex, char openBracket, char closeBracket)
        {
            int depth = 1;
            for (int i = startIndex + 1; i < data.Length; i++)
            {
                if (data[i] == openBracket)
                    depth++;
                else if (data[i] == closeBracket)
                    depth--;
                
                if (depth == 0)
                    return i;
            }
            return -1;
        }

        private byte[] HandleGetHospitalInfo()
        {
            string hospitalName = _dailyAnalysisService.GetHospitalName();
            string encodedName = HttpUtility.HtmlEncode(hospitalName);
            return Encoding.UTF8.GetBytes($"{{\"success\": true, \"hospitalName\": \"{encodedName}\"}}");
        }

        private byte[] HandleTestDbConnection(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string server = ExtractValue(postData, "server");
                string database = ExtractValue(postData, "database");
                string username = ExtractValue(postData, "username");
                string password = ExtractValue(postData, "password");

                LogHelper.LogInfo($"测试数据库连接: Server={server}, Database={database}, User={username}");

                if (string.IsNullOrEmpty(server))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"服务器地址不能为空\"}");
                }
                if (string.IsNullOrEmpty(database))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"数据库名称不能为空\"}");
                }
                if (string.IsNullOrEmpty(username))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"用户名不能为空\"}");
                }
                if (string.IsNullOrEmpty(password))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"密码不能为空\"}");
                }

                string connectionString = $"Server={server};Database={database};User ID={username};Password={password};Integrated Security=False;TrustServerCertificate=True;";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        LogHelper.LogInfo("数据库连接测试成功");
                        return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"数据库连接成功！\"}");
                    }
                }
                catch (SqlException ex)
                {
                    LogHelper.LogException(ex, "数据库连接测试失败");
                    string errorDetail = ex.Message;
                    if (ex.Number == 18456)
                    {
                        errorDetail = "登录失败：用户名或密码错误";
                    }
                    else if (ex.Number == 4060)
                    {
                        errorDetail = "无法打开数据库 '" + database + "'";
                    }
                    else if (ex.Number == 1326)
                    {
                        errorDetail = "登录失败：用户名或密码错误";
                    }
                    else if (ex.Number == 53)
                    {
                        errorDetail = "无法连接到服务器：网络错误或服务器不存在";
                    }
                    else if (ex.Number == 2)
                    {
                        errorDetail = "无法连接到服务器：服务器名称无效或端口未开放";
                    }
                    return Encoding.UTF8.GetBytes($"{{\"success\": false, \"error\": \"{HttpUtility.HtmlEncode(errorDetail)}\", \"errorCode\": {ex.Number}}}");
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "数据库连接测试失败");
                    return Encoding.UTF8.GetBytes($"{{\"success\": false, \"error\": \"{HttpUtility.HtmlEncode(ex.Message)}\"}}");
                }
            }
        }

        private byte[] HandleGetDbConfig()
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"未配置数据库连接\"}");
                }

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                
                string server = HttpUtility.HtmlEncode(builder.DataSource ?? "");
                string database = HttpUtility.HtmlEncode(builder.InitialCatalog ?? "");
                string username = HttpUtility.HtmlEncode(builder.UserID ?? "");
                
                string password = HttpUtility.HtmlEncode(builder.Password ?? "");
                
                return Encoding.UTF8.GetBytes(
                    "{\"success\": true, \"data\": {\"server\": \"" + server + "\", \"database\": \"" + database + "\", \"username\": \"" + username + "\", \"password\": \"" + password + "\"}}");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取数据库配置失败");
                return Encoding.UTF8.GetBytes($"{{\"success\": false, \"error\": \"{HttpUtility.HtmlEncode(ex.Message)}\"}}");
            }
        }

        private byte[] HandleTestDbData()
        {
            try
            {
                string connectionString = ConnectionStrings.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"数据库连接字符串为空\"}");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string taskCountSql = "SELECT COUNT(*) FROM EXAM_TASK WITH(NOLOCK) WHERE IS_DEL = 0";
                    string reportCountSql = "SELECT COUNT(*) FROM EXAM_REPORT WITH(NOLOCK)";
                    string dateRangeSql = "SELECT MIN(CREATED_AT) as MinDate, MAX(CREATED_AT) as MaxDate FROM EXAM_TASK WITH(NOLOCK) WHERE IS_DEL = 0";

                    int taskCount = 0;
                    int reportCount = 0;
                    string minDate = "";
                    string maxDate = "";

                    using (SqlCommand cmd = new SqlCommand(taskCountSql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        taskCount = result != null ? (int)result : 0;
                    }

                    using (SqlCommand cmd = new SqlCommand(reportCountSql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        reportCount = result != null ? (int)result : 0;
                    }

                    using (SqlCommand cmd = new SqlCommand(dateRangeSql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                minDate = reader["MinDate"]?.ToString() ?? "";
                                maxDate = reader["MaxDate"]?.ToString() ?? "";
                            }
                        }
                    }

                    return Encoding.UTF8.GetBytes(
                        $"{{\"success\": true, \"taskCount\": {taskCount}, \"reportCount\": {reportCount}, \"minDate\": \"{minDate}\", \"maxDate\": \"{maxDate}\"}}");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "测试数据库数据失败");
                return Encoding.UTF8.GetBytes($"{{\"success\": false, \"error\": \"{ex.Message}\"}}");
            }
        }

        private byte[] HandleUpdateDbConfig(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string server = ExtractValue(postData, "server");
                string database = ExtractValue(postData, "database");
                string username = ExtractValue(postData, "username");
                string password = ExtractValue(postData, "password");

                LogHelper.LogInfo("尝试更新数据库配置");

                try
                {
                    string connectionString = $"Server={server};Database={database};User ID={username};Password={password};Integrated Security=False;TrustServerCertificate=True;";
                    
                    DbProcedureCaller.Config.ConnectionStrings.ClearCache();
                    
                    System.IO.File.WriteAllText(@"d:\AI\tran\config.dat", connectionString);
                    
                    LogHelper.LogInfo("数据库配置更新成功");
                    return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"数据库配置更新成功，请重启程序生效\"}");
                }
                catch (Exception ex)
                {
                    LogHelper.LogError($"更新数据库配置失败: {ex.Message}");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleGetPort()
        {
            try
            {
                string exeDir = AppContext.BaseDirectory;
                string configFile = System.IO.Path.Combine(exeDir, "server_config.dat");
                string configPort = "9094";

                if (System.IO.File.Exists(configFile))
                {
                    string encrypted = System.IO.File.ReadAllText(configFile).Trim();
                    try
                    {
                        byte[] data = Convert.FromBase64String(encrypted);
                        configPort = Encoding.UTF8.GetString(data);
                    }
                    catch { }
                }

                return Encoding.UTF8.GetBytes($"{{\"success\": true, \"data\": {{\"port\": \"{configPort}\", \"runningPort\": \"{RunningPort}\"}}}}");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取端口配置失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleSetPort(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string port = ExtractValue(postData, "port");

                if (string.IsNullOrEmpty(port))
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"端口号不能为空\"}");
                }

                int portNum;
                if (!int.TryParse(port, out portNum) || portNum < 1 || portNum > 65535)
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"端口号必须在1-65535之间\"}");
                }

                try
                {
                    string exeDir = AppContext.BaseDirectory;
                    string configFile = System.IO.Path.Combine(exeDir, "server_config.dat");
                    byte[] data = Encoding.UTF8.GetBytes(port);
                    string encrypted = Convert.ToBase64String(data);
                    System.IO.File.WriteAllText(configFile, encrypted);
                    
                    LogHelper.LogInfo($"端口配置已保存: {port}");
                    return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"端口配置已保存，重启程序后生效\"}");
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "保存端口配置失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleInitDb()
        {
            LogHelper.LogInfo("开始初始化数据库配置表");
            try
            {
                string result = _dailyAnalysisService.InitializeConfigTables();
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "初始化数据库配置表失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetAllOptions(string url)
        {
            LogHelper.LogInfo("获取所有下拉框选项（聚合API）");
            try
            {
                string system = ExtractUrlParam(url, "system");
                string result = _dailyAnalysisService.GetAllOptions(system);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取所有下拉框选项失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetSystemTypes()
        {
            LogHelper.LogInfo("获取系统类型列表");
            try
            {
                string result = _dailyAnalysisService.GetSystemTypes();
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取系统类型失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetReporters(string url)
        {
            LogHelper.LogInfo("获取报告医生列表");
            try
            {
                string system = ExtractUrlParam(url, "system");
                string result = _dailyAnalysisService.GetReporters(system);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取报告医生失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetReviewers(string url)
        {
            LogHelper.LogInfo("获取审核医生列表");
            try
            {
                string system = ExtractUrlParam(url, "system");
                string result = _dailyAnalysisService.GetReviewers(system);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取审核医生失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetCategories(string url)
        {
            LogHelper.LogInfo("获取检查类型列表");
            try
            {
                string system = ExtractUrlParam(url, "system");
                string result = _dailyAnalysisService.GetCategories(system);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取检查类型失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetDepartments(string url)
        {
            LogHelper.LogInfo("获取执行科室列表");
            try
            {
                string system = ExtractUrlParam(url, "system");
                string result = _dailyAnalysisService.GetDepartments(system);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取执行科室失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetPatientTypes(string url)
        {
            LogHelper.LogInfo("获取病人类型列表");
            try
            {
                string system = ExtractUrlParam(url, "system");
                string result = _dailyAnalysisService.GetPatientTypes(system);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取病人类型失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetResultStatus()
        {
            LogHelper.LogInfo("获取结果状态列表");
            try
            {
                string result = _dailyAnalysisService.GetResultStatus();
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取结果状态失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private string ExtractUrlParam(string url, string paramName)
        {
            if (url.Contains("?" + paramName + "="))
            {
                string param = url.Substring(url.IndexOf("?" + paramName + "=") + paramName.Length + 2);
                if (param.Contains("&"))
                {
                    param = param.Substring(0, param.IndexOf("&"));
                }
                return System.Net.WebUtility.UrlDecode(param);
            }
            return "";
        }

        /// <summary>
        /// 将DataTable转换为JSON字符串（带XSS防护）
        /// 2026-04-29 修改：添加HTML编码防止XSS攻击
        /// </summary>
        /// <param name="dt">DataTable数据</param>
        /// <returns>JSON字符串</returns>
        private string ConvertDataTableToJson(DataTable dt)
        {
            var rows = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                var row = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    object value = dr[col] != DBNull.Value ? dr[col] : null;
                    if (value is string)
                    {
                        // XSS防护：对字符串值进行HTML编码
                        row.Add(col.ColumnName, HttpUtility.HtmlEncode(value.ToString()));
                    }
                    else
                    {
                        row.Add(col.ColumnName, value);
                    }
                }
                rows.Add(row);
            }
            return string.Format("{{\"success\": true, \"data\": {0}}}",
                Newtonsoft.Json.JsonConvert.SerializeObject(rows));
        }

        /// <summary>
        /// 创建统一的错误响应（带XSS防护）
        /// 2026-04-29 新增：统一错误处理机制，确保错误消息经过HTML编码
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>错误响应字节数组</returns>
        private byte[] CreateErrorResponse(string errorMessage)
        {
            string encodedMessage = HttpUtility.HtmlEncode(errorMessage);
            return Encoding.UTF8.GetBytes($"{{\"success\": false, \"error\": \"{encodedMessage}\"}}");
        }

        /// <summary>
        /// Token登录
        /// </summary>
        private byte[] HandleTokenLogin(string url)
        {
            string token = ExtractUrlParam(url, "token");
            
            if (string.IsNullOrEmpty(token))
            {
                return CreateErrorResponse("Token不能为空");
            }

            int? userId = _tokenService.ValidateToken(token);
            if (userId.HasValue)
            {
                string username = _tokenService.GetUsernameById(userId.Value);
                if (!string.IsNullOrEmpty(username))
                {
                    bool isAdmin = _userService.IsAdminUser(username);
                    LogHelper.LogInfo($"Token登录成功: {username}");
                    return Encoding.UTF8.GetBytes($"{{\"success\": true, \"username\": \"{HttpUtility.HtmlEncode(username)}\", \"isAdmin\": {isAdmin.ToString().ToLower()}}}");
                }
            }

            return CreateErrorResponse("Token无效或已过期");
        }

        /// <summary>
        /// 获取Token列表
        /// </summary>
        private byte[] HandleGetTokens()
        {
            string result = _tokenService.GetTokensJson();
            return Encoding.UTF8.GetBytes(result);
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        private byte[] HandleGenerateToken(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                int userId = int.TryParse(ExtractValue(postData, "userId"), out int id) ? id : 0;
                int expireHours = int.TryParse(ExtractValue(postData, "expireHours"), out int hours) ? hours : 24;

                if (userId <= 0)
                {
                    return CreateErrorResponse("用户ID不能为空");
                }

                string token = _tokenService.GenerateToken(userId, expireHours);
                if (!string.IsNullOrEmpty(token))
                {
                    return Encoding.UTF8.GetBytes($"{{\"success\": true, \"token\": \"{token}\"}}");
                }

                return CreateErrorResponse("Token生成失败");
            }
        }

        /// <summary>
        /// 删除Token
        /// </summary>
        private byte[] HandleDeleteToken(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                int tokenId = int.TryParse(ExtractValue(postData, "tokenId"), out int id) ? id : 0;

                if (tokenId <= 0)
                {
                    return CreateErrorResponse("Token ID不能为空");
                }

                bool success = _tokenService.DeleteToken(tokenId);
                if (success)
                {
                    return Encoding.UTF8.GetBytes($"{{\"success\": true}}");
                }

                return CreateErrorResponse("Token删除失败");
            }
        }

        private byte[] HandleGetRoles()
        {
            LogHelper.LogInfo("获取角色列表");
            string result = _permissionService.GetRolesJson();
            return Encoding.UTF8.GetBytes(result);
        }

        private byte[] HandleGetUserRoles(string url)
        {
            string userIdStr = ExtractUrlParam(url, "userId");
            int userId = int.TryParse(userIdStr, out int id) ? id : 0;
            
            LogHelper.LogInfo($"获取用户角色: userId={userId}");
            
            if (userId <= 0)
            {
                return CreateErrorResponse("用户ID无效");
            }
            
            string result = _permissionService.GetUserRolesJson(userId);
            return Encoding.UTF8.GetBytes(result);
        }

        private byte[] HandleAssignRole(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                int userId = int.TryParse(ExtractValue(postData, "userId"), out int uid) ? uid : 0;
                int roleId = int.TryParse(ExtractValue(postData, "roleId"), out int rid) ? rid : 0;

                LogHelper.LogInfo($"分配角色: userId={userId}, roleId={roleId}");

                if (userId <= 0)
                {
                    return CreateErrorResponse("用户ID不能为空");
                }
                if (roleId <= 0)
                {
                    return CreateErrorResponse("角色ID不能为空");
                }

                bool success = _permissionService.AssignRoleToUser(userId, roleId);
                if (success)
                {
                    return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"角色分配成功\"}");
                }

                return CreateErrorResponse("角色分配失败");
            }
        }

        private byte[] HandleRemoveRole(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                int userId = int.TryParse(ExtractValue(postData, "userId"), out int uid) ? uid : 0;
                int roleId = int.TryParse(ExtractValue(postData, "roleId"), out int rid) ? rid : 0;

                LogHelper.LogInfo($"移除角色: userId={userId}, roleId={roleId}");

                if (userId <= 0)
                {
                    return CreateErrorResponse("用户ID不能为空");
                }
                if (roleId <= 0)
                {
                    return CreateErrorResponse("角色ID不能为空");
                }

                bool success = _permissionService.RemoveRoleFromUser(userId, roleId);
                if (success)
                {
                    return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"角色移除成功\"}");
                }

                return CreateErrorResponse("角色移除失败");
            }
        }

        private byte[] HandleGetMenus()
        {
            LogHelper.LogInfo("获取菜单配置");
            string result = _permissionService.GetMenusJson();
            return Encoding.UTF8.GetBytes(result);
        }

        private byte[] HandleCheckPermission(string url)
        {
            string username = ExtractUrlParam(url, "username");
            string permission = ExtractUrlParam(url, "permission");

            LogHelper.LogInfo($"检查权限: username={username}, permission={permission}");

            if (string.IsNullOrEmpty(username))
            {
                return CreateErrorResponse("用户名不能为空");
            }
            if (string.IsNullOrEmpty(permission))
            {
                return CreateErrorResponse("权限名称不能为空");
            }

            bool hasPermission = _permissionService.HasPermission(username, permission);
            return Encoding.UTF8.GetBytes($"{{\"success\": true, \"hasPermission\": {hasPermission.ToString().ToLower()}}}");
        }

        private byte[] HandleGetUserMenus(string url)
        {
            string username = ExtractUrlParam(url, "username");

            LogHelper.LogInfo($"获取用户菜单: username={username}");

            if (string.IsNullOrEmpty(username))
            {
                return CreateErrorResponse("用户名不能为空");
            }

            string result = _permissionService.GetMenusByUserJson(username);
            return Encoding.UTF8.GetBytes(result);
        }

        private byte[] HandleCheckAnalysisPermission(string url)
        {
            string username = ExtractUrlParam(url, "username");
            string analysisType = ExtractUrlParam(url, "analysisType");

            LogHelper.LogInfo($"检查分析权限: username={username}, analysisType={analysisType}");

            if (string.IsNullOrEmpty(username))
            {
                return CreateErrorResponse("用户名不能为空");
            }

            bool hasPermission = _permissionService.HasAnalysisPermission(username, analysisType);
            return Encoding.UTF8.GetBytes($"{{\"success\": true, \"hasPermission\": {hasPermission.ToString().ToLower()}}}");
        }

        private byte[] HandleShutdown()
        {
            LogHelper.LogInfo("收到关闭服务请求");
            Console.WriteLine("收到关闭服务请求，正在停止...");
            
            byte[] response = Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"服务正在关闭...\"}");
            
            ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(1000);
                Environment.Exit(0);
            });
            
            return response;
        }

        private byte[] HandleResetConfig()
        {
            LogHelper.LogInfo("收到重置配置请求");
            
            try
            {
                ConnectionStrings.ResetToDefault();
                
                string exeDir = AppContext.BaseDirectory;
                string configFile = System.IO.Path.Combine(exeDir, "server_config.dat");
                byte[] data = Encoding.UTF8.GetBytes("9094");
                string encrypted = Convert.ToBase64String(data);
                System.IO.File.WriteAllText(configFile, encrypted);
                
                LogHelper.LogInfo("配置已重置为默认值");
                return Encoding.UTF8.GetBytes("{\"success\": true, \"message\": \"配置已重置为默认值\"}");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "重置配置失败");
                return CreateErrorResponse("重置配置失败: " + ex.Message);
            }
        }

        private byte[] HandleSearchStoredProc(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string procName = ExtractValue(postData, "procName");

                LogHelper.LogInfo($"搜索存储过程: {procName}");

                try
                {
                    string result = _dailyAnalysisService.SearchStoredProcedures(procName);
                    return Encoding.UTF8.GetBytes(result);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "搜索存储过程失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleGetProcMetadata(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string procName = ExtractValue(postData, "procName");

                LogHelper.LogInfo($"获取存储过程元数据: {procName}");

                try
                {
                    string result = _dailyAnalysisService.GetStoredProcedureMetadata(procName);
                    return Encoding.UTF8.GetBytes(result);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "获取存储过程元数据失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleExecuteStoredProc(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string procName = ExtractValue(postData, "procName");
                
                LogHelper.LogInfo($"执行存储过程: {procName}");

                try
                {
                    string result = _dailyAnalysisService.ExecuteStoredProcedure(postData);
                    return Encoding.UTF8.GetBytes(result);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "执行存储过程失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleSaveProcConfig(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();

                LogHelper.LogInfo("保存统计配置到数据库");

                try
                {
                    string result = _statConfigService.SaveConfig(postData);
                    return Encoding.UTF8.GetBytes(result);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "保存统计配置失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleGetProcConfigs()
        {
            LogHelper.LogInfo("从数据库获取所有统计配置");

            try
            {
                string result = _statConfigService.GetConfigs();
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取统计配置失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetProcConfig(string url)
        {
            string configId = ExtractUrlParam(url, "id");
            
            LogHelper.LogInfo($"获取统计配置: {configId}");

            try
            {
                string result = _statConfigService.GetConfigById(configId);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取统计配置失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleDeleteProcConfig(string url, Stream inputStream)
        {
            string configId = ExtractUrlParam(url, "id");

            LogHelper.LogInfo($"删除统计配置: {configId}");

            try
            {
                string result = _statConfigService.DeleteConfig(configId);
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "删除统计配置失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetUpgradeMessages()
        {
            LogHelper.LogInfo("获取数据库升级消息");

            try
            {
                string result = _statConfigService.GetUpgradeMessages();
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取升级消息失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleSaveParamConfig(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();

                LogHelper.LogInfo("保存参数配置");

                try
                {
                    string configId = ExtractValue(postData, "configId");
                    string parametersJson = ExtractValue(postData, "parameters");

                    if (string.IsNullOrEmpty(configId))
                    {
                        return CreateErrorResponse("配置ID不能为空");
                    }

                    string result = _statConfigService.SaveParamConfig(configId, parametersJson);
                    return Encoding.UTF8.GetBytes(result);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "保存参数配置失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleGetSystemConfigs()
        {
            LogHelper.LogInfo("获取系统配置列表");

            try
            {
                var configs = _systemConfigService.GetAllConfigs();
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    success = true,
                    data = configs
                });
                return Encoding.UTF8.GetBytes(json);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取系统配置列表失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetMenuConfigList()
        {
            LogHelper.LogInfo("获取菜单配置列表");

            try
            {
                string result = _statConfigService.GetConfigs();
                return Encoding.UTF8.GetBytes(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取菜单配置列表失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleGetSystemConfig(string url)
        {
            string configKey = ExtractUrlParam(url, "key");
            LogHelper.LogInfo($"获取系统配置: {configKey}");

            try
            {
                var config = _systemConfigService.GetConfigByKey(configKey);
                if (config != null)
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        success = true,
                        data = config
                    });
                    return Encoding.UTF8.GetBytes(json);
                }
                else
                {
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"配置不存在\"}");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "获取系统配置失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleUpdateSystemConfig(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();

                LogHelper.LogInfo("更新系统配置");

                try
                {
                    string configKey = ExtractValue(postData, "configKey");
                    string configValue = ExtractValue(postData, "configValue");

                    if (string.IsNullOrEmpty(configKey))
                    {
                        return CreateErrorResponse("配置键不能为空");
                    }

                    bool success = _systemConfigService.UpdateConfig(configKey, configValue);
                    return Encoding.UTF8.GetBytes("{\"success\": " + success.ToString().ToLower() + "}");
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "更新系统配置失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleUpdateSystemConfigs(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();

                LogHelper.LogInfo("批量更新系统配置");

                try
                {
                    var configs = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(postData);
                    bool success = _systemConfigService.UpdateConfigs(configs);
                    return Encoding.UTF8.GetBytes("{\"success\": " + success.ToString().ToLower() + "}");
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "批量更新系统配置失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleAddSystemConfig(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();

                LogHelper.LogInfo("添加系统配置");

                try
                {
                    var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.SystemConfigItem>(postData);
                    bool success = _systemConfigService.AddConfig(config);
                    return Encoding.UTF8.GetBytes("{\"success\": " + success.ToString().ToLower() + "}");
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "添加系统配置失败");
                    return CreateErrorResponse(ex.Message);
                }
            }
        }

        private byte[] HandleDeleteSystemConfig(string url, Stream inputStream)
        {
            string configKey = ExtractUrlParam(url, "key");
            LogHelper.LogInfo($"删除系统配置: {configKey}");

            try
            {
                bool success = _systemConfigService.DeleteConfig(configKey);
                return Encoding.UTF8.GetBytes("{\"success\": " + success.ToString().ToLower() + "}");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "删除系统配置失败");
                return CreateErrorResponse(ex.Message);
            }
        }

        private byte[] HandleParseSqlProc(Stream inputStream)
        {
            using (StreamReader reader = new StreamReader(inputStream, Encoding.UTF8))
            {
                string postData = reader.ReadToEnd();
                string sql = ExtractValue(postData, "sql");

                LogHelper.LogInfo("解析SQL存储过程");

                try
                {
                    var result = ParseSqlProcedure(sql);
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Encoding.UTF8.GetBytes(json);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "解析SQL存储过程失败");
                    return Encoding.UTF8.GetBytes("{\"success\": false, \"error\": \"" + ex.Message.Replace("\"", "'") + "\"}");
                }
            }
        }

        private Dictionary<string, object> ParseSqlProcedure(string sql)
        {
            var result = new Dictionary<string, object>();
            result["success"] = true;
            result["procName"] = "";
            result["configId"] = "";
            result["configName"] = "";
            result["parameters"] = new List<Dictionary<string, object>>();

            var procMatch = System.Text.RegularExpressions.Regex.Match(sql, @"CREATE\s+PROC(?:EDURE)?\s+([a-zA-Z_][a-zA-Z0-9_]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (procMatch.Success)
            {
                string procName = procMatch.Groups[1].Value;
                result["procName"] = procName;
                result["configId"] = GenerateConfigId(procName);
                result["configName"] = GenerateDisplayName(procName);
            }

            var paramSection = System.Text.RegularExpressions.Regex.Match(sql, @"CREATE\s+PROC(?:EDURE)?\s+[a-zA-Z_][a-zA-Z0-9_]*\s*\(([\s\S]*?)\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (paramSection.Success)
            {
                string paramsText = paramSection.Groups[1].Value;
                List<string> paramsList = new List<string>();
                int depth = 0;
                string current = "";

                foreach (char c in paramsText)
                {
                    if (c == '(' || c == '[') depth++;
                    else if (c == ')' || c == ']') depth--;
                    else if (c == ',' && depth == 0)
                    {
                        paramsList.Add(current.Trim());
                        current = "";
                        continue;
                    }
                    current += c;
                }
                if (!string.IsNullOrWhiteSpace(current)) paramsList.Add(current.Trim());

                var parameters = (List<Dictionary<string, object>>)result["parameters"];
                foreach (string p in paramsList)
                {
                    var paramMatch = System.Text.RegularExpressions.Regex.Match(p, @"(@[a-zA-Z_][a-zA-Z0-9_]*)\s+([a-zA-Z]+(?:\([^)]*\))?)");
                    if (paramMatch.Success)
                    {
                        string name = paramMatch.Groups[1].Value;
                        string sqlType = paramMatch.Groups[2].Value.ToUpper();
                        string type = "varchar";

                        if (sqlType.Contains("DATE") || sqlType.Contains("TIME"))
                            type = "datetime";
                        else if (sqlType.Contains("INT") || sqlType.Contains("DECIMAL") || sqlType.Contains("FLOAT") || sqlType.Contains("NUMERIC"))
                            type = "int";

                        var defaultValueMatch = System.Text.RegularExpressions.Regex.Match(p, @"=\s*([^\s,)]+)");
                        string defaultValue = defaultValueMatch.Success ? defaultValueMatch.Groups[1].Value : "";

                        var param = new Dictionary<string, object>();
                        param["name"] = name;
                        param["displayName"] = GenerateDisplayName(name);
                        param["type"] = type;
                        param["defaultValue"] = defaultValue;
                        param["options"] = "";
                        param["isRequired"] = string.IsNullOrEmpty(defaultValue);
                        param["isMultiple"] = false;

                        parameters.Add(param);
                    }
                }
            }

            return result;
        }

        private string GenerateConfigId(string name)
        {
            string id = name.Replace("proc_", "").Replace("sp_", "").Replace("usp_", "");
            id = System.Text.RegularExpressions.Regex.Replace(id, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
            return id;
        }

        private string GenerateDisplayName(string name)
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>
            {
                {"StartDate", "开始日期"}, {"EndDate", "结束日期"}, {"StatDate", "统计日期"},
                {"SystemType", "系统类型"}, {"Department", "科室"}, {"Doctor", "医生"},
                {"Reporter", "报告医生"}, {"Reviewer", "审核医生"}, {"Technician", "技师"},
                {"Category", "检查类别"}, {"Type", "类型"}, {"Status", "状态"},
                {"Start", "开始"}, {"End", "结束"}, {"Date", "日期"}, {"Stat", "统计"},
                {"System", "系统"}, {"Department", "科室"}, {"Doctor", "医生"}
            };

            string cleanName = name.Replace("@", "");
            if (mappings.ContainsKey(cleanName))
                return mappings[cleanName];

            var words = System.Text.RegularExpressions.Regex.Matches(cleanName, @"[A-Z][a-z]*|[a-z]+|[0-9]+");
            string result = "";
            foreach (System.Text.RegularExpressions.Match word in words)
            {
                string w = word.Value;
                if (mappings.ContainsKey(w))
                    result += mappings[w];
                else
                    result += w;
            }
            return result;
        }
    }
}