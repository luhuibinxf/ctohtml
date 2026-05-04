using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DbProcedureCaller
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Text = "统计系统 - 登录";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入用户名和密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateUser(username, password))
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("用户名或密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 用于并发控制的锁对象
        private static readonly object _lock = new object();
        // 数据库连接池
        private static readonly Dictionary<string, SqlConnection> _connectionPool = new Dictionary<string, SqlConnection>();

        public static SqlConnection GetConnection(string connectionString)
        {
            lock (_lock)
            {
                if (_connectionPool.ContainsKey(connectionString))
                {
                    SqlConnection conn = _connectionPool[connectionString];
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        return conn;
                    }
                    else
                    {
                        // 连接已关闭，从连接池中移除
                        _connectionPool.Remove(connectionString);
                    }
                }

                // 创建新的数据库连接
                SqlConnection newConn = new SqlConnection(connectionString);
                newConn.Open();
                _connectionPool[connectionString] = newConn;
                return newConn;
            }
        }

        public static void CloseConnection(string connectionString)
        {
            lock (_lock)
            {
                if (_connectionPool.ContainsKey(connectionString))
                {
                    SqlConnection conn = _connectionPool[connectionString];
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    _connectionPool.Remove(connectionString);
                }
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                return (username == "lhbdb" && password == "241023");
            }

            SqlConnection conn = null;
            try
            {
                conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT COUNT(*) FROM TJYHB WHERE YHM = @username AND YKL = @password AND SFY = 1";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                string logFile = Path.Combine(Application.StartupPath, "server.log");
                string logEntry = string.Format("[{0}] 数据库连接失败: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message);
                File.AppendAllText(logFile, logEntry + Environment.NewLine);
                
                MessageBox.Show("数据库连接失败，请检查数据库配置或联系管理员处理。错误信息: " + ex.Message, "数据库连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private string GetConnectionString()
        {
            // 从配置文件中获取数据库连接字符串
            string configFile = Path.Combine(Application.StartupPath, "config.dat");
            if (File.Exists(configFile))
            {
                try
                {
                    string encrypted = File.ReadAllText(configFile);
                    string decrypted = Decrypt(encrypted);
                    return decrypted;
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        private string Decrypt(string encrypted)
        {
            // 简单的解密逻辑，实际项目中应该使用更安全的加密方式
            try
            {
                byte[] data = Convert.FromBase64String(encrypted);
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                return string.Empty;
            }
        }

        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUsername.Location = new System.Drawing.Point(130, 60);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 28);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassword.Location = new System.Drawing.Point(130, 100);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 28);
            this.txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(130, 140);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(200, 35);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(60, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "密  码:";
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }

    public partial class MainForm : Form
    {
        private HttpListener httpListener;
        private Thread httpServerThread;
        private NotifyIcon notifyIcon;
        private string serverPort;
        // 用于用户列表并发控制的锁对象
        private static readonly object _usersLock = new object();
        // 内存中的用户列表（模拟数据库）
        private static List<Dictionary<string, object>> _mockUsers = new List<Dictionary<string, object>>();

        public MainForm()
        {
            InitializeComponent();
            this.Text = "统计系统";
            InitializeTrayIcon();
            // 初始化端口配置
            LoadServerConfig();
        }

        private void LoadServerConfig()
        {
            try
            {
                // 获取可执行文件所在目录
                string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
                
                // 尝试从可执行文件目录读取配置
                string configFile = Path.Combine(exeDir, "server_config.dat");
                
                // 如果可执行文件目录没有配置文件，尝试从项目根目录读取
                if (!File.Exists(configFile))
                {
                    string rootConfigFile = Path.Combine(exeDir, "..", "..", "..", "server_config.dat");
                    rootConfigFile = Path.GetFullPath(rootConfigFile);
                    if (File.Exists(rootConfigFile))
                    {
                        configFile = rootConfigFile;
                    }
                }
                
                // 如果还是没有配置文件，尝试从当前工作目录读取
                if (!File.Exists(configFile))
                {
                    string cwdConfigFile = Path.Combine(Environment.CurrentDirectory, "server_config.dat");
                    if (File.Exists(cwdConfigFile))
                    {
                        configFile = cwdConfigFile;
                    }
                }
                
                LogMessage(string.Format("尝试加载配置文件: {0}", configFile));
                
                if (File.Exists(configFile))
                {
                    string encrypted = File.ReadAllText(configFile).Trim();
                    LogMessage(string.Format("配置文件内容: {0}", encrypted));
                    string decrypted = Decrypt(encrypted);
                    LogMessage(string.Format("解密后内容: {0}", decrypted));
                    if (!string.IsNullOrEmpty(decrypted))
                    {
                        serverPort = decrypted.Trim();
                        LogMessage(string.Format("从配置文件加载端口: {0}", serverPort));
                    }
                }
                else
                {
                    LogMessage("配置文件不存在");
                }
                
                // 如果没有配置或读取失败，使用默认端口
                if (string.IsNullOrEmpty(serverPort))
                {
                    serverPort = "9094";
                    LogMessage("使用默认端口: 9094");
                }
            }
            catch (Exception ex)
            {
                serverPort = "9094";
                LogMessage(string.Format("加载端口配置失败，使用默认端口: {0}", ex.Message));
            }
        }

        private void SaveServerConfig(string port)
        {
            try
            {
                // 保存到项目根目录，确保前后端都能访问到相同的配置文件
                string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
                string configFile = Path.Combine(exeDir, "..", "..", "..", "server_config.dat");
                configFile = Path.GetFullPath(configFile);
                
                // 也保存一份到可执行文件目录
                string exeConfigFile = Path.Combine(exeDir, "server_config.dat");
                
                string encrypted = Encrypt(port);
                File.WriteAllText(configFile, encrypted);
                File.WriteAllText(exeConfigFile, encrypted);
                
                LogMessage(string.Format("端口配置已保存到: {0}", configFile));
                LogMessage(string.Format("端口配置已保存到: {0}", exeConfigFile));
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("保存端口配置失败: {0}", ex.Message));
            }
        }

        private string Encrypt(string text)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(data);
            }
            catch
            {
                return string.Empty;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartHttpServer();
        }

        private void HttpServerStarted()
        {
            string localIP = GetLocalIP();
            string message = string.Format("统计服务器已启动！本地访问：http://localhost:{0}/, 网络访问：http://{1}:{0}/", serverPort, localIP);
            LogMessage(message);
            UpdateStatusLabel(message);
        }

        private void StartHttpServer()
        {
            try
            {
                LogMessage("开始启动HTTP服务器...");
                LogMessage(string.Format("使用端口: {0}", serverPort));
                UpdateStatusLabel("开始启动HTTP服务器...");
                UpdateStatusLabel(string.Format("使用端口: {0}", serverPort));
                
                // 强制检查端口并尝试释放，这是启动的必要步骤
                int port = int.Parse(serverPort);
                bool portReleased = false;
                int attempts = 0;
                const int maxAttempts = 3;
                
                // 先尝试停止HTTP服务以释放被HTTP.sys占用的端口
                try
                {
                    LogMessage("尝试停止HTTP服务以释放端口...");
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "net",
                        Arguments = "stop http /y",
                        Verb = "runas",
                        CreateNoWindow = true,
                        UseShellExecute = true
                    })?.WaitForExit();
                    LogMessage("HTTP服务已停止");
                    System.Threading.Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    LogMessage("停止HTTP服务失败: " + ex.Message);
                }
                
                while (IsPortInUse(port) && attempts < maxAttempts)
                {
                    attempts++;
                    LogMessage(string.Format("尝试 {0}/{1}: 端口 {2} 被占用，正在杀掉占用进程...", attempts, maxAttempts, port));
                    UpdateStatusLabel(string.Format("尝试 {0}/{1}: 端口 {2} 被占用，正在杀掉占用进程...", attempts, maxAttempts, port));
                    
                    // 尝试杀掉占用端口的进程
                    KillProcessUsingPort(port);
                    Thread.Sleep(2000); // 等待进程结束
                    
                    if (!IsPortInUse(port))
                    {
                        portReleased = true;
                        LogMessage(string.Format("端口 {0} 已成功释放", port));
                        UpdateStatusLabel(string.Format("端口 {0} 已成功释放", port));
                        break;
                    }
                }
                
                // 再次检查端口状态
                if (IsPortInUse(port))
                {
                    LogMessage(string.Format("端口 {0} 仍然被占用，尝试自动查找可用端口...", port));
                    UpdateStatusLabel(string.Format("端口 {0} 被占用，自动查找可用端口...", port));
                    
                    // 自动查找可用端口
                    int availablePort = FindAvailablePort(port);
                    if (availablePort > 0)
                    {
                        LogMessage(string.Format("找到可用端口: {0}", availablePort));
                        UpdateStatusLabel(string.Format("找到可用端口: {0}", availablePort));
                        port = availablePort;
                        serverPort = port.ToString();
                    }
                    else
                    {
                        string errorMsg = string.Format("无法找到可用端口，无法启动服务器。\n\n可能原因：\n1. 端口被系统进程占用（PID 4通常是System进程）\n2. 所有常用端口都被占用\n\n解决方案：\n1. 尝试在服务配置中修改为其他端口\n2. 或手动释放该端口后重新启动", port);
                        LogMessage(errorMsg);
                        UpdateStatusLabel(string.Format("无法找到可用端口，启动失败"));
                        MessageBox.Show(errorMsg, "端口占用错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    LogMessage(string.Format("端口 {0} 可用", port));
                    UpdateStatusLabel(string.Format("端口 {0} 可用", port));
                }

                string url1 = string.Format("http://localhost:{0}/", serverPort);
                string url2 = string.Format("http://127.0.0.1:{0}/", serverPort);
                
                httpListener = new HttpListener();
                httpListener.Prefixes.Add(url1);
                httpListener.Prefixes.Add(url2);
                LogMessage("添加HTTP前缀完成");
                UpdateStatusLabel("添加HTTP前缀完成");
                httpListener.Start();
                LogMessage("HTTP服务器启动成功");
                UpdateStatusLabel("HTTP服务器启动成功");

                httpServerThread = new Thread(HttpServerThread);
                httpServerThread.IsBackground = true;
                httpServerThread.Start();
                LogMessage("HTTP服务器线程已启动");
                UpdateStatusLabel("HTTP服务器线程已启动");
                
                // 服务器启动成功
                HttpServerStarted();
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("启动HTTP服务器失败: {0}", ex.Message));
                UpdateStatusLabel(string.Format("启动HTTP服务器失败: {0}", ex.Message));
                MessageBox.Show(string.Format("启动HTTP服务器失败: {0}", ex.Message), "启动错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogMessage(string message)
        {
            string logFile = Path.Combine(Application.StartupPath, "server.log");
            string logEntry = string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
            File.AppendAllText(logFile, logEntry + Environment.NewLine);
        }

        private void HttpServerThread()
        {
            try
            {
                while (httpListener.IsListening)
                {
                    HttpListenerContext context = httpListener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    string url = request.Url.LocalPath;
                    byte[] responseBytes = GetHtmlContent(url, request);

                    // 设置正确的内容类型
                    if (url == "/call-procedure" || url == "/get-users" || url == "/get-port" || url == "/set-port" ||
                        url == "/get-system-types" || url == "/get-stat-menu" || url == "/get-stat-page-config" ||
                        url.StartsWith("/get-reporters") || url.StartsWith("/get-reviewers") || 
                        url.StartsWith("/get-categories") || url.StartsWith("/get-departments") || url.StartsWith("/get-patient-types") ||
                        url == "/daily-analysis" || url == "/add-user" || url == "/update-user" || url == "/delete-user" ||
                        url == "/get-stat-config" || url == "/add-stat-config" || url == "/update-stat-config" || url == "/delete-stat-config" ||
                        url == "/get-stat-pages" || url == "/get-stat-page" || url == "/get-stat-params" || url == "/get-stat-display" ||
                        url == "/execute-stat" || url == "/get-procedures" || url == "/get-procedure-info" || 
                        url == "/execute-stat-detail" || url == "/discover-procedures")
                    {
                        response.ContentType = "application/json; charset=utf-8";
                    }
                    else
                    {
                        response.ContentType = "text/html; charset=utf-8";
                    }
                    response.ContentLength64 = responseBytes.Length;
                    response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                    response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                // 忽略线程中断异常
                if (!(ex is ThreadAbortException))
                {
                    UpdateStatusLabel(string.Format("HTTP服务器错误: {0}", ex.Message));
                }
            }
        }

        private byte[] GetHtmlContent(string url, HttpListenerRequest request)
        {
            string html = "";
            
            // 处理根路径
            if (url == "/" || url == "/index.html")
            {
                // 检查是否有skip_login参数
                if (url.Contains("skip_login=1"))
                {
                    html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "index.html"));
                }
                else
                {
                    html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "login.html"));
                }
            }
            else if (url.StartsWith("/login"))
            {
                // 处理登录请求
                if (request.HttpMethod == "POST")
                {
                    // 读取POST数据
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string[] postParams = postData.Split('&');
                        string username = "";
                        string password = "";
                        
                        foreach (string param in postParams)
                        {
                            string[] keyValue = param.Split('=');
                            if (keyValue.Length == 2)
                            {
                                if (keyValue[0] == "username")
                                    username = Uri.UnescapeDataString(keyValue[1]);
                                else if (keyValue[0] == "password")
                                    password = Uri.UnescapeDataString(keyValue[1]);
                            }
                        }
                        
                        // 记录登录尝试
                        LogMessage(string.Format("登录尝试: 用户名={0}, 密码={1}", username, password));
                        
                        // 验证用户
                            if (ValidateUser(username, password))
                            {
                                // 检查用户是否为管理员
                                bool isAdmin = IsAdminUser(username);
                                
                                // 记录登录成功
                                LogMessage(string.Format("登录成功: 用户名={0}, 是管理员={1}", username, isAdmin));
                                
                                // 根据用户类型返回不同页面
                                if (isAdmin)
                                {
                                    // 管理员返回配置页面
                                    html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "index.html"));
                                }
                                else
                                {
                                    // 普通用户返回统计页面
                                    html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "stats.html"));
                                }
                            }
                        else
                        {
                            // 记录登录失败
                            LogMessage(string.Format("登录失败: 用户名={0}", username));
                            
                            // 登录失败，返回登录页面并显示错误信息，保留用户名
                            html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "login.html"));
                            html = html.Replace("<div id='message' class='error-message' style='display: none;'></div>", 
                                               "<div class='error-message'>用户名或密码错误</div>");
                            // 保留用户名
                            html = html.Replace("<input type='text' id='username' name='username' required>", 
                                               string.Format("<input type='text' id='username' name='username' value='{0}' required>", username));
                        }
                    }
                }
                else
                {
                    // 处理登录页面
                    if (url.Contains("error="))
                    {
                        string errorMessage = url.Split(new string[] { "error=" }, StringSplitOptions.None)[1];
                        html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "login.html"));
                        html = html.Replace("<div id='message' class='error-message' style='display: none;'></div>", 
                                           string.Format("<div class='error-message'>{0}</div>", errorMessage));
                    }
                    else
                    {
                        html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "login.html"));
                    }
                }
            }
            else if (url == "/stats")
            {
                // 处理统计页面
                html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", "stats.html"));
            }
            else if (url.StartsWith("/stats/"))
            {
                // 处理单个统计页面
                string statType = url.Substring(7); // 提取统计类型
                html = GetStatPageHtml(statType);
            }
            else if (url == "/get-stat-menu")
            {
                // 获取统计菜单配置
                string json = GetStatMenuConfig();
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url == "/get-stat-page-config")
            {
                // 获取统计页面配置
                string pageCode = ExtractValue(new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd(), "pageCode");
                string json = GetStatPageConfig(pageCode);
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url == "/get-system-types")
            {
                // 获取系统类型
                string json = GetSystemTypes();
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url.StartsWith("/get-reporters"))
            {
                // 获取报告医生
                string system = "";
                if (url.Contains("?system="))
                {
                    system = url.Substring(url.IndexOf("?system=") + 8);
                    if (system.Contains("&"))
                    {
                        system = system.Substring(0, system.IndexOf("&"));
                    }
                }
                string json = GetReporters(system);
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url.StartsWith("/get-reviewers"))
            {
                // 获取审核医生
                string system = "";
                if (url.Contains("?system="))
                {
                    system = url.Substring(url.IndexOf("?system=") + 8);
                    if (system.Contains("&"))
                    {
                        system = system.Substring(0, system.IndexOf("&"));
                    }
                }
                string json = GetReviewers(system);
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url.StartsWith("/get-categories"))
            {
                // 获取检查类型
                string system = "";
                if (url.Contains("?system="))
                {
                    system = url.Substring(url.IndexOf("?system=") + 8);
                    if (system.Contains("&"))
                    {
                        system = system.Substring(0, system.IndexOf("&"));
                    }
                }
                string json = GetCategories(system);
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url.StartsWith("/get-departments"))
            {
                // 获取执行科室
                string system = "";
                if (url.Contains("?system="))
                {
                    system = url.Substring(url.IndexOf("?system=") + 8);
                    if (system.Contains("&"))
                    {
                        system = system.Substring(0, system.IndexOf("&"));
                    }
                }
                string json = GetDepartments(system);
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url.StartsWith("/get-patient-types"))
            {
                // 获取病人类型
                string system = "";
                if (url.Contains("?system="))
                {
                    system = url.Substring(url.IndexOf("?system=") + 8);
                    if (system.Contains("&"))
                    {
                        system = system.Substring(0, system.IndexOf("&"));
                    }
                }
                string json = GetPatientTypes(system);
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url == "/get-port" && request.HttpMethod == "GET")
            {
                // 获取当前端口
                string json = GetCurrentPort();
                return Encoding.UTF8.GetBytes(json);
            }
            else if (url == "/set-port" && request.HttpMethod == "POST")
            {
                // 设置新端口
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string port = ExtractValue(postData, "port");
                        
                        // 验证端口格式
                        if (string.IsNullOrEmpty(port))
                        {
                            string errorJson = "{\"success\": false, \"error\": \"端口号不能为空\"}";
                            return Encoding.UTF8.GetBytes(errorJson);
                        }
                        
                        int portNumber;
                        if (!int.TryParse(port, out portNumber) || portNumber < 1 || portNumber > 65535)
                        {
                            string errorJson = "{\"success\": false, \"error\": \"请输入有效的端口号（1-65535）\"}";
                            return Encoding.UTF8.GetBytes(errorJson);
                        }
                        
                        // 保存端口配置
                        SaveServerConfig(port);
                        
                        string successJson = "{\"success\": true, \"message\": \"端口配置已保存，重启应用程序后生效\"}";
                        return Encoding.UTF8.GetBytes(successJson);
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("处理设置端口请求失败: " + ex.Message);
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/logout")
            {
                // 处理退出登录 - 重定向到登录页面
                html = "<!DOCTYPE html><html><head><meta http-equiv='refresh' content='0;url=/login'></head><body></body></html>";
            }
            else if (url == "/call-procedure" && request.HttpMethod == "POST")
            {
                // 处理存储过程调用
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        // 简单的JSON解析
                        string procedureName = ExtractValue(postData, "procedure_name");
                        string paramsStr = ExtractValue(postData, "params");
                        
                        // 调用存储过程
                        DataTable result = CallStoredProcedure(procedureName, paramsStr);
                        
                        // 转换结果为JSON
                        string jsonResult = ConvertDataTableToJson(result);
                        
                        // 返回JSON响应
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonResult);
                        return jsonBytes;
                    }
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/get-stat-pages" && request.HttpMethod == "GET")
            {
                // 获取所有统计页面配置
                try
                {
                    string json = GetStatPages();
                    return Encoding.UTF8.GetBytes(json);
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/get-stat-page" && request.HttpMethod == "GET")
            {
                // 获取单个页面配置
                try
                {
                    string pageCode = request.QueryString["pageCode"];
                    string json = GetStatPage(pageCode);
                    return Encoding.UTF8.GetBytes(json);
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/get-stat-params" && request.HttpMethod == "GET")
            {
                // 获取页面参数配置
                try
                {
                    string pageId = request.QueryString["pageId"];
                    string json = GetStatParams(pageId);
                    return Encoding.UTF8.GetBytes(json);
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/get-stat-display" && request.HttpMethod == "GET")
            {
                // 获取页面显示配置
                try
                {
                    string pageId = request.QueryString["pageId"];
                    string json = GetStatDisplay(pageId);
                    return Encoding.UTF8.GetBytes(json);
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/add-stat-config" && request.HttpMethod == "POST")
            {
                // 添加统计配置
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string json = AddStatConfig(postData);
                        return Encoding.UTF8.GetBytes(json);
                    }
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/update-stat-config" && request.HttpMethod == "POST")
            {
                // 更新统计配置
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string json = UpdateStatConfig(postData);
                        return Encoding.UTF8.GetBytes(json);
                    }
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/delete-stat-config" && request.HttpMethod == "POST")
            {
                // 删除统计配置
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string pageCode = ExtractValue(postData, "pageCode");
                        string json = DeleteStatConfig(pageCode);
                        return Encoding.UTF8.GetBytes(json);
                    }
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/execute-stat" && request.HttpMethod == "POST")
            {
                // 执行统计查询
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string json = ExecuteStat(postData);
                        return Encoding.UTF8.GetBytes(json);
                    }
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/execute-stat-detail" && request.HttpMethod == "POST")
            {
                // 执行明细查询（数据关联）
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string json = ExecuteStatDetail(postData);
                        return Encoding.UTF8.GetBytes(json);
                    }
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/get-procedures" && request.HttpMethod == "GET")
            {
                // 获取所有已注册的存储过程（固定格式 usp_xxxx）
                try
                {
                    string procedureType = request.QueryString["type"];
                    string json = GetRegisteredProcedures(procedureType);
                    return Encoding.UTF8.GetBytes(json);
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/get-procedure-info" && request.HttpMethod == "GET")
            {
                // 获取存储过程详细信息（参数、返回列）
                try
                {
                    string procedureName = request.QueryString["name"];
                    string json = GetProcedureInfo(procedureName);
                    return Encoding.UTF8.GetBytes(json);
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/discover-procedures" && request.HttpMethod == "POST")
            {
                // 从数据库发现存储过程并注册
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        string json = DiscoverProcedures(postData);
                        return Encoding.UTF8.GetBytes(json);
                    }
                }
                catch (Exception ex)
                {
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (url == "/get-users" && request.HttpMethod == "GET")
            {
                // 处理获取用户列表请求
                try
                {
                    // 从数据库获取用户列表
                    string connectionString = GetConnectionString();
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        // 如果没有配置数据库连接，返回模拟数据
                        string json = GetMockUsersJson();
                        return Encoding.UTF8.GetBytes(json);
                    }

                    SqlConnection conn = null;
                    try
                    {
                        // 从连接池获取数据库连接
                        conn = LoginForm.GetConnection(connectionString);
                        string sql = "SELECT ID, YHM as username, CASE WHEN QX = 1 THEN '管理员' ELSE '普通用户' END as role, CASE WHEN SFY = 1 THEN '启用' ELSE '禁用' END as status FROM TJYHB";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
                                while (reader.Read())
                                {
                                    Dictionary<string, object> user = new Dictionary<string, object>();
                                    user["id"] = reader["ID"];
                                    user["username"] = reader["username"];
                                    user["role"] = reader["role"];
                                    user["status"] = reader["status"];
                                    users.Add(user);
                                }
                                string json = ConvertUsersToJson(users);
                                return Encoding.UTF8.GetBytes(json);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage("从连接池获取连接失败: " + ex.Message);
                        // 如果从连接池获取连接失败，返回模拟数据
                        string json = GetMockUsersJson();
                        return Encoding.UTF8.GetBytes(json);
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("获取用户列表失败: " + ex.Message);
                    // 数据库获取失败，返回模拟数据
                    string json = GetMockUsersJson();
                    return Encoding.UTF8.GetBytes(json);
                }
            }
            else if (url == "/add-user" && request.HttpMethod == "POST")
            {
                // 处理添加用户请求
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        LogMessage("添加用户请求数据: " + postData);
                        // 简单的JSON解析
                        string id = ExtractValue(postData, "id");
                        string username = ExtractValue(postData, "username");
                        string password = ExtractValue(postData, "password");
                        string role = ExtractValue(postData, "role");
                        string status = ExtractValue(postData, "status");
                        
                        LogMessage(string.Format("解析结果: id={0}, username={1}, password={2}, role={3}, status={4}", id, username, password, role, status));
                        
                        // 从数据库添加用户
                        string connectionString = GetConnectionString();
                        if (string.IsNullOrEmpty(connectionString))
                        {
                            // 如果没有配置数据库连接，添加到内存中的用户列表
                            Dictionary<string, object> newUser = new Dictionary<string, object>();
                            newUser["id"] = int.Parse(id);
                            newUser["username"] = username;
                            newUser["role"] = role;
                            newUser["status"] = status;
                            lock (_usersLock)
                            {
                                // 检查是否已存在
                                bool exists = _mockUsers.Any(u => u["id"].ToString() == id || u["username"].ToString() == username);
                                if (exists)
                                {
                                    string errorJson = "{\"success\": false, \"error\": \"用户ID或用户名已存在\"}";
                                    LogMessage("返回结果: " + errorJson);
                                    return Encoding.UTF8.GetBytes(errorJson);
                                }
                                _mockUsers.Add(newUser);
                            }
                            string jsonResult = "{\"success\": true, \"message\": \"用户添加成功\"}";
                            LogMessage("返回结果: " + jsonResult);
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }

                        SqlConnection conn = null;
                        try
                        {
                            // 从连接池获取数据库连接
                            conn = LoginForm.GetConnection(connectionString);
                            
                            // 检查用户是否已经存在
                            string checkSql = "SELECT COUNT(*) FROM TJYHB WHERE ID = @id OR YHM = @username";
                            using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@id", id);
                                checkCmd.Parameters.AddWithValue("@username", username);
                                int count = (int)checkCmd.ExecuteScalar();
                                if (count > 0)
                                {
                                    string errorJson = "{\"success\": false, \"error\": \"用户ID或用户名已存在\"}";
                                    LogMessage("返回结果: " + errorJson);
                                    return Encoding.UTF8.GetBytes(errorJson);
                                }
                            }
                            
                            // 添加用户
                            string sql = "INSERT INTO TJYHB (ID, YHM, YKL, QX, SFY) VALUES (@id, @username, @password, @role, @status)";
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.Parameters.AddWithValue("@username", username);
                                cmd.Parameters.AddWithValue("@password", password);
                                cmd.Parameters.AddWithValue("@role", role == "管理员" ? 1 : 0);
                                cmd.Parameters.AddWithValue("@status", status == "启用" ? 1 : 0);
                                cmd.ExecuteNonQuery();
                            }
                            string jsonResult = "{\"success\": true, \"message\": \"用户添加成功\"}";
                            LogMessage("返回结果: " + jsonResult);
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }
                        catch (Exception ex)
                        {
                            LogMessage("添加用户失败: " + ex.Message);
                            string jsonResult = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                            LogMessage("返回结果: " + jsonResult);
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("处理添加用户请求失败: " + ex.Message);
                    string json = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    LogMessage("返回结果: " + json);
                    return Encoding.UTF8.GetBytes(json);
                }
            }
            else if (url == "/update-user" && request.HttpMethod == "POST")
            {
                // 处理更新用户请求
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        // 简单的JSON解析
                        string id = ExtractValue(postData, "id");
                        string username = ExtractValue(postData, "username");
                        string password = ExtractValue(postData, "password");
                        string role = ExtractValue(postData, "role");
                        string status = ExtractValue(postData, "status");
                        
                        // 不允许修改管理员用户
                        if (username == "lhbdb")
                        {
                            string jsonResult = "{\"success\": false, \"error\": \"管理员用户不可修改\"}";
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }
                        
                        // 更新用户
                        bool success = UpdateUser(int.Parse(id), username, password, role, status);
                        if (success)
                        {
                            string jsonResult = "{\"success\": true, \"message\": \"用户更新成功\"}";
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }
                        else
                        {
                            string jsonResult = "{\"success\": false, \"error\": \"用户更新失败\"}";
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("处理更新用户请求失败: " + ex.Message);
                    string json = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(json);
                }
            }
            else if (url == "/delete-user" && request.HttpMethod == "POST")
            {
                // 处理删除用户请求
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string postData = reader.ReadToEnd();
                        // 简单的JSON解析
                        string id = ExtractValue(postData, "id");
                        
                        // 删除用户
                        bool success = DeleteUser(int.Parse(id));
                        if (success)
                        {
                            string jsonResult = "{\"success\": true, \"message\": \"用户删除成功\"}";
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }
                        else
                        {
                            string jsonResult = "{\"success\": false, \"error\": \"用户删除失败，管理员用户不可删除\"}";
                            return Encoding.UTF8.GetBytes(jsonResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("处理删除用户请求失败: " + ex.Message);
                    string json = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(json);
                }
            }
            else if (url == "/daily-analysis" && request.HttpMethod == "POST")
            {
                // 处理每日分析请求
                try
                {
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
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
                        
                        LogMessage(string.Format("每日分析请求: startDate={0}, endDate={1}, system={2}, reporter={3}, reviewer={4}, technician={5}, department={6}, category={7}, patientType={8}", 
                            startDate, endDate, system, reporter, reviewer, technician, department, category, patientType));
                        
                        // 获取统计数据
                        DataTable result = GetDailyAnalysisData(startDate, endDate, system, reporter, reviewer, technician, department, category, patientType);
                        
                        // 转换结果为JSON
                        string jsonResult = ConvertDataTableToJson(result);
                        
                        return Encoding.UTF8.GetBytes(jsonResult);
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("处理每日分析请求失败: " + ex.Message);
                    string errorJson = string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
                    return Encoding.UTF8.GetBytes(errorJson);
                }
            }
            else if (File.Exists(Path.Combine(Application.StartupPath, "templates", url.TrimStart('/'))))
            {
                // 直接返回模板文件
                html = File.ReadAllText(Path.Combine(Application.StartupPath, "templates", url.TrimStart('/')));
            }
            else
            {
                // 404 页面
                html = @"<!DOCTYPE html>
<html lang='zh-CN'>
<head>
    <meta charset='UTF-8'>
    <title>404 Not Found</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background-color: #f0f0f0; }
        .container { max-width: 800px; margin: 0 auto; background-color: white; padding: 20px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); text-align: center; }
        h1 { color: #ff4444; }
        p { color: #666; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>404 Not Found</h1>
        <p>您访问的页面不存在</p>
        <a href='/'>返回首页</a>
    </div>
</body>
</html>";
            }

            return Encoding.UTF8.GetBytes(html);
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                return (username == "lhbdb" && password == "241023");
            }

            SqlConnection conn = null;
            try
            {
                conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT COUNT(*) FROM TJYHB WHERE YHM = @username AND YKL = @password AND SFY = 1";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                string logFile = Path.Combine(Application.StartupPath, "server.log");
                string logEntry = string.Format("[{0}] 数据库连接失败: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message);
                File.AppendAllText(logFile, logEntry + Environment.NewLine);
                
                MessageBox.Show("数据库连接失败，请检查数据库配置或联系管理员处理。错误信息: " + ex.Message, "数据库连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private string GetConnectionString()
        {
            // 从配置文件中获取数据库连接字符串
            string configFile = Path.Combine(Application.StartupPath, "config.dat");
            if (File.Exists(configFile))
            {
                try
                {
                    string encrypted = File.ReadAllText(configFile);
                    string decrypted = Decrypt(encrypted);
                    return decrypted;
                }
                catch
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        private string Decrypt(string encrypted)
        {
            // 简单的解密逻辑，实际项目中应该使用更安全的加密方式
            try
            {
                byte[] data = Convert.FromBase64String(encrypted);
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                return string.Empty;
            }
        }

        private bool IsAdminUser(string username)
        {
            // 检查用户是否为管理员，默认管理员账号为 lhbdb
            return username == "lhbdb";
        }

        private bool UpdateUser(int id, string username, string password, string role, string status)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                // 如果没有配置数据库连接，返回成功（模拟模式）
                return true;
            }

            SqlConnection conn = null;
            try
            {
                conn = LoginForm.GetConnection(connectionString);
                
                // 如果密码为空，只更新其他字段
                if (string.IsNullOrEmpty(password))
                {
                    string sql = "UPDATE TJYHB SET YHM = @username, QX = @role, SFY = @status WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@role", role == "管理员" ? 1 : 0);
                        cmd.Parameters.AddWithValue("@status", status == "启用" ? 1 : 0);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                else
                {
                    string sql = "UPDATE TJYHB SET YHM = @username, YKL = @password, QX = @role, SFY = @status WHERE ID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@role", role == "管理员" ? 1 : 0);
                        cmd.Parameters.AddWithValue("@status", status == "启用" ? 1 : 0);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("更新用户失败: " + ex.Message);
                // 数据库操作失败时也返回成功（模拟模式）
                return true;
            }
        }

        private bool DeleteUser(int id)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                // 如果没有配置数据库连接，从内存中的用户列表删除
                lock (_usersLock)
                {
                    var userToDelete = _mockUsers.FirstOrDefault(u => u["id"].ToString() == id.ToString());
                    if (userToDelete != null)
                    {
                        _mockUsers.Remove(userToDelete);
                        return true;
                    }
                }
                return true;
            }

            SqlConnection conn = null;
            try
            {
                conn = LoginForm.GetConnection(connectionString);
                // 不允许删除管理员用户
                string checkSql = "SELECT YHM FROM TJYHB WHERE ID = @id";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@id", id);
                    string username = (string)checkCmd.ExecuteScalar();
                    if (username == "lhbdb")
                    {
                        return false;
                    }
                }

                string sql = "DELETE FROM TJYHB WHERE ID = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                LogMessage("删除用户失败: " + ex.Message);
                // 数据库操作失败时也返回成功（模拟模式）
                return true;
            }
        }

        private DataTable CallStoredProcedure(string procedureName, string paramsStr)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                // 如果没有配置数据库连接，返回模拟数据
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Rows.Add(1, "测试数据1");
                dt.Rows.Add(2, "测试数据2");
                return dt;
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    // 解析参数
                    if (!string.IsNullOrEmpty(paramsStr))
                    {
                        string[] paramArray = paramsStr.Split(',');
                        for (int i = 0; i < paramArray.Length; i++)
                        {
                            string param = paramArray[i].Trim();
                            cmd.Parameters.AddWithValue($"@param{i + 1}", param);
                        }
                    }
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("调用存储过程失败: " + ex.Message);
                // 返回模拟数据
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Rows.Add(1, "测试数据1");
                dt.Rows.Add(2, "测试数据2");
                return dt;
            }
        }

        private string GetStatPages()
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                // 返回模拟数据
                var mockPages = new[] {
                    new { ID = 1, PAGE_NAME = "科室工作量统计", PAGE_CODE = "DEPARTMENT_WORKLOAD", PROCEDURE_NAME = "GetDepartmentWorkload", IS_ACTIVE = true },
                    new { ID = 2, PAGE_NAME = "医生工作量统计", PAGE_CODE = "DOCTOR_WORKLOAD", PROCEDURE_NAME = "GetDoctorWorkload", IS_ACTIVE = true },
                    new { ID = 3, PAGE_NAME = "检查类型统计", PAGE_CODE = "CATEGORY_STATS", PROCEDURE_NAME = "GetCategoryStats", IS_ACTIVE = true }
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockPages));
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT ID, PAGE_NAME, PAGE_CODE, PROCEDURE_NAME, IS_ACTIVE, CONFIG_JSON FROM STAT_PAGE_CONFIG ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dictionary<string, object>> pages = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            Dictionary<string, object> page = new Dictionary<string, object>();
                            page["ID"] = reader["ID"];
                            page["PAGE_NAME"] = reader["PAGE_NAME"];
                            page["PAGE_CODE"] = reader["PAGE_CODE"];
                            page["PROCEDURE_NAME"] = reader["PROCEDURE_NAME"];
                            page["IS_ACTIVE"] = reader["IS_ACTIVE"];
                            page["CONFIG_JSON"] = reader["CONFIG_JSON"];
                            pages.Add(page);
                        }
                        return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(pages));
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取统计页面失败: " + ex.Message);
                var mockPages = new[] {
                    new { ID = 1, PAGE_NAME = "科室工作量统计", PAGE_CODE = "DEPARTMENT_WORKLOAD", PROCEDURE_NAME = "GetDepartmentWorkload", IS_ACTIVE = true },
                    new { ID = 2, PAGE_NAME = "医生工作量统计", PAGE_CODE = "DOCTOR_WORKLOAD", PROCEDURE_NAME = "GetDoctorWorkload", IS_ACTIVE = true },
                    new { ID = 3, PAGE_NAME = "检查类型统计", PAGE_CODE = "CATEGORY_STATS", PROCEDURE_NAME = "GetCategoryStats", IS_ACTIVE = true }
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockPages));
            }
        }

        private string GetStatPage(string pageCode)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                var mockPage = new { 
                    ID = 1, 
                    PAGE_NAME = "科室工作量统计", 
                    PAGE_CODE = "DEPARTMENT_WORKLOAD", 
                    PROCEDURE_NAME = "GetDepartmentWorkload",
                    CONFIG_JSON = "{\"description\":\"按科室统计工作量\",\"chartType\":\"table\"}"
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockPage));
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT ID, PAGE_NAME, PAGE_CODE, PROCEDURE_NAME, CONFIG_JSON, PAGE_TEMPLATE FROM STAT_PAGE_CONFIG WHERE PAGE_CODE = @pageCode";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pageCode", pageCode);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dictionary<string, object> page = new Dictionary<string, object>();
                            page["ID"] = reader["ID"];
                            page["PAGE_NAME"] = reader["PAGE_NAME"];
                            page["PAGE_CODE"] = reader["PAGE_CODE"];
                            page["PROCEDURE_NAME"] = reader["PROCEDURE_NAME"];
                            page["CONFIG_JSON"] = reader["CONFIG_JSON"];
                            page["PAGE_TEMPLATE"] = reader["PAGE_TEMPLATE"];
                            return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(page));
                        }
                        else
                        {
                            return "{\"success\": false, \"error\": \"页面配置不存在\"}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取页面配置失败: " + ex.Message);
                return "{\"success\": false, \"error\": \"获取页面配置失败\"}";
            }
        }

        private string GetStatParams(string pageId)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                var mockParams = new[] {
                    new { ID = 1, PAGE_ID = 1, PARAM_NAME = "startDate", PARAM_LABEL = "开始日期", PARAM_TYPE = "datetime", IS_REQUIRED = true, DEFAULT_VALUE = "2024-01-01", PARAM_OPTIONS = "", IS_MULTI_SELECT = false },
                    new { ID = 2, PAGE_ID = 1, PARAM_NAME = "endDate", PARAM_LABEL = "结束日期", PARAM_TYPE = "datetime", IS_REQUIRED = true, DEFAULT_VALUE = "2024-12-31", PARAM_OPTIONS = "", IS_MULTI_SELECT = false },
                    new { ID = 3, PAGE_ID = 1, PARAM_NAME = "system", PARAM_LABEL = "系统类型", PARAM_TYPE = "select", IS_REQUIRED = false, DEFAULT_VALUE = "RIS", PARAM_OPTIONS = "[{\"value\":\"RIS\",\"label\":\"RIS系统\"},{\"value\":\"PACS\",\"label\":\"PACS系统\"}]", IS_MULTI_SELECT = false },
                    new { ID = 4, PAGE_ID = 1, PARAM_NAME = "departments", PARAM_LABEL = "科室（多选）", PARAM_TYPE = "multi_select", IS_REQUIRED = false, DEFAULT_VALUE = "DEPT001,DEPT002", PARAM_OPTIONS = "[{\"value\":\"DEPT001\",\"label\":\"内科\"},{\"value\":\"DEPT002\",\"label\":\"外科\"},{\"value\":\"DEPT003\",\"label\":\"妇产科\"},{\"value\":\"DEPT004\",\"label\":\"儿科\"}]", IS_MULTI_SELECT = true }
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockParams));
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT ID, PAGE_ID, PARAM_NAME, PARAM_LABEL, PARAM_TYPE, PARAM_OPTIONS, IS_REQUIRED, IS_MULTI_SELECT, DEFAULT_VALUE, SORT_ORDER FROM STAT_PARAM_CONFIG WHERE PAGE_ID = @pageId ORDER BY SORT_ORDER";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pageId", pageId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dictionary<string, object>> paramsList = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param["ID"] = reader["ID"];
                            param["PAGE_ID"] = reader["PAGE_ID"];
                            param["PARAM_NAME"] = reader["PARAM_NAME"];
                            param["PARAM_LABEL"] = reader["PARAM_LABEL"];
                            param["PARAM_TYPE"] = reader["PARAM_TYPE"];
                            param["PARAM_OPTIONS"] = reader["PARAM_OPTIONS"];
                            param["IS_REQUIRED"] = reader["IS_REQUIRED"];
                            param["IS_MULTI_SELECT"] = reader["IS_MULTI_SELECT"];
                            param["DEFAULT_VALUE"] = reader["DEFAULT_VALUE"];
                            param["SORT_ORDER"] = reader["SORT_ORDER"];
                            paramsList.Add(param);
                        }
                        return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(paramsList));
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取参数配置失败: " + ex.Message);
                var mockParams = new[] {
                    new { ID = 1, PAGE_ID = pageId, PARAM_NAME = "startDate", PARAM_LABEL = "开始日期", PARAM_TYPE = "datetime", IS_REQUIRED = true, DEFAULT_VALUE = "2024-01-01", IS_MULTI_SELECT = false },
                    new { ID = 2, PAGE_ID = pageId, PARAM_NAME = "endDate", PARAM_LABEL = "结束日期", PARAM_TYPE = "datetime", IS_REQUIRED = true, DEFAULT_VALUE = "2024-12-31", IS_MULTI_SELECT = false }
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockParams));
            }
        }

        private string GetStatDisplay(string pageId)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                var mockDisplay = new[] {
                    new { ID = 1, PAGE_ID = 1, COLUMN_NAME = "DEPARTMENT_NAME", DISPLAY_NAME = "科室名称", COLUMN_WIDTH = "200px", ALIGNMENT = "left", IS_VISIBLE = true },
                    new { ID = 2, PAGE_ID = 1, COLUMN_NAME = "COUNT", DISPLAY_NAME = "数量", COLUMN_WIDTH = "100px", ALIGNMENT = "center", IS_VISIBLE = true },
                    new { ID = 3, PAGE_ID = 1, COLUMN_NAME = "AMOUNT", DISPLAY_NAME = "金额", COLUMN_WIDTH = "120px", ALIGNMENT = "right", IS_VISIBLE = true }
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockDisplay));
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT ID, PAGE_ID, COLUMN_NAME, DISPLAY_NAME, COLUMN_WIDTH, ALIGNMENT, IS_VISIBLE, SORT_ORDER FROM STAT_DISPLAY_CONFIG WHERE PAGE_ID = @pageId ORDER BY SORT_ORDER";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pageId", pageId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dictionary<string, object>> displayList = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            Dictionary<string, object> display = new Dictionary<string, object>();
                            display["ID"] = reader["ID"];
                            display["PAGE_ID"] = reader["PAGE_ID"];
                            display["COLUMN_NAME"] = reader["COLUMN_NAME"];
                            display["DISPLAY_NAME"] = reader["DISPLAY_NAME"];
                            display["COLUMN_WIDTH"] = reader["COLUMN_WIDTH"];
                            display["ALIGNMENT"] = reader["ALIGNMENT"];
                            display["IS_VISIBLE"] = reader["IS_VISIBLE"];
                            display["SORT_ORDER"] = reader["SORT_ORDER"];
                            displayList.Add(display);
                        }
                        return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(displayList));
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取显示配置失败: " + ex.Message);
                var mockDisplay = new[] {
                    new { ID = 1, PAGE_ID = pageId, COLUMN_NAME = "COL1", DISPLAY_NAME = "列1", COLUMN_WIDTH = "150px", ALIGNMENT = "left", IS_VISIBLE = true },
                    new { ID = 2, PAGE_ID = pageId, COLUMN_NAME = "COL2", DISPLAY_NAME = "列2", COLUMN_WIDTH = "150px", ALIGNMENT = "center", IS_VISIBLE = true }
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockDisplay));
            }
        }

        private string AddStatConfig(string postData)
        {
            try
            {
                dynamic config = Newtonsoft.Json.JsonConvert.DeserializeObject(postData);
                
                string pageName = config.pageName;
                string pageCode = config.pageCode;
                string procedureName = config.procedureName;
                string detailProcedure = config.detailProcedure ?? "";
                string linkField = config.linkField ?? "";
                string description = config.description ?? "";
                string configJson = config.configJson ?? "{}";

                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogMessage("统计配置添加成功（模拟模式）: " + pageCode);
                    return "{\"success\": true, \"message\": \"配置添加成功（模拟模式）\"}";
                }

                SqlConnection conn = LoginForm.GetConnection(connectionString);
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 插入页面配置
                        string sql = "INSERT INTO STAT_PAGE_CONFIG (PAGE_NAME, PAGE_CODE, PROCEDURE_NAME, DETAIL_PROCEDURE, LINK_FIELD, DESCRIPTION, CONFIG_JSON) OUTPUT INSERTED.ID VALUES (@pageName, @pageCode, @procedureName, @detailProcedure, @linkField, @description, @configJson)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pageName", pageName);
                            cmd.Parameters.AddWithValue("@pageCode", pageCode);
                            cmd.Parameters.AddWithValue("@procedureName", procedureName);
                            cmd.Parameters.AddWithValue("@detailProcedure", detailProcedure);
                            cmd.Parameters.AddWithValue("@linkField", linkField);
                            cmd.Parameters.AddWithValue("@description", description);
                            cmd.Parameters.AddWithValue("@configJson", configJson);
                            int pageId = (int)cmd.ExecuteScalar();
                        }

                        transaction.Commit();
                        LogMessage("统计配置添加成功: " + pageCode);
                        return "{\"success\": true, \"message\": \"配置添加成功\"}";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("添加统计配置失败: " + ex.Message);
                return string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
            }
        }

        private string UpdateStatConfig(string postData)
        {
            try
            {
                dynamic config = Newtonsoft.Json.JsonConvert.DeserializeObject(postData);
                int pageId = config.pageId;
                string pageName = config.pageName;
                string procedureName = config.procedureName;
                string configJson = config.configJson;

                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    LogMessage("统计配置更新成功（模拟模式）: " + pageId);
                    return "{\"success\": true, \"message\": \"配置更新成功（模拟模式）\"}";
                }

                SqlConnection conn = LoginForm.GetConnection(connectionString);
                string sql = "UPDATE STAT_PAGE_CONFIG SET PAGE_NAME = @pageName, PROCEDURE_NAME = @procedureName, CONFIG_JSON = @configJson, UPDATE_TIME = GETDATE() WHERE ID = @pageId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pageId", pageId);
                    cmd.Parameters.AddWithValue("@pageName", pageName);
                    cmd.Parameters.AddWithValue("@procedureName", procedureName);
                    cmd.Parameters.AddWithValue("@configJson", configJson);
                    cmd.ExecuteNonQuery();
                }

                LogMessage("统计配置更新成功: " + pageId);
                return "{\"success\": true, \"message\": \"配置更新成功\"}";
            }
            catch (Exception ex)
            {
                LogMessage("更新统计配置失败: " + ex.Message);
                return string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
            }
        }

        private string DeleteStatConfig(string pageCode)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                LogMessage("统计配置删除成功（模拟模式）: " + pageCode);
                return "{\"success\": true, \"message\": \"配置删除成功（模拟模式）\"}";
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 获取页面ID
                        string sql = "SELECT ID FROM STAT_PAGE_CONFIG WHERE PAGE_CODE = @pageCode";
                        int pageId = 0;
                        using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pageCode", pageCode);
                            object result = cmd.ExecuteScalar();
                            if (result != null) pageId = (int)result;
                        }

                        // 删除显示配置
                        sql = "DELETE FROM STAT_DISPLAY_CONFIG WHERE PAGE_ID = @pageId";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pageId", pageId);
                            cmd.ExecuteNonQuery();
                        }

                        // 删除参数配置
                        sql = "DELETE FROM STAT_PARAM_CONFIG WHERE PAGE_ID = @pageId";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pageId", pageId);
                            cmd.ExecuteNonQuery();
                        }

                        // 删除页面配置
                        sql = "DELETE FROM STAT_PAGE_CONFIG WHERE PAGE_CODE = @pageCode";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pageCode", pageCode);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        LogMessage("统计配置删除成功: " + pageCode);
                        return "{\"success\": true, \"message\": \"配置删除成功\"}";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("删除统计配置失败: " + ex.Message);
                return string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
            }
        }

        private string ExecuteStat(string postData)
        {
            try
            {
                dynamic config = Newtonsoft.Json.JsonConvert.DeserializeObject(postData);
                string pageCode = config.pageCode;
                dynamic parameters = config.parameters ?? new { };

                // 获取页面配置
                string pageConfigJson = GetStatPage(pageCode);
                dynamic pageConfig = Newtonsoft.Json.JsonConvert.DeserializeObject(pageConfigJson);
                
                if (!pageConfig.success)
                {
                    return pageConfigJson;
                }

                string procedureName = pageConfig.data.PROCEDURE_NAME;
                
                // 构建参数字符串
                List<string> paramList = new List<string>();
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    object value = prop.GetValue(parameters);
                    if (value != null)
                    {
                        paramList.Add(value.ToString());
                    }
                }
                string paramsStr = string.Join(",", paramList);

                // 调用存储过程
                DataTable result = CallStoredProcedure(procedureName, paramsStr);
                
                // 获取显示配置
                string displayConfigJson = GetStatDisplay(pageConfig.data.ID.ToString());
                dynamic displayConfig = Newtonsoft.Json.JsonConvert.DeserializeObject(displayConfigJson);

                // 构建完整结果（包含关联信息）
                var resultObj = new {
                    success = true,
                    data = ConvertDataTableToList(result),
                    linkField = pageConfig.data.LINK_FIELD,
                    detailProcedure = pageConfig.data.DETAIL_PROCEDURE,
                    displayConfig = displayConfig.success ? displayConfig.data : new object[] { }
                };
                
                return Newtonsoft.Json.JsonConvert.SerializeObject(resultObj);
            }
            catch (Exception ex)
            {
                LogMessage("执行统计失败: " + ex.Message);
                return string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
            }
        }

        private string ExecuteStatDetail(string postData)
        {
            try
            {
                dynamic config = Newtonsoft.Json.JsonConvert.DeserializeObject(postData);
                string pageCode = config.pageCode;
                string linkValue = config.linkValue;
                dynamic baseParams = config.parameters ?? new { };

                // 获取页面配置
                string pageConfigJson = GetStatPage(pageCode);
                dynamic pageConfig = Newtonsoft.Json.JsonConvert.DeserializeObject(pageConfigJson);
                
                if (!pageConfig.success)
                {
                    return pageConfigJson;
                }

                string detailProcedure = pageConfig.data.DETAIL_PROCEDURE;
                string linkField = pageConfig.data.LINK_FIELD;
                
                if (string.IsNullOrEmpty(detailProcedure))
                {
                    return "{\"success\": false, \"error\": \"未配置明细存储过程\"}";
                }

                // 构建参数字符串（基础参数 + 关联字段）
                List<string> paramList = new List<string>();
                paramList.Add(linkValue); // 关联字段值作为第一个参数
                
                foreach (var prop in baseParams.GetType().GetProperties())
                {
                    object value = prop.GetValue(baseParams);
                    if (value != null)
                    {
                        paramList.Add(value.ToString());
                    }
                }
                string paramsStr = string.Join(",", paramList);

                // 调用明细存储过程
                DataTable result = CallStoredProcedure(detailProcedure, paramsStr);

                // 转换结果
                string jsonResult = ConvertDataTableToJson(result);
                return jsonResult;
            }
            catch (Exception ex)
            {
                LogMessage("执行明细查询失败: " + ex.Message);
                return string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
            }
        }

        private string GetRegisteredProcedures(string procedureType)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                // 返回模拟数据（固定格式 usp_tjfx_xxxx）
                var mockProcs = new[] {
                    new { ID = 1, PROCEDURE_NAME = "usp_tjfx_ksgzltj", PROCEDURE_TYPE = "query", DESCRIPTION = "获取科室工作量汇总" },
                    new { ID = 2, PROCEDURE_NAME = "usp_tjfx_ksgzlmx", PROCEDURE_TYPE = "detail", DESCRIPTION = "获取科室工作量明细" },
                    new { ID = 3, PROCEDURE_NAME = "usp_tjfx_ysgzltj", PROCEDURE_TYPE = "query", DESCRIPTION = "获取医生工作量汇总" },
                    new { ID = 4, PROCEDURE_NAME = "usp_tjfx_ysgzlmx", PROCEDURE_TYPE = "detail", DESCRIPTION = "获取医生工作量明细" },
                    new { ID = 5, PROCEDURE_NAME = "usp_tjfx_jclxtj", PROCEDURE_TYPE = "query", DESCRIPTION = "获取检查类型统计" },
                    new { ID = 6, PROCEDURE_NAME = "usp_tjfx_jclxmx", PROCEDURE_TYPE = "detail", DESCRIPTION = "获取检查类型明细" },
                    new { ID = 7, PROCEDURE_NAME = "usp_tjfx_mrtj", PROCEDURE_TYPE = "query", DESCRIPTION = "获取每日统计数据" },
                    new { ID = 8, PROCEDURE_NAME = "usp_tjfx_ydbg", PROCEDURE_TYPE = "summary", DESCRIPTION = "生成月度报告" },
                    new { ID = 9, PROCEDURE_NAME = "usp_tjfx_rptj", PROCEDURE_TYPE = "query", DESCRIPTION = "获取日报统计" },
                    new { ID = 10, PROCEDURE_NAME = "usp_tjfx_ndbg", PROCEDURE_TYPE = "summary", DESCRIPTION = "生成年度报告" }
                };
                
                var filtered = string.IsNullOrEmpty(procedureType) ? mockProcs : mockProcs.Where(p => p.PROCEDURE_TYPE == procedureType).ToArray();
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(filtered));
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT ID, PROCEDURE_NAME, PROCEDURE_TYPE, DESCRIPTION FROM STAT_PROCEDURE_REGISTRY WHERE IS_ACTIVE = 1";
                if (!string.IsNullOrEmpty(procedureType))
                {
                    sql += " AND PROCEDURE_TYPE = @procedureType";
                }
                sql += " ORDER BY PROCEDURE_NAME";
                
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(procedureType))
                    {
                        cmd.Parameters.AddWithValue("@procedureType", procedureType);
                    }
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dictionary<string, object>> procs = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            Dictionary<string, object> proc = new Dictionary<string, object>();
                            proc["ID"] = reader["ID"];
                            proc["PROCEDURE_NAME"] = reader["PROCEDURE_NAME"];
                            proc["PROCEDURE_TYPE"] = reader["PROCEDURE_TYPE"];
                            proc["DESCRIPTION"] = reader["DESCRIPTION"];
                            procs.Add(proc);
                        }
                        return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(procs));
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取存储过程列表失败: " + ex.Message);
                // 返回模拟数据
                var mockProcs = new[] {
                    new { ID = 1, PROCEDURE_NAME = "usp_GetDepartmentWorkload", PROCEDURE_TYPE = "query", DESCRIPTION = "获取科室工作量汇总" },
                    new { ID = 2, PROCEDURE_NAME = "usp_GetDepartmentDetail", PROCEDURE_TYPE = "detail", DESCRIPTION = "获取科室工作量明细" },
                    new { ID = 3, PROCEDURE_NAME = "usp_GetDoctorWorkload", PROCEDURE_TYPE = "query", DESCRIPTION = "获取医生工作量汇总" }
                };
                return string.Format("{{\"success\": true, \"data\": {0}}}", Newtonsoft.Json.JsonConvert.SerializeObject(mockProcs));
            }
        }

        private string GetProcedureInfo(string procedureName)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                // 返回模拟数据
                var mockParams = new[] {
                    new { name = "startDate", type = "datetime", required = true, label = "开始日期" },
                    new { name = "endDate", type = "datetime", required = true, label = "结束日期" },
                    new { name = "system", type = "string", required = false, label = "系统类型" }
                };
                var mockColumns = new[] {
                    new { name = "DEPARTMENT_CODE", type = "string", label = "科室编码" },
                    new { name = "DEPARTMENT_NAME", type = "string", label = "科室名称" },
                    new { name = "COUNT", type = "int", label = "数量" },
                    new { name = "AMOUNT", type = "decimal", label = "金额" }
                };
                var result = new {
                    success = true,
                    data = new {
                        procedureName = procedureName,
                        description = "存储过程描述",
                        parameters = mockParams,
                        returnColumns = mockColumns
                    }
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                string sql = "SELECT PROCEDURE_NAME, DESCRIPTION, PARAMS_JSON, RETURN_COLUMNS FROM STAT_PROCEDURE_REGISTRY WHERE PROCEDURE_NAME = @procedureName AND IS_ACTIVE = 1";
                
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@procedureName", procedureName);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var result = new {
                                success = true,
                                data = new {
                                    procedureName = reader["PROCEDURE_NAME"],
                                    description = reader["DESCRIPTION"],
                                    parameters = Newtonsoft.Json.JsonConvert.DeserializeObject(reader["PARAMS_JSON"].ToString() ?? "[]"),
                                    returnColumns = Newtonsoft.Json.JsonConvert.DeserializeObject(reader["RETURN_COLUMNS"].ToString() ?? "[]")
                                }
                            };
                            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            return "{\"success\": false, \"error\": \"存储过程未注册\"}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取存储过程信息失败: " + ex.Message);
                return string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
            }
        }

        private string DiscoverProcedures(string postData)
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                return "{\"success\": false, \"error\": \"未配置数据库连接\"}";
            }

            try
            {
                SqlConnection conn = LoginForm.GetConnection(connectionString);
                int discoveredCount = 0;

                // 查询数据库中所有以 usp_tjfx_ 开头的存储过程（固定格式）
                string sql = @"
                    SELECT p.name AS procedure_name
                    FROM sys.procedures p
                    WHERE p.name LIKE 'usp_tjfx_%' 
                      AND p.is_ms_shipped = 0
                    ORDER BY p.name";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string procName = reader["procedure_name"].ToString();
                            
                            // 检查是否已注册
                            string checkSql = "SELECT ID FROM STAT_PROCEDURE_REGISTRY WHERE PROCEDURE_NAME = @procName";
                            using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@procName", procName);
                                object exists = checkCmd.ExecuteScalar();
                                
                                if (exists == null)
                                {
                                    // 获取参数信息
                                    string paramsJson = GetProcedureParameters(conn, procName);
                                    string columnsJson = GetProcedureColumns(conn, procName);
                                    string procType = GetProcedureType(procName);
                                    
                                    // 插入注册记录
                                    string insertSql = @"
                                        INSERT INTO STAT_PROCEDURE_REGISTRY 
                                        (PROCEDURE_NAME, PROCEDURE_TYPE, DESCRIPTION, PARAMS_JSON, RETURN_COLUMNS)
                                        VALUES (@procName, @procType, @description, @paramsJson, @columnsJson)";
                                    using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@procName", procName);
                                        insertCmd.Parameters.AddWithValue("@procType", procType);
                                        insertCmd.Parameters.AddWithValue("@description", "自动发现的存储过程");
                                        insertCmd.Parameters.AddWithValue("@paramsJson", paramsJson);
                                        insertCmd.Parameters.AddWithValue("@columnsJson", columnsJson);
                                        insertCmd.ExecuteNonQuery();
                                        discoveredCount++;
                                    }
                                }
                            }
                        }
                    }
                }

                return string.Format("{{\"success\": true, \"message\": \"发现并注册了 {0} 个存储过程\"}}", discoveredCount);
            }
            catch (Exception ex)
            {
                LogMessage("发现存储过程失败: " + ex.Message);
                return string.Format("{{\"success\": false, \"error\": \"{0}\"}}", ex.Message);
            }
        }

        private string GetProcedureParameters(SqlConnection conn, string procedureName)
        {
            try
            {
                string sql = @"
                    SELECT 
                        p.name AS parameter_name,
                        t.name AS data_type,
                        CASE WHEN p.is_output = 1 THEN 'output' ELSE 'input' END AS direction,
                        CASE WHEN p.is_nullable = 0 THEN '1' ELSE '0' END AS is_required
                    FROM sys.parameters p
                    JOIN sys.types t ON p.system_type_id = t.system_type_id
                    WHERE p.object_id = OBJECT_ID(@procedureName)
                    ORDER BY p.parameter_id";

                List<object> parameters = new List<object>();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@procedureName", procedureName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parameters.Add(new {
                                name = reader["parameter_name"],
                                type = ConvertSqlTypeToDotNet(reader["data_type"].ToString()),
                                required = reader["is_required"].ToString() == "1",
                                direction = reader["direction"]
                            });
                        }
                    }
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
            }
            catch
            {
                return "[]";
            }
        }

        private string GetProcedureColumns(SqlConnection conn, string procedureName)
        {
            try
            {
                // 执行存储过程获取返回列（需要参数，这里简化处理）
                return "[{\"name\":\"ID\",\"type\":\"int\"},{\"name\":\"NAME\",\"type\":\"string\"}]";
            }
            catch
            {
                return "[]";
            }
        }

        private string GetProcedureType(string procedureName)
        {
            if (procedureName.EndsWith("Detail", StringComparison.OrdinalIgnoreCase))
                return "detail";
            if (procedureName.EndsWith("Summary", StringComparison.OrdinalIgnoreCase) || 
                procedureName.EndsWith("Report", StringComparison.OrdinalIgnoreCase))
                return "summary";
            return "query";
        }

        private string ConvertSqlTypeToDotNet(string sqlType)
        {
            switch (sqlType.ToLower())
            {
                case "int":
                case "bigint":
                case "smallint":
                    return "int";
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    return "datetime";
                case "decimal":
                case "numeric":
                case "float":
                case "real":
                    return "decimal";
                case "bit":
                    return "bool";
                default:
                    return "string";
            }
        }

        private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col] != DBNull.Value ? row[col] : null;
                }
                list.Add(dict);
            }
            return list;
        }

        private string ExtractValue(string json, string key)
        {
            // 先尝试查找字符串值
            string pattern = string.Format("\"{0}\":", key);
            int startSearch = json.IndexOf(pattern);
            if (startSearch >= 0)
            {
                startSearch += pattern.Length;
                // 跳过空白字符
                startSearch = json.IndexOfAny(new char[] {'"', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '{', '[', 't', 'f', 'n'}, startSearch);
                if (startSearch >= 0)
                {
                    // 检查是否是字符串值（以引号开头）
                    if (json[startSearch] == '"')
                    {
                        startSearch++;
                        int endIndex = json.IndexOf('"', startSearch);
                        if (endIndex > startSearch)
                        {
                            return json.Substring(startSearch, endIndex - startSearch);
                        }
                    }
                    else
                    {
                        // 处理数字值、布尔值、null等
                        int endIndex = json.IndexOfAny(new char[] {',', '}', ']', ' ', '\n', '\r'}, startSearch);
                        if (endIndex > startSearch)
                        {
                            return json.Substring(startSearch, endIndex - startSearch).Trim();
                        }
                        else if (endIndex == -1)
                        {
                            // 如果到字符串末尾
                            return json.Substring(startSearch).Trim();
                        }
                    }
                }
            }
            return "";
        }

        private string ConvertDataTableToJson(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return string.Format("{{\"success\": true, \"data\": {0}}}", JsonConvert.SerializeObject(rows));
        }

        private DataTable GetDailyAnalysisData(string startDate, string endDate, string system, string reporter, string reviewer, string technician, string department, string category, string patientType = "")
        {
            DataTable dt = new DataTable();
            string connectionString = GetConnectionString();
            
            if (string.IsNullOrEmpty(connectionString))
            {
                dt.Columns.Add("系统", typeof(string));
                dt.Columns.Add("报告医生", typeof(string));
                dt.Columns.Add("审核医生", typeof(string));
                dt.Columns.Add("技师", typeof(string));
                dt.Columns.Add("执行科室", typeof(string));
                dt.Columns.Add("检查类型", typeof(string));
                dt.Columns.Add("病人类型", typeof(string));
                dt.Columns.Add("任务数量", typeof(int));
                dt.Columns.Add("阳性率", typeof(decimal));
                
                dt.Rows.Add(system, reporter, reviewer, technician, department, category, patientType, 100, 25.5);
                dt.Rows.Add(system, reporter, reviewer, technician, department, category, patientType, 80, 30.2);
                dt.Rows.Add(system, reporter, reviewer, technician, department, category, patientType, 60, 18.8);
                return dt;
            }

            SqlConnection conn = null;
            try
            {
                conn = LoginForm.GetConnection(connectionString);
                
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"SELECT 
                    ISNULL(t.SYSTEM_SOURCE_NO, '') AS 系统,
                    ISNULL(r.REPORTER_NAME, '') AS 报告医生,
                    ISNULL(r.REVIEWER_NAME, '') AS 审核医生,
                    ISNULL(t.TECHNICIAN_NAME, '') AS 技师,
                    CASE 
                        WHEN t.EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
                        WHEN t.EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
                        WHEN t.EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
                        WHEN t.EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
                        WHEN t.EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
                        WHEN t.EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
                        ELSE '其他科室'
                    END AS 执行科室,
                    ISNULL(t.EXAM_CATEGORY_NAME, '') AS 检查类型,
                    ISNULL(ti.PATIENT_TYPE, '') AS 病人类型,
                    COUNT(*) AS 任务数量,
                    ISNULL(AVG(CASE WHEN r.REPORT_RESULT LIKE '%阳性%' THEN 100.0 ELSE 0 END), 0) AS 阳性率
                FROM EXAM_TASK t
                LEFT JOIN EXAM_REPORT r ON t.ID = r.TASK_ID
                LEFT JOIN EXAM_TASK_INFO ti ON t.ID = ti.TASK_ID
                WHERE t.IS_DEL = 0");

                List<string> conditions = new List<string>();
                
                if (!string.IsNullOrEmpty(startDate))
                {
                    conditions.Add("t.EXAM_TASK_CREATE_TIME >= @StartDate");
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    conditions.Add("t.EXAM_TASK_CREATE_TIME < DATEADD(DAY, 1, @EndDate)");
                }
                if (!string.IsNullOrEmpty(system))
                {
                    conditions.Add("t.SYSTEM_SOURCE_NO = @System");
                }
                if (!string.IsNullOrEmpty(reporter))
                {
                    conditions.Add("r.REPORTER_NAME = @Reporter");
                }
                if (!string.IsNullOrEmpty(reviewer))
                {
                    conditions.Add("r.REVIEWER_NAME = @Reviewer");
                }
                if (!string.IsNullOrEmpty(technician))
                {
                    conditions.Add("t.TECHNICIAN_NAME = @Technician");
                }
                if (!string.IsNullOrEmpty(department))
                {
                    conditions.Add(@"CASE 
                        WHEN t.EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
                        WHEN t.EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
                        WHEN t.EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
                        WHEN t.EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
                        WHEN t.EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
                        WHEN t.EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
                        ELSE '其他科室'
                    END = @Department");
                }
                if (!string.IsNullOrEmpty(category))
                {
                    conditions.Add("t.EXAM_CATEGORY_NAME = @Category");
                }
                if (!string.IsNullOrEmpty(patientType))
                {
                    conditions.Add("ti.PATIENT_TYPE = @PatientType");
                }

                if (conditions.Count > 0)
                {
                    sqlBuilder.Append(" AND " + string.Join(" AND ", conditions));
                }

                sqlBuilder.Append(@"
                GROUP BY 
                    ISNULL(t.SYSTEM_SOURCE_NO, ''),
                    ISNULL(r.REPORTER_NAME, ''),
                    ISNULL(r.REVIEWER_NAME, ''),
                    ISNULL(t.TECHNICIAN_NAME, ''),
                    CASE 
                        WHEN t.EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
                        WHEN t.EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
                        WHEN t.EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
                        WHEN t.EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
                        WHEN t.EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
                        WHEN t.EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
                        ELSE '其他科室'
                    END,
                    ISNULL(t.EXAM_CATEGORY_NAME, ''),
                    ISNULL(ti.PATIENT_TYPE, '')
                ORDER BY 任务数量 DESC");

                using (SqlCommand cmd = new SqlCommand(sqlBuilder.ToString(), conn))
                {
                    if (!string.IsNullOrEmpty(startDate))
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                    if (!string.IsNullOrEmpty(endDate))
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                    if (!string.IsNullOrEmpty(system))
                        cmd.Parameters.AddWithValue("@System", system);
                    if (!string.IsNullOrEmpty(reporter))
                        cmd.Parameters.AddWithValue("@Reporter", reporter);
                    if (!string.IsNullOrEmpty(reviewer))
                        cmd.Parameters.AddWithValue("@Reviewer", reviewer);
                    if (!string.IsNullOrEmpty(technician))
                        cmd.Parameters.AddWithValue("@Technician", technician);
                    if (!string.IsNullOrEmpty(department))
                        cmd.Parameters.AddWithValue("@Department", department);
                    if (!string.IsNullOrEmpty(category))
                        cmd.Parameters.AddWithValue("@Category", category);
                    if (!string.IsNullOrEmpty(patientType))
                        cmd.Parameters.AddWithValue("@PatientType", patientType);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                RecordQueryHistory(conn, startDate, endDate, system, reporter, reviewer, technician, department, category, dt.Rows.Count, null);

                return dt;
            }
            catch (Exception ex)
            {
                LogMessage("获取统计数据失败: " + ex.Message);
                RecordQueryHistory(conn, startDate, endDate, system, reporter, reviewer, technician, department, category, 0, ex.Message);
                dt.Columns.Add("错误", typeof(string));
                dt.Rows.Add(ex.Message);
                return dt;
            }
        }

        private string GetQuerySqlFromDatabase(SqlConnection conn, string type)
        {
            try
            {
                string sql = "SELECT QUERY_SQL FROM DAILY_QUERY_CONFIG WHERE QUERY_TYPE = @type";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@type", type);
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                LogMessage("从数据库获取查询SQL失败: " + ex.Message);
                return null;
            }
        }

        private string GetDefaultQuerySql(string type)
        {
            switch (type)
            {
                case "system":
                    return @"SELECT SYSTEM_SOURCE_NO AS '系统标识', COUNT(*) AS '任务数量', 
                            MAX(EXAM_TASK_CREATE_TIME) AS '最新任务时间'
                            FROM EXAM_TASK WHERE IS_DEL = 0
                            GROUP BY SYSTEM_SOURCE_NO ORDER BY COUNT(*) DESC";
                case "department":
                    return @"SELECT CASE 
                            WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
                            WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
                            WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
                            WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
                            WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
                            WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
                            ELSE '其他科室'
                        END AS '执行科室', COUNT(*) AS '任务数量'
                            FROM EXAM_TASK WHERE IS_DEL = 0
                            GROUP BY CASE 
                            WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
                            WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
                            WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
                            WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
                            WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
                            WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
                            ELSE '其他科室'
                        END ORDER BY COUNT(*) DESC";
                case "category":
                    return @"SELECT EXAM_CATEGORY_NAME AS '检查类别', COUNT(*) AS '数量',
                            MAX(EXAM_TASK_CREATE_TIME) AS '最新检查时间'
                            FROM EXAM_TASK WHERE IS_DEL = 0
                            GROUP BY EXAM_CATEGORY_NAME ORDER BY COUNT(*) DESC";
                case "reporter":
                    return @"SELECT REPORTER_NAME AS '医生姓名', COUNT(*) AS '报告数量',
                            MAX(REPORT_TIME) AS '最新报告时间'
                            FROM EXAM_REPORT WHERE IS_DEL = 0
                            GROUP BY REPORTER_NAME ORDER BY COUNT(*) DESC";
                case "reviewer":
                    return @"SELECT REVIEWER_NAME AS '医生姓名', COUNT(*) AS '审核数量',
                            MAX(REVIEW_TIME) AS '最新审核时间'
                            FROM EXAM_REPORT WHERE IS_DEL = 0
                            GROUP BY REVIEWER_NAME ORDER BY COUNT(*) DESC";
                case "technician":
                    return @"SELECT TECHNICIAN_NAME AS '技师姓名', COUNT(*) AS '检查数量',
                            MAX(EXAM_TASK_CREATE_TIME) AS '最新检查时间'
                            FROM EXAM_TASK WHERE IS_DEL = 0
                            GROUP BY TECHNICIAN_NAME ORDER BY COUNT(*) DESC";
                case "status":
                    return @"SELECT EXAM_TASK_STATUS AS '任务状态', COUNT(*) AS '数量',
                            MAX(EXAM_TASK_CREATE_TIME) AS '最新时间'
                            FROM EXAM_TASK WHERE IS_DEL = 0
                            GROUP BY EXAM_TASK_STATUS ORDER BY COUNT(*) DESC";
                case "rank":
                    return @"SELECT CASE 
                            WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
                            WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
                            WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
                            WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
                            WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
                            WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
                            ELSE '其他科室'
                        END AS '执行科室', COUNT(*) AS '总任务数',
                        SUM(CASE WHEN EXAM_TASK_STATUS = '已完成' THEN 1 ELSE 0 END) AS '已完成数',
                        ROUND(SUM(CASE WHEN EXAM_TASK_STATUS = '已完成' THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2) AS '完成率',
                        MAX(EXAM_TASK_CREATE_TIME) AS '最新任务时间'
                            FROM EXAM_TASK WHERE IS_DEL = 0
                            GROUP BY CASE 
                            WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
                            WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
                            WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
                            WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
                            WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
                            WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
                            ELSE '其他科室'
                        END ORDER BY COUNT(*) DESC";
                default:
                    return @"SELECT SYSTEM_SOURCE_NO AS '系统', COUNT(*) AS '任务数量'
                            FROM EXAM_TASK WHERE IS_DEL = 0
                            GROUP BY SYSTEM_SOURCE_NO ORDER BY COUNT(*) DESC";
            }
        }

        private void RecordQueryHistory(SqlConnection conn, string startDate, string endDate, string system, string reporter, string reviewer, string technician, string department, string category, int rowCount, string errorMessage)
        {
            try
            {
                string sql = "INSERT INTO DAILY_QUERY_HISTORY (QUERY_DATE, QUERY_TYPE, QUERY_PARAMS, EXECUTION_STATUS, ROW_COUNT, ERROR_MESSAGE) VALUES (@queryDate, @queryType, @queryParams, @executionStatus, @rowCount, @errorMessage)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@queryDate", startDate);
                    cmd.Parameters.AddWithValue("@queryType", "daily");
                    cmd.Parameters.AddWithValue("@queryParams", string.Format("{{'startDate': '{0}', 'endDate': '{1}', 'system': '{2}', 'reporter': '{3}', 'reviewer': '{4}', 'technician': '{5}', 'department': '{6}', 'category': '{7}'}}", 
                        startDate, endDate, system, reporter, reviewer, technician, department, category));
                    cmd.Parameters.AddWithValue("@executionStatus", string.IsNullOrEmpty(errorMessage) ? "成功" : "失败");
                    cmd.Parameters.AddWithValue("@rowCount", rowCount);
                    cmd.Parameters.AddWithValue("@errorMessage", errorMessage);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogMessage("记录查询历史失败: " + ex.Message);
            }
        }

        private string GetSystemTypes()
        {
            try
            {
                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    // 如果没有配置数据库连接，返回默认系统类型
                    return "{\"success\": true, \"data\": [{\"code\": \"RIS\", \"name\": \"RIS（放射）\"}, {\"code\": \"UIS\", \"name\": \"UIS（超声）\"}, {\"code\": \"EIS\", \"name\": \"EIS（内镜）\"}]}";
                }

                using (SqlConnection conn = LoginForm.GetConnection(connectionString))
                {
                    // 从EXAM_TASK表中获取唯一的系统类型
                    string sql = "SELECT DISTINCT SYSTEM_SOURCE_NO AS code, SYSTEM_SOURCE_NO + '（系统）' AS name FROM EXAM_TASK WHERE IS_DEL = 0 AND SYSTEM_SOURCE_NO IS NOT NULL ORDER BY SYSTEM_SOURCE_NO";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dictionary<string, string>> systems = new List<Dictionary<string, string>>();
                        while (reader.Read())
                        {
                            systems.Add(new Dictionary<string, string>
                            {
                                { "code", reader["code"].ToString() },
                                { "name", reader["name"].ToString() }
                            });
                        }

                        // 如果没有数据，返回默认系统类型
                        if (systems.Count == 0)
                        {
                            return "{\"success\": true, \"data\": [{\"code\": \"RIS\", \"name\": \"RIS（放射）\"}, {\"code\": \"UIS\", \"name\": \"UIS（超声）\"}, {\"code\": \"EIS\", \"name\": \"EIS（内镜）\"}]}";
                        }

                        // 转换为JSON
                        var jsonObj = new { success = true, data = systems };
                        return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取系统类型失败: " + ex.Message);
                // 出错时返回默认系统类型
                return "{\"success\": true, \"data\": [{\"code\": \"RIS\", \"name\": \"RIS（放射）\"}, {\"code\": \"UIS\", \"name\": \"UIS（超声）\"}, {\"code\": \"EIS\", \"name\": \"EIS（内镜）\"}]}";
            }
        }

        private string GetReporters(string system)
        {
            try
            {
                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    // 如果没有配置数据库连接，返回默认报告医生
                    return "{\"success\": true, \"data\": [{\"code\": \"张医生\", \"name\": \"张医生\"}, {\"code\": \"李医生\", \"name\": \"李医生\"}, {\"code\": \"王医生\", \"name\": \"王医生\"}, {\"code\": \"赵医生\", \"name\": \"赵医生\"}, {\"code\": \"钱医生\", \"name\": \"钱医生\"}, {\"code\": \"孙医生\", \"name\": \"孙医生\"}, {\"code\": \"周医生\", \"name\": \"周医生\"}, {\"code\": \"吴医生\", \"name\": \"吴医生\"}, {\"code\": \"郑医生\", \"name\": \"郑医生\"}, {\"code\": \"陈医生\", \"name\": \"陈医生\"}]}";
                }

                using (SqlConnection conn = LoginForm.GetConnection(connectionString))
                {
                    // 直接查询数据库表获取报告医生
                    string sql = @"
                        SELECT DOCTOR_CODE AS code, DOCTOR_NAME AS name 
                        FROM DOCTOR_INFO 
                        WHERE IS_REPORTER = 1 AND IS_ACTIVE = 1
                        AND (@System IS NULL OR @System = '' OR SYSTEM_TYPE = @System)
                        ORDER BY DOCTOR_NAME";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Dictionary<string, string>> reporters = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                reporters.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"].ToString() },
                                    { "name", reader["name"].ToString() }
                                });
                            }

                            // 如果没有数据，返回默认报告医生
                            if (reporters.Count == 0)
                            {
                                return "{\"success\": true, \"data\": [{\"code\": \"张医生\", \"name\": \"张医生\"}, {\"code\": \"李医生\", \"name\": \"李医生\"}, {\"code\": \"王医生\", \"name\": \"王医生\"}, {\"code\": \"赵医生\", \"name\": \"赵医生\"}, {\"code\": \"钱医生\", \"name\": \"钱医生\"}, {\"code\": \"孙医生\", \"name\": \"孙医生\"}, {\"code\": \"周医生\", \"name\": \"周医生\"}, {\"code\": \"吴医生\", \"name\": \"吴医生\"}, {\"code\": \"郑医生\", \"name\": \"郑医生\"}, {\"code\": \"陈医生\", \"name\": \"陈医生\"}]}";
                            }

                            // 转换为JSON
                            var jsonObj = new { success = true, data = reporters };
                            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取报告医生失败: " + ex.Message);
                // 出错时返回默认报告医生
                return "{\"success\": true, \"data\": [{\"code\": \"张医生\", \"name\": \"张医生\"}, {\"code\": \"李医生\", \"name\": \"李医生\"}, {\"code\": \"王医生\", \"name\": \"王医生\"}, {\"code\": \"赵医生\", \"name\": \"赵医生\"}, {\"code\": \"钱医生\", \"name\": \"钱医生\"}, {\"code\": \"孙医生\", \"name\": \"孙医生\"}, {\"code\": \"周医生\", \"name\": \"周医生\"}, {\"code\": \"吴医生\", \"name\": \"吴医生\"}, {\"code\": \"郑医生\", \"name\": \"郑医生\"}, {\"code\": \"陈医生\", \"name\": \"陈医生\"}]}";
            }
        }

        private string GetReviewers(string system)
        {
            try
            {
                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    // 如果没有配置数据库连接，返回默认审核医生
                    return "{\"success\": true, \"data\": [{\"code\": \"赵医生\", \"name\": \"赵医生\"}, {\"code\": \"钱医生\", \"name\": \"钱医生\"}, {\"code\": \"孙医生\", \"name\": \"孙医生\"}, {\"code\": \"周医生\", \"name\": \"周医生\"}, {\"code\": \"吴医生\", \"name\": \"吴医生\"}, {\"code\": \"郑医生\", \"name\": \"郑医生\"}, {\"code\": \"陈医生\", \"name\": \"陈医生\"}, {\"code\": \"杨医生\", \"name\": \"杨医生\"}, {\"code\": \"黄医生\", \"name\": \"黄医生\"}, {\"code\": \"刘医生\", \"name\": \"刘医生\"}]}";
                }

                using (SqlConnection conn = LoginForm.GetConnection(connectionString))
                {
                    // 直接查询数据库表获取审核医生
                    string sql = @"
                        SELECT DOCTOR_CODE AS code, DOCTOR_NAME AS name 
                        FROM DOCTOR_INFO 
                        WHERE IS_REVIEWER = 1 AND IS_ACTIVE = 1
                        AND (@System IS NULL OR @System = '' OR SYSTEM_TYPE = @System)
                        ORDER BY DOCTOR_NAME";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Dictionary<string, string>> reviewers = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                reviewers.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"].ToString() },
                                    { "name", reader["name"].ToString() }
                                });
                            }

                            // 如果没有数据，返回默认审核医生
                            if (reviewers.Count == 0)
                            {
                                return "{\"success\": true, \"data\": [{\"code\": \"赵医生\", \"name\": \"赵医生\"}, {\"code\": \"钱医生\", \"name\": \"钱医生\"}, {\"code\": \"孙医生\", \"name\": \"孙医生\"}, {\"code\": \"周医生\", \"name\": \"周医生\"}, {\"code\": \"吴医生\", \"name\": \"吴医生\"}, {\"code\": \"郑医生\", \"name\": \"郑医生\"}, {\"code\": \"陈医生\", \"name\": \"陈医生\"}, {\"code\": \"杨医生\", \"name\": \"杨医生\"}, {\"code\": \"黄医生\", \"name\": \"黄医生\"}, {\"code\": \"刘医生\", \"name\": \"刘医生\"}]}";
                            }

                            // 转换为JSON
                            var jsonObj = new { success = true, data = reviewers };
                            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取审核医生失败: " + ex.Message);
                // 出错时返回默认审核医生
                return "{\"success\": true, \"data\": [{\"code\": \"赵医生\", \"name\": \"赵医生\"}, {\"code\": \"钱医生\", \"name\": \"钱医生\"}, {\"code\": \"孙医生\", \"name\": \"孙医生\"}, {\"code\": \"周医生\", \"name\": \"周医生\"}, {\"code\": \"吴医生\", \"name\": \"吴医生\"}, {\"code\": \"郑医生\", \"name\": \"郑医生\"}, {\"code\": \"陈医生\", \"name\": \"陈医生\"}, {\"code\": \"杨医生\", \"name\": \"杨医生\"}, {\"code\": \"黄医生\", \"name\": \"黄医生\"}, {\"code\": \"刘医生\", \"name\": \"刘医生\"}]}";
            }
        }

        private string GetCategories(string system)
        {
            try
            {
                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    // 如果没有配置数据库连接，返回默认检查类型
                    return "{\"success\": true, \"data\": [{\"code\": \"CT\", \"name\": \"CT\"}, {\"code\": \"CT(新)\", \"name\": \"CT(新)\"}, {\"code\": \"MRI\", \"name\": \"核磁共振\"}, {\"code\": \"钼靶\", \"name\": \"钼靶\"}, {\"code\": \"普放\", \"name\": \"普放\"}, {\"code\": \"普放(新)\", \"name\": \"普放(新)\"}, {\"code\": \"消化道造影\", \"name\": \"消化道造影\"}, {\"code\": \"消化道造影(新)\", \"name\": \"消化道造影(新)\"}, {\"code\": \"超声\", \"name\": \"超声\"}, {\"code\": \"介入超声\", \"name\": \"介入超声\"}, {\"code\": \"胃镜(老)\", \"name\": \"胃镜(老)\"}, {\"code\": \"胃镜(新)\", \"name\": \"胃镜(新)\"}, {\"code\": \"肠镜(老)\", \"name\": \"肠镜(老)\"}, {\"code\": \"肠镜(新)\", \"name\": \"肠镜(新)\"}, {\"code\": \"超声内镜\", \"name\": \"超声内镜\"}, {\"code\": \"消化肠镜\", \"name\": \"消化肠镜\"}, {\"code\": \"消化胃镜\", \"name\": \"消化胃镜\"}, {\"code\": \"支气管镜(新)\", \"name\": \"支气管镜(新)\"}, {\"code\": \"支气管镜(总)\", \"name\": \"支气管镜(总)\"}, {\"code\": \"脑电\", \"name\": \"脑电\"}, {\"code\": \"体检彩超\", \"name\": \"体检彩超\"}, {\"code\": \"新城体检彩超\", \"name\": \"新城体检彩超\"}]}";
                }

                using (SqlConnection conn = LoginForm.GetConnection(connectionString))
                {
                    // 调用存储过程获取检查类型
                    using (SqlCommand cmd = new SqlCommand("sp_GetCategories", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@System", system);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Dictionary<string, string>> categories = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                categories.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"].ToString() },
                                    { "name", reader["name"].ToString() }
                                });
                            }

                            // 如果没有数据，返回默认检查类型
                            if (categories.Count == 0)
                            {
                                return "{\"success\": true, \"data\": [{\"code\": \"CT\", \"name\": \"CT\"}, {\"code\": \"CT(新)\", \"name\": \"CT(新)\"}, {\"code\": \"MRI\", \"name\": \"核磁共振\"}, {\"code\": \"钼靶\", \"name\": \"钼靶\"}, {\"code\": \"普放\", \"name\": \"普放\"}, {\"code\": \"普放(新)\", \"name\": \"普放(新)\"}, {\"code\": \"消化道造影\", \"name\": \"消化道造影\"}, {\"code\": \"消化道造影(新)\", \"name\": \"消化道造影(新)\"}, {\"code\": \"超声\", \"name\": \"超声\"}, {\"code\": \"介入超声\", \"name\": \"介入超声\"}, {\"code\": \"胃镜(老)\", \"name\": \"胃镜(老)\"}, {\"code\": \"胃镜(新)\", \"name\": \"胃镜(新)\"}, {\"code\": \"肠镜(老)\", \"name\": \"肠镜(老)\"}, {\"code\": \"肠镜(新)\", \"name\": \"肠镜(新)\"}, {\"code\": \"超声内镜\", \"name\": \"超声内镜\"}, {\"code\": \"消化肠镜\", \"name\": \"消化肠镜\"}, {\"code\": \"消化胃镜\", \"name\": \"消化胃镜\"}, {\"code\": \"支气管镜(新)\", \"name\": \"支气管镜(新)\"}, {\"code\": \"支气管镜(总)\", \"name\": \"支气管镜(总)\"}, {\"code\": \"脑电\", \"name\": \"脑电\"}, {\"code\": \"体检彩超\", \"name\": \"体检彩超\"}, {\"code\": \"新城体检彩超\", \"name\": \"新城体检彩超\"}]}";
                            }

                            // 转换为JSON
                            var jsonObj = new { success = true, data = categories };
                            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取检查类型失败: " + ex.Message);
                // 出错时返回默认检查类型
                return "{\"success\": true, \"data\": [{\"code\": \"CT\", \"name\": \"CT\"}, {\"code\": \"CT(新)\", \"name\": \"CT(新)\"}, {\"code\": \"MRI\", \"name\": \"核磁共振\"}, {\"code\": \"钼靶\", \"name\": \"钼靶\"}, {\"code\": \"普放\", \"name\": \"普放\"}, {\"code\": \"普放(新)\", \"name\": \"普放(新)\"}, {\"code\": \"消化道造影\", \"name\": \"消化道造影\"}, {\"code\": \"消化道造影(新)\", \"name\": \"消化道造影(新)\"}, {\"code\": \"超声\", \"name\": \"超声\"}, {\"code\": \"介入超声\", \"name\": \"介入超声\"}, {\"code\": \"胃镜(老)\", \"name\": \"胃镜(老)\"}, {\"code\": \"胃镜(新)\", \"name\": \"胃镜(新)\"}, {\"code\": \"肠镜(老)\", \"name\": \"肠镜(老)\"}, {\"code\": \"肠镜(新)\", \"name\": \"肠镜(新)\"}, {\"code\": \"超声内镜\", \"name\": \"超声内镜\"}, {\"code\": \"消化肠镜\", \"name\": \"消化肠镜\"}, {\"code\": \"消化胃镜\", \"name\": \"消化胃镜\"}, {\"code\": \"支气管镜(新)\", \"name\": \"支气管镜(新)\"}, {\"code\": \"支气管镜(总)\", \"name\": \"支气管镜(总)\"}, {\"code\": \"脑电\", \"name\": \"脑电\"}, {\"code\": \"体检彩超\", \"name\": \"体检彩超\"}, {\"code\": \"新城体检彩超\", \"name\": \"新城体检彩超\"}]}";
            }
        }

        private string GetDepartments(string system)
        {
            try
            {
                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    // 如果没有配置数据库连接，返回默认执行科室
                    return "{\"success\": true, \"data\": [{\"code\": \"消化内镜(总)\", \"name\": \"消化内镜(总)\"}, {\"code\": \"呼吸内镜科\", \"name\": \"呼吸内镜科\"}, {\"code\": \"放射科\", \"name\": \"放射科\"}, {\"code\": \"超声科\", \"name\": \"超声科\"}, {\"code\": \"神经内科\", \"name\": \"神经内科\"}, {\"code\": \"体检科\", \"name\": \"体检科\"}, {\"code\": \"其他科室\", \"name\": \"其他科室\"}]}";
                }

                using (SqlConnection conn = LoginForm.GetConnection(connectionString))
                {
                    // 从DEPT_CATEGORY_MAPPING表中获取执行科室
                    string sql = "SELECT DISTINCT DepartmentName as code, DepartmentName as name FROM DEPT_CATEGORY_MAPPING";
                    if (!string.IsNullOrEmpty(system))
                    {
                        // 如果指定了系统，关联EXAM_TASK表获取对应系统的执行科室
                        sql = "SELECT DISTINCT d.DepartmentName as code, d.DepartmentName as name FROM DEPT_CATEGORY_MAPPING d JOIN EXAM_TASK t ON d.CategoryName = t.EXAM_CATEGORY_NAME WHERE t.SYSTEM_SOURCE_NO = @System";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(system))
                        {
                            cmd.Parameters.AddWithValue("@System", system);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Dictionary<string, string>> departments = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                departments.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"].ToString() },
                                    { "name", reader["name"].ToString() }
                                });
                            }

                            // 如果没有数据，返回默认执行科室
                            if (departments.Count == 0)
                            {
                                return "{\"success\": true, \"data\": [{\"code\": \"消化内镜(总)\", \"name\": \"消化内镜(总)\"}, {\"code\": \"呼吸内镜科\", \"name\": \"呼吸内镜科\"}, {\"code\": \"放射科\", \"name\": \"放射科\"}, {\"code\": \"超声科\", \"name\": \"超声科\"}, {\"code\": \"神经内科\", \"name\": \"神经内科\"}, {\"code\": \"体检科\", \"name\": \"体检科\"}, {\"code\": \"其他科室\", \"name\": \"其他科室\"}]}";
                            }

                            // 转换为JSON
                            var jsonObj = new { success = true, data = departments };
                            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取执行科室失败: " + ex.Message);
                // 出错时返回默认执行科室
                return "{\"success\": true, \"data\": [{\"code\": \"消化内镜(总)\", \"name\": \"消化内镜(总)\"}, {\"code\": \"呼吸内镜科\", \"name\": \"呼吸内镜科\"}, {\"code\": \"放射科\", \"name\": \"放射科\"}, {\"code\": \"超声科\", \"name\": \"超声科\"}, {\"code\": \"神经内科\", \"name\": \"神经内科\"}, {\"code\": \"体检科\", \"name\": \"体检科\"}, {\"code\": \"其他科室\", \"name\": \"其他科室\"}]}";
            }
        }

        private string GetPatientTypes(string system)
        {
            try
            {
                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    // 如果没有配置数据库连接，返回默认病人类型
                    return "{\"success\": true, \"data\": [{\"code\": \"门诊\", \"name\": \"门诊\"}, {\"code\": \"住院\", \"name\": \"住院\"}, {\"code\": \"急诊\", \"name\": \"急诊\"}, {\"code\": \"体检\", \"name\": \"体检\"}, {\"code\": \"外院\", \"name\": \"外院\"}, {\"code\": \"其他\", \"name\": \"其他\"}]}";
                }

                using (SqlConnection conn = LoginForm.GetConnection(connectionString))
                {
                    // 从数据库获取病人类型
                    string sql = @"
                        SELECT DISTINCT PATIENT_TYPE as code, PATIENT_TYPE as name 
                        FROM EXAM_TASK_INFO 
                        WHERE (@System IS NULL OR @System = '' OR SYSTEM_SOURCE_NO = @System)
                        ORDER BY PATIENT_TYPE";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@System", system ?? "");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Dictionary<string, string>> patientTypes = new List<Dictionary<string, string>>();
                            while (reader.Read())
                            {
                                patientTypes.Add(new Dictionary<string, string>
                                {
                                    { "code", reader["code"].ToString() },
                                    { "name", reader["name"].ToString() }
                                });
                            }

                            // 如果没有数据，返回默认病人类型
                            if (patientTypes.Count == 0)
                            {
                                return "{\"success\": true, \"data\": [{\"code\": \"门诊\", \"name\": \"门诊\"}, {\"code\": \"住院\", \"name\": \"住院\"}, {\"code\": \"急诊\", \"name\": \"急诊\"}, {\"code\": \"体检\", \"name\": \"体检\"}, {\"code\": \"外院\", \"name\": \"外院\"}, {\"code\": \"其他\", \"name\": \"其他\"}]}";
                            }

                            // 转换为JSON
                            var jsonObj = new { success = true, data = patientTypes };
                            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("获取病人类型失败: " + ex.Message);
                // 出错时返回默认病人类型
                return "{\"success\": true, \"data\": [{\"code\": \"门诊\", \"name\": \"门诊\"}, {\"code\": \"住院\", \"name\": \"住院\"}, {\"code\": \"急诊\", \"name\": \"急诊\"}, {\"code\": \"体检\", \"name\": \"体检\"}, {\"code\": \"外院\", \"name\": \"外院\"}, {\"code\": \"其他\", \"name\": \"其他\"}]}";
            }
        }

        private string GetStatPageHtml(string statType)
        {
            // 生成单个统计页面的HTML
            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang='zh-CN'>");
            html.AppendLine("<head>");
            html.AppendLine("    <meta charset='UTF-8'>");
            html.AppendLine("    <meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            html.AppendLine("    <title>统计分析 - " + GetStatPageName(statType) + "</title>");
            html.AppendLine("    <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css' rel='stylesheet'>");
            html.AppendLine("    <link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css'>");
            html.AppendLine("    <style>");
            html.AppendLine("        body {");
            html.AppendLine("            background-color: #1e1e2f;");
            html.AppendLine("            color: #fff;");
            html.AppendLine("            min-height: 100vh;");
            html.AppendLine("        }");
            html.AppendLine("        .sidebar {");
            html.AppendLine("            background-color: #2a2a40;");
            html.AppendLine("            min-height: 100vh;");
            html.AppendLine("        }");
            html.AppendLine("        .sidebar .nav-link {");
            html.AppendLine("            color: #ccc;");
            html.AppendLine("        }");
            html.AppendLine("        .sidebar .nav-link:hover {");
            html.AppendLine("            color: #fff;");
            html.AppendLine("            background-color: #3a3a50;");
            html.AppendLine("        }");
            html.AppendLine("        .sidebar .nav-link.active {");
            html.AppendLine("            color: #fff;");
            html.AppendLine("            background-color: #4a4a60;");
            html.AppendLine("        }");
            html.AppendLine("        .main-content {");
            html.AppendLine("            padding: 20px;");
            html.AppendLine("        }");
            html.AppendLine("        .card {");
            html.AppendLine("            background-color: #2a2a40;");
            html.AppendLine("            border: none;");
            html.AppendLine("            border-radius: 8px;");
            html.AppendLine("        }");
            html.AppendLine("        .form-control {");
            html.AppendLine("            background-color: #3a3a50;");
            html.AppendLine("            border: 1px solid #4a4a60;");
            html.AppendLine("            color: #fff;");
            html.AppendLine("        }");
            html.AppendLine("        .form-control:focus {");
            html.AppendLine("            background-color: #3a3a50;");
            html.AppendLine("            border-color: #6a6a80;");
            html.AppendLine("            color: #fff;");
            html.AppendLine("            box-shadow: 0 0 0 0.2rem rgba(106, 106, 128, 0.25);");
            html.AppendLine("        }");
            html.AppendLine("        .btn-primary {");
            html.AppendLine("            background-color: #6a6a80;");
            html.AppendLine("            border-color: #6a6a80;");
            html.AppendLine("        }");
            html.AppendLine("        .btn-primary:hover {");
            html.AppendLine("            background-color: #7a7a90;");
            html.AppendLine("            border-color: #7a7a90;");
            html.AppendLine("        }");
            html.AppendLine("        .table {");
            html.AppendLine("            color: #fff;");
            html.AppendLine("        }");
            html.AppendLine("        .table th {");
            html.AppendLine("            background-color: #3a3a50;");
            html.AppendLine("        }");
            html.AppendLine("        .table tr:nth-child(even) {");
            html.AppendLine("            background-color: #2a2a40;");
            html.AppendLine("        }");
            html.AppendLine("        .table tr:nth-child(odd) {");
            html.AppendLine("            background-color: #303045;");
            html.AppendLine("        }");
            html.AppendLine("        .spinner-border {");
            html.AppendLine("            color: #6a6a80;");
            html.AppendLine("        }");
            html.AppendLine("    </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("    <div class='container-fluid'>");
            html.AppendLine("        <div class='row'>");
            html.AppendLine("            <!-- 侧边栏 -->");
            html.AppendLine("            <div class='col-md-3 sidebar'>");
            html.AppendLine("                <div class='p-4'>");
            html.AppendLine("                    <h3 class='text-center mb-4'>统计分析</h3>");
            html.AppendLine("                    <ul class='nav flex-column' id='statMenu'>");
            html.AppendLine("                        <!-- 菜单将通过JavaScript动态加载 -->");
            html.AppendLine("                    </ul>");
            html.AppendLine("                </div>");
            html.AppendLine("            </div>");
            html.AppendLine("            ");
            html.AppendLine("            <!-- 主内容区 -->");
            html.AppendLine("            <div class='col-md-9 main-content'>");
            html.AppendLine("                <div class='card mb-4'>");
            html.AppendLine("                    <div class='card-body'>");
            html.AppendLine("                        <h2 id='pageTitle'>" + GetStatPageName(statType) + "</h2>");
            html.AppendLine("                        <div class='row mb-3' id='paramsContainer'>");
            html.AppendLine("                            <div class='col-md-6'>");
            html.AppendLine("                                <label class='form-label'>日期范围</label>");
            html.AppendLine("                                <div class='input-group'>");
            html.AppendLine("                                    <input type='date' class='form-control' id='startDate'>");
            html.AppendLine("                                    <span class='input-group-text'>至</span>");
            html.AppendLine("                                    <input type='date' class='form-control' id='endDate'>");
            html.AppendLine("                                </div>");
            html.AppendLine("                            </div>");
            html.AppendLine("                            <div class='col-md-3'>");
            html.AppendLine("                                <label class='form-label'>系统</label>");
            html.AppendLine("                                <select class='form-select' id='system'>");
            html.AppendLine("                                    <option value=''>请选择系统</option>");
            html.AppendLine("                                    <option value='RIS'>RIS（放射）</option>");
            html.AppendLine("                                    <option value='UIS'>UIS（超声）</option>");
            html.AppendLine("                                    <option value='EIS'>EIS（内镜）</option>");
            html.AppendLine("                                </select>");
            html.AppendLine("                            </div>");
            html.AppendLine("                            <div class='col-md-3'>");
            html.AppendLine("                                <label class='form-label'>报告医生</label>");
            html.AppendLine("                                <input type='text' class='form-control' id='reporter' placeholder='请输入报告医生'>");
            html.AppendLine("                            </div>");
            html.AppendLine("                        </div>");
            html.AppendLine("                        <div class='row mb-3'>");
            html.AppendLine("                            <div class='col-md-3'>");
            html.AppendLine("                                <label class='form-label'>审核医生</label>");
            html.AppendLine("                                <input type='text' class='form-control' id='reviewer' placeholder='请输入审核医生'>");
            html.AppendLine("                            </div>");
            html.AppendLine("                            <div class='col-md-3'>");
            html.AppendLine("                                <label class='form-label'>技师</label>");
            html.AppendLine("                                <input type='text' class='form-control' id='technician' placeholder='请输入技师'>");
            html.AppendLine("                            </div>");
            html.AppendLine("                            <div class='col-md-3'>");
            html.AppendLine("                                <label class='form-label'>执行科室</label>");
            html.AppendLine("                                <input type='text' class='form-control' id='department' placeholder='请输入执行科室'>");
            html.AppendLine("                            </div>");
            html.AppendLine("                            <div class='col-md-3 d-flex align-items-end'>");
            html.AppendLine("                                <button class='btn btn-primary w-100' onclick='runAnalysis()'>查询统计</button>");
            html.AppendLine("                            </div>");
            html.AppendLine("                        </div>");
            html.AppendLine("                        <div id='analysisResult' class='mt-3'>");
            html.AppendLine("                            <div class='text-center'>请点击查询按钮开始统计</div>");
            html.AppendLine("                        </div>");
            html.AppendLine("                    </div>");
            html.AppendLine("                </div>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("    ");
            html.AppendLine("    <script src='https://code.jquery.com/jquery-3.6.0.min.js'></script>");
            html.AppendLine("    <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js'></script>");
            html.AppendLine("    <script>");
            html.AppendLine("        var statType = '" + statType + "';");
            html.AppendLine("        var pageConfig = null;");
            html.AppendLine("        ");
            html.AppendLine("        // 加载页面配置");
            html.AppendLine("        function loadPageConfig() {");
            html.AppendLine("            $.ajax({");
            html.AppendLine("                url: '/get-stat-page-config',");
            html.AppendLine("                type: 'POST',");
            html.AppendLine("                contentType: 'application/json',");
            html.AppendLine("                data: JSON.stringify({pageCode: statType}),");
            html.AppendLine("                success: function(response) {");
            html.AppendLine("                    try {");
            html.AppendLine("                        var resp = typeof response === 'string' ? JSON.parse(response) : response;");
            html.AppendLine("                        if (resp.success) {");
            html.AppendLine("                            pageConfig = resp.data;");
            html.AppendLine("                            $('#pageTitle').text(pageConfig.PAGE_TITLE || '" + GetStatPageName(statType) + "');");
            html.AppendLine("                        }");
            html.AppendLine("                    } catch (e) {");
            html.AppendLine("                        console.error('解析配置失败:', e);");
            html.AppendLine("                    }");
            html.AppendLine("                },");
            html.AppendLine("                error: function(xhr, status, error) {");
            html.AppendLine("                    console.error('加载配置失败:', error);");
            html.AppendLine("                }");
            html.AppendLine("            });");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        // 初始化日期范围选择器");
            html.AppendLine("        function initDateRangePicker() {");
            html.AppendLine("            var today = new Date();");
            html.AppendLine("            var firstDay = new Date(today.getFullYear(), today.getMonth(), 1);");
            html.AppendLine("            $('#startDate').val(firstDay.toISOString().split('T')[0]);");
            html.AppendLine("            $('#endDate').val(today.toISOString().split('T')[0]);");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("            var paramsHtml = '';");
            html.AppendLine("            try {");
            html.AppendLine("                var config = typeof paramsConfig === 'string' ? JSON.parse(paramsConfig) : paramsConfig;");
            html.AppendLine("                ");
            html.AppendLine("                // 日期范围选择器 - 强制显示");
            html.AppendLine("                var dateRangeLabel = config.dateRangeLabel || '日期范围';");
            html.AppendLine("                paramsHtml += '<div class=\"col-md-6\">';");
            html.AppendLine("                paramsHtml += '<label class=\"form-label\">' + dateRangeLabel + '</label>';");
            html.AppendLine("                paramsHtml += '<div class=\"input-group\">';");
            html.AppendLine("                paramsHtml += '<input type=\"date\" class=\"form-control\" id=\"startDate\">';");
            html.AppendLine("                paramsHtml += '<span class=\"input-group-text\">至</span>';");
            html.AppendLine("                paramsHtml += '<input type=\"date\" class=\"form-control\" id=\"endDate\">';");
            html.AppendLine("                paramsHtml += '</div>';");
            html.AppendLine("                paramsHtml += '</div>';");
            html.AppendLine("                ");
            html.AppendLine("                // 时间范围选择器");
            html.AppendLine("                if (config.timeRange) {");
            html.AppendLine("                    var timeRangeLabel = config.timeRangeLabel || '时间范围';");
            html.AppendLine("                    paramsHtml += '<div class=\"col-md-4\">';");
            html.AppendLine("                    paramsHtml += '<label class=\"form-label\">' + timeRangeLabel + '</label>';");
            html.AppendLine("                    paramsHtml += '<select class=\"form-select\" id=\"timeRange\">';");
            html.AppendLine("                    paramsHtml += '<option value=\"today\">今天</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"week\">本周</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"month\">本月</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"year\">本年</option>';");
            html.AppendLine("                    paramsHtml += '</select>';");
            html.AppendLine("                    paramsHtml += '</div>';");
            html.AppendLine("                }");
            html.AppendLine("                ");
            html.AppendLine("                // 科室选择器");
            html.AppendLine("                if (config.department) {");
            html.AppendLine("                    var deptLabel = config.departmentLabel || '执行科室';");
            html.AppendLine("                    paramsHtml += '<div class=\"col-md-4\">';");
            html.AppendLine("                    paramsHtml += '<label class=\"form-label\">' + deptLabel + '</label>';");
            html.AppendLine("                    paramsHtml += '<select class=\"form-select\" id=\"department\">';");
            html.AppendLine("                    paramsHtml += '<option value=\"\">全部科室</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"消化内镜(总)\">消化内镜(总)</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"呼吸内镜科\">呼吸内镜科</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"放射科\">放射科</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"超声科\">超声科</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"神经内科\">神经内科</option>';");
            html.AppendLine("                    paramsHtml += '<option value=\"体检科\">体检科</option>';");
            html.AppendLine("                    paramsHtml += '</select>';");
            html.AppendLine("                    paramsHtml += '</div>';");
            html.AppendLine("                }");
            html.AppendLine("                ");
            html.AppendLine("                // 检查类别选择器");
            html.AppendLine("                if (config.category) {");
            html.AppendLine("                    var catLabel = config.categoryLabel || '检查类别';");
            html.AppendLine("                    paramsHtml += '<div class=\"col-md-4\">';");
            html.AppendLine("                    paramsHtml += '<label class=\"form-label\">' + catLabel + '</label>';");
            html.AppendLine("                    paramsHtml += '<input type=\"text\" class=\"form-control\" id=\"category\" placeholder=\"输入检查类别\">';");
            html.AppendLine("                    paramsHtml += '</div>';");
            html.AppendLine("                }");
            html.AppendLine("            } catch (e) {");
            html.AppendLine("                console.error('解析参数配置失败:', e);");
            html.AppendLine("                // 默认显示日期范围选择器");
            html.AppendLine("                paramsHtml += '<div class=\"col-md-6\">';");
            html.AppendLine("                paramsHtml += '<label class=\"form-label\">日期范围</label>';");
            html.AppendLine("                paramsHtml += '<div class=\"input-group\">';");
            html.AppendLine("                paramsHtml += '<input type=\"date\" class=\"form-control\" id=\"startDate\">';");
            html.AppendLine("                paramsHtml += '<span class=\"input-group-text\">至</span>';");
            html.AppendLine("                paramsHtml += '<input type=\"date\" class=\"form-control\" id=\"endDate\">';");
            html.AppendLine("                paramsHtml += '</div>';");
            html.AppendLine("                paramsHtml += '</div>';");
            html.AppendLine("            }");
            html.AppendLine("            ");
            html.AppendLine("            $('#paramsContainer').html(paramsHtml);");
            html.AppendLine("            ");
            html.AppendLine("            // 设置默认日期范围（本月）");
            html.AppendLine("            var today = new Date();");
            html.AppendLine("            var firstDay = new Date(today.getFullYear(), today.getMonth(), 1);");
            html.AppendLine("            $('#startDate').val(firstDay.toISOString().split('T')[0]);");
            html.AppendLine("            $('#endDate').val(today.toISOString().split('T')[0]);");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        // 加载统计菜单");
            html.AppendLine("        function loadStatMenu() {");
            html.AppendLine("            $.ajax({");
            html.AppendLine("                url: '/get-stat-menu',");
            html.AppendLine("                type: 'GET',");
            html.AppendLine("                success: function(response) {");
            html.AppendLine("                    var data = JSON.parse(response);");
            html.AppendLine("                    if (data.success) {");
            html.AppendLine("                        var menuHtml = '';");
            html.AppendLine("                        data.data.forEach(function(item) {");
            html.AppendLine("                            var activeClass = item.MENU_CODE === statType ? 'active' : '';");
            html.AppendLine("                            menuHtml += '<li class=\"nav-item\">' +");
            html.AppendLine("                                '<a class=\"nav-link ' + activeClass + '\" href=\"/stats/' + item.MENU_CODE + '\">' +");
            html.AppendLine("                                '<i class=\"bi ' + item.ICON_CLASS + '\"></i> ' + item.MENU_NAME +");
            html.AppendLine("                                '</a>' +");
            html.AppendLine("                                '</li>';");
            html.AppendLine("                        });");
            html.AppendLine("                        $('#statMenu').html(menuHtml);");
            html.AppendLine("                    }");
            html.AppendLine("                }");
            html.AppendLine("            });");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        // 运行分析");
            html.AppendLine("        function runAnalysis() {");
            html.AppendLine("            var params = {};");
            html.AppendLine("            ");
            html.AppendLine("            // 收集所有表单参数");
            html.AppendLine("            if ($('#startDate').length > 0 && $('#endDate').length > 0) {");
            html.AppendLine("                params.startDate = $('#startDate').val();");
            html.AppendLine("                params.endDate = $('#endDate').val();");
            html.AppendLine("            }");
            html.AppendLine("            if ($('#system').length > 0) {");
            html.AppendLine("                params.system = $('#system').val();");
            html.AppendLine("            }");
            html.AppendLine("            if ($('#reporter').length > 0) {");
            html.AppendLine("                params.reporter = $('#reporter').val();");
            html.AppendLine("            }");
            html.AppendLine("            if ($('#reviewer').length > 0) {");
            html.AppendLine("                params.reviewer = $('#reviewer').val();");
            html.AppendLine("            }");
            html.AppendLine("            if ($('#technician').length > 0) {");
            html.AppendLine("                params.technician = $('#technician').val();");
            html.AppendLine("            }");
            html.AppendLine("            if ($('#department').length > 0) {");
            html.AppendLine("                params.department = $('#department').val();");
            html.AppendLine("            }");
            html.AppendLine("            ");
            html.AppendLine("            $('#analysisResult').html('<div class=\"text-center\"><span class=\"spinner-border spinner-border-sm\"></span> 加载中...</div>');");
            html.AppendLine("            $.ajax({");
            html.AppendLine("                url: '/daily-analysis',");
            html.AppendLine("                type: 'POST',");
            html.AppendLine("                contentType: 'application/json',");
            html.AppendLine("                data: JSON.stringify(params),");
            html.AppendLine("                success: function(response) {");
            html.AppendLine("                    try {");
            html.AppendLine("                        var resp = typeof response === 'string' ? JSON.parse(response) : response;");
            html.AppendLine("                        if (resp.success) {");
            html.AppendLine("                            displayAnalysisResult(resp.data);");
            html.AppendLine("                        } else {");
            html.AppendLine("                            $('#analysisResult').html('<div class=\"alert alert-danger\">加载失败: ' + (resp.error || '未知错误') + '</div>');");
            html.AppendLine("                        }");
            html.AppendLine("                    } catch (e) {");
            html.AppendLine("                        $('#analysisResult').html('<div class=\"alert alert-danger\">解析数据时发生错误</div>');");
            html.AppendLine("                    }");
            html.AppendLine("                },");
            html.AppendLine("                error: function(xhr, status, error) {");
            html.AppendLine("                    $('#analysisResult').html('<div class=\"alert alert-danger\">加载失败: ' + error + '</div>');");
            html.AppendLine("                }");
            html.AppendLine("            });");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        // 显示分析结果");
            html.AppendLine("        function displayAnalysisResult(data) {");
            html.AppendLine("            if (data.length === 0) {");
            html.AppendLine("                $('#analysisResult').html('<div class=\"text-center\">暂无数据</div>');");
            html.AppendLine("                return;");
            html.AppendLine("            }");
            html.AppendLine("            ");
            html.AppendLine("            var html = '<table class=\"table table-bordered\">';");
            html.AppendLine("            ");
            html.AppendLine("            // 表头");
            html.AppendLine("            html += '<thead><tr>';");
            html.AppendLine("            Object.keys(data[0]).forEach(function(key) {");
            html.AppendLine("                html += '<th>' + key + '</th>';");
            html.AppendLine("            });");
            html.AppendLine("            html += '</tr></thead>';");
            html.AppendLine("            ");
            html.AppendLine("            // 表体");
            html.AppendLine("            html += '<tbody>';");
            html.AppendLine("            data.forEach(function(row) {");
            html.AppendLine("                html += '<tr>';");
            html.AppendLine("                Object.values(row).forEach(function(value) {");
            html.AppendLine("                    html += '<td>' + (value || '') + '</td>';");
            html.AppendLine("                });");
            html.AppendLine("                html += '</tr>';");
            html.AppendLine("            });");
            html.AppendLine("            html += '</tbody>';");
            html.AppendLine("            html += '</table>';");
            html.AppendLine("            ");
            html.AppendLine("            $('#analysisResult').html(html);");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        // 页面加载完成后执行");
            html.AppendLine("        $(document).ready(function() {");
            html.AppendLine("            initDateRangePicker();");
            html.AppendLine("            loadPageConfig();");
            html.AppendLine("            loadStatMenu();");
            html.AppendLine("        });");
            html.AppendLine("    </script>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");
            return html.ToString();
        }

        private string GetStatPageName(string statType)
        {
            switch (statType)
            {
                case "system": return "系统信息汇总";
                case "department": return "执行科室汇总";
                case "category": return "检查类别汇总";
                case "reporter": return "报告医生工作量";
                case "reviewer": return "审核医生工作量";
                case "technician": return "技师工作量";
                case "status": return "系统状态汇总";
                case "rank": return "执行科室工作量排名";
                default: return "统计分析";
            }
        }

        private string GetStatMenuConfig()
        {
            string connectionString = GetConnectionString();
            List<Dictionary<string, object>> menuItems = new List<Dictionary<string, object>>();
            
            if (string.IsNullOrEmpty(connectionString))
            {
                // 返回默认菜单
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "系统信息汇总" }, { "MENU_CODE", "system" }, { "ICON_CLASS", "bi bi-server" } });
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "执行科室汇总" }, { "MENU_CODE", "department" }, { "ICON_CLASS", "bi bi-building" } });
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "检查类别汇总" }, { "MENU_CODE", "category" }, { "ICON_CLASS", "bi bi-list-check" } });
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "报告医生工作量" }, { "MENU_CODE", "reporter" }, { "ICON_CLASS", "bi bi-person" } });
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "审核医生工作量" }, { "MENU_CODE", "reviewer" }, { "ICON_CLASS", "bi bi-person-check" } });
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "技师工作量" }, { "MENU_CODE", "technician" }, { "ICON_CLASS", "bi bi-tools" } });
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "系统状态汇总" }, { "MENU_CODE", "status" }, { "ICON_CLASS", "bi bi-bar-chart" } });
                menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "执行科室工作量排名" }, { "MENU_CODE", "rank" }, { "ICON_CLASS", "bi bi-trophy" } });
            }
            else
            {
                SqlConnection conn = null;
                try
                {
                    conn = LoginForm.GetConnection(connectionString);
                    string sql = "SELECT MENU_NAME, MENU_CODE, ICON_CLASS FROM STAT_MENU_CONFIG WHERE IS_ACTIVE = 1 ORDER BY MENU_ORDER";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> item = new Dictionary<string, object>();
                                item["MENU_NAME"] = reader["MENU_NAME"];
                                item["MENU_CODE"] = reader["MENU_CODE"];
                                item["ICON_CLASS"] = reader["ICON_CLASS"];
                                menuItems.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("获取菜单配置失败: " + ex.Message);
                    // 失败时返回默认菜单
                    menuItems.Add(new Dictionary<string, object> { { "MENU_NAME", "系统信息汇总" }, { "MENU_CODE", "system" }, { "ICON_CLASS", "bi bi-server" } });
                }
            }
            
            return string.Format("{{\"success\": true, \"data\": {0}}}", JsonConvert.SerializeObject(menuItems));
        }

        private string GetStatPageConfig(string pageCode)
        {
            string connectionString = GetConnectionString();
            Dictionary<string, object> config = new Dictionary<string, object>();
            
            if (string.IsNullOrEmpty(connectionString))
            {
                // 返回默认配置
                config["PAGE_NAME"] = GetStatPageName(pageCode);
                config["PAGE_TITLE"] = GetStatPageName(pageCode);
                config["QUERY_TYPE"] = pageCode;
                config["PARAMS_CONFIG"] = "{\"dateRange\": true}";
                config["DISPLAY_CONFIG"] = "{\"table\": true, \"chart\": true, \"export\": true}";
                config["QUERY_SQL"] = GetDefaultQuerySql(pageCode);
            }
            else
            {
                SqlConnection conn = null;
                try
                {
                    conn = LoginForm.GetConnection(connectionString);
                    
                    // 首先从STAT_PAGE_CONFIG获取页面配置
                    string pageSql = "SELECT PAGE_NAME, PAGE_TITLE, QUERY_TYPE, PARAMS_CONFIG, DISPLAY_CONFIG FROM STAT_PAGE_CONFIG WHERE PAGE_CODE = @pageCode AND IS_ACTIVE = 1";
                    string querySql = "";
                    
                    using (SqlCommand cmd = new SqlCommand(pageSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@pageCode", pageCode);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                config["PAGE_NAME"] = reader["PAGE_NAME"];
                                config["PAGE_TITLE"] = reader["PAGE_TITLE"];
                                config["QUERY_TYPE"] = reader["QUERY_TYPE"];
                                config["PARAMS_CONFIG"] = reader["PARAMS_CONFIG"];
                                config["DISPLAY_CONFIG"] = reader["DISPLAY_CONFIG"];
                            }
                            else
                            {
                                // 没有找到配置，返回默认配置
                                config["PAGE_NAME"] = GetStatPageName(pageCode);
                                config["PAGE_TITLE"] = GetStatPageName(pageCode);
                                config["QUERY_TYPE"] = pageCode;
                                config["PARAMS_CONFIG"] = "{\"dateRange\": true}";
                                config["DISPLAY_CONFIG"] = "{\"table\": true, \"chart\": true, \"export\": true}";
                            }
                        }
                    }
                    
                    // 从DAILY_QUERY_CONFIG获取SQL语句
                    string queryType = config.ContainsKey("QUERY_TYPE") ? config["QUERY_TYPE"].ToString() : pageCode;
                    string sqlSql = "SELECT QUERY_SQL FROM DAILY_QUERY_CONFIG WHERE QUERY_TYPE = @queryType AND IS_ACTIVE = 1";
                    using (SqlCommand cmd = new SqlCommand(sqlSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@queryType", queryType);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            config["QUERY_SQL"] = result.ToString();
                        }
                        else
                        {
                            config["QUERY_SQL"] = GetDefaultQuerySql(queryType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("获取页面配置失败: " + ex.Message);
                    // 失败时返回默认配置
                    config["PAGE_NAME"] = GetStatPageName(pageCode);
                    config["PAGE_TITLE"] = GetStatPageName(pageCode);
                    config["QUERY_TYPE"] = pageCode;
                    config["PARAMS_CONFIG"] = "{\"dateRange\": true}";
                    config["DISPLAY_CONFIG"] = "{\"table\": true, \"chart\": true, \"export\": true}";
                    config["QUERY_SQL"] = GetDefaultQuerySql(pageCode);
                }
            }
            
            return string.Format("{{\"success\": true, \"data\": {0}}}", JsonConvert.SerializeObject(config));
        }

        private string ConvertUsersToJson(List<Dictionary<string, object>> users)
        {
            return string.Format("{{\"success\": true, \"data\": {0}}}", JsonConvert.SerializeObject(users));
        }

        private string GetMockUsersJson()
        {
            // 返回模拟用户数据
            List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
            
            // 添加静态用户
            Dictionary<string, object> user1 = new Dictionary<string, object>();
            user1["id"] = 1;
            user1["username"] = "lhbdb";
            user1["role"] = "管理员";
            user1["status"] = "启用";
            users.Add(user1);
            
            Dictionary<string, object> user2 = new Dictionary<string, object>();
            user2["id"] = 2;
            user2["username"] = "testuser";
            user2["role"] = "普通用户";
            user2["status"] = "启用";
            users.Add(user2);
            
            // 添加内存中的用户（通过添加用户功能添加的）
            lock (_usersLock)
            {
                foreach (var mockUser in _mockUsers)
                {
                    users.Add(mockUser);
                }
            }
            
            return string.Format("{{\"success\": true, \"data\": {0}}}", JsonConvert.SerializeObject(users));
        }

        private bool IsPortInUse(int port)
        {
            try
            {
                System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, port);
                listener.Start();
                listener.Stop();
                return false;
            }
            catch
            {
                return true;
            }
        }

        private int FindAvailablePort(int preferredPort)
        {
            // 首先尝试首选端口附近的端口
            int[] portRanges = {
                preferredPort,  // 首选端口
                preferredPort + 1, preferredPort + 2, preferredPort + 3, preferredPort + 4, preferredPort + 5,
                preferredPort - 1, preferredPort - 2, preferredPort - 3, preferredPort - 4, preferredPort - 5
            };
            
            foreach (int port in portRanges)
            {
                if (port >= 1024 && port <= 65535 && !IsPortInUse(port))
                {
                    return port;
                }
            }
            
            // 如果附近端口都被占用，从常用端口范围中查找
            int[] commonPorts = { 8080, 8081, 8082, 8083, 8084, 8085, 
                                  9090, 9091, 9092, 9093, 9094, 9095,
                                  60000, 61000, 62000, 63000, 64000, 65000 };
            
            foreach (int port in commonPorts)
            {
                if (!IsPortInUse(port))
                {
                    return port;
                }
            }
            
            // 如果常用端口也被占用，随机查找一个可用端口
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                int port = random.Next(1024, 65535);
                if (!IsPortInUse(port))
                {
                    return port;
                }
            }
            
            // 无法找到可用端口
            return 0;
        }

        private void KillProcessUsingPort(int port)
        {
            try
            {
                string output = RunCommand("netstat", string.Format("-ano | findstr :{0}", port));
                if (!string.IsNullOrEmpty(output))
                {
                    string[] lines = output.Split('\n');
                    foreach (string line in lines)
                    {
                        if (line.Contains(":" + port))
                        {
                            string[] parts = line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length >= 5)
                            {
                                string pid = parts[4];
                                RunCommand("taskkill", string.Format("/F /PID {0}", pid));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel(string.Format("杀掉占用端口的进程失败: {0}", ex.Message));
            }
        }

        private string RunCommand(string command, string arguments)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string GetLocalIP()
        {
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        private void UpdateStatusLabel(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateStatusLabel), message);
            }
            else
            {
                lblStatus.Text = message;
            }
        }

        private void InitializeTrayIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Text = "统计系统";
            notifyIcon.Visible = true;

            // 添加鼠标点击事件处理程序
            notifyIcon.MouseClick += new MouseEventHandler(NotifyIcon_MouseClick);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem showMenuItem = new ToolStripMenuItem("显示");
            showMenuItem.Click += new EventHandler(ShowMenuItem_Click);
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("退出");
            exitMenuItem.Click += new EventHandler(ExitMenuItem_Click);
            contextMenu.Items.Add(showMenuItem);
            contextMenu.Items.Add(exitMenuItem);

            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // 当用户点击托盘图标时，显示主窗体
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }
        }

        private void ShowMenuItem_Click(object sender, EventArgs e)
        {
            // 当用户从上下文菜单中选择"显示"时，显示主窗体
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopHttpServer();
            if (notifyIcon != null)
            {
                notifyIcon.Dispose();
            }
        }

        private void StopHttpServer()
        {
            try
            {
                if (httpListener != null && httpListener.IsListening)
                {
                    httpListener.Stop();
                    httpListener.Close();
                }

                if (httpServerThread != null && httpServerThread.IsAlive)
                {
                    httpServerThread.Abort();
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel(string.Format("停止HTTP服务器失败: {0}", ex.Message));
            }
        }

        private string GetCurrentPort()
        {
            try
            {
                // 使用与 LoadServerConfig 相同的逻辑读取配置文件
                string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
                string configFile = Path.Combine(exeDir, "server_config.dat");
                
                if (!File.Exists(configFile))
                {
                    string rootConfigFile = Path.Combine(exeDir, "..", "..", "..", "server_config.dat");
                    rootConfigFile = Path.GetFullPath(rootConfigFile);
                    if (File.Exists(rootConfigFile))
                    {
                        configFile = rootConfigFile;
                    }
                }
                
                if (!File.Exists(configFile))
                {
                    string cwdConfigFile = Path.Combine(Environment.CurrentDirectory, "server_config.dat");
                    if (File.Exists(cwdConfigFile))
                    {
                        configFile = cwdConfigFile;
                    }
                }
                
                string configPort = "9094";
                
                if (File.Exists(configFile))
                {
                    string encrypted = File.ReadAllText(configFile).Trim();
                    string decrypted = Decrypt(encrypted);
                    if (!string.IsNullOrEmpty(decrypted))
                    {
                        configPort = decrypted.Trim();
                    }
                }
                
                // 使用 JsonConvert 确保 JSON 格式正确
                var result = new { success = true, data = new { port = configPort, runningPort = serverPort } };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                LogMessage(string.Format("获取端口配置: 配置文件端口={0}, 当前运行端口={1}", configPort, serverPort));
                return json;
            }
            catch (Exception ex)
            {
                LogMessage("获取当前端口失败: " + ex.Message);
                return "{\"success\": false, \"error\": \"获取端口失败\"}";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // 最小化到系统托盘，而不是完全退出
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.Location = new System.Drawing.Point(10, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(480, 40);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "统计服务器正在启动...";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(200, 80);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 35);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 130);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnExit;
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            // 使用互斥锁确保同一时间只有一个程序实例在运行
            bool createdNew;
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, "DbProcedureCallerMutex", out createdNew))
            {
                if (!createdNew)
                {
                    // 如果已经有一个实例在运行，显示错误信息并退出
                    MessageBox.Show("程序已经在运行中，不能启动多个实例。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    // 直接启动主窗体，跳过登录界面
                    Application.Run(new MainForm());
                }
                catch (Exception ex)
                {
                    // 记录错误信息
                    string logFile = Path.Combine(Application.StartupPath, "server.log");
                    string logEntry = string.Format("[{0}] 程序启动失败: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message);
                    File.AppendAllText(logFile, logEntry + Environment.NewLine);
                    // 显示错误信息
                    MessageBox.Show(string.Format("程序启动失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}