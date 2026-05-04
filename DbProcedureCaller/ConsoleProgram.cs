using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using DbProcedureCaller.API;
using DbProcedureCaller.Core;

namespace DbProcedureCaller
{
    class ConsoleProgram
    {
        private static HttpListener httpListener;
        private static Thread httpServerThread;
        private static ApiHandler apiHandler;
        private static string serverPort;
        private static bool isRunning;
        private static int restartCount = 0;
        private static readonly int maxRestartCount = 5;
        private static readonly TimeSpan restartDelay = TimeSpan.FromSeconds(30);

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            bool createdNew;
            using (Mutex mutex = new Mutex(true, "DbProcedureCallerModularMutex", out createdNew))
            {
                if (!createdNew)
                {
                    Console.WriteLine("错误: 程序已经在运行中，不能启动多个实例。");
                    Console.WriteLine("按任意键退出...");
                    Console.ReadKey();
                    return;
                }

                try
                {
                    Console.WriteLine("========================================");
                    Console.WriteLine("      统计分析系统 - 服务端程序");
                    Console.WriteLine("========================================");
                    
                    LogHelper.Init();
                    apiHandler = new ApiHandler();
                    LoadServerConfig();
                    
                    StartServer(serverPort);
                    
                    Console.WriteLine($"服务已启动，端口: {serverPort}");
                    Console.WriteLine($"本地访问: http://localhost:{serverPort}/");
                    Console.WriteLine("按任意键停止服务...");
                    
                    var exitEvent = new ManualResetEvent(false);
                    
                    var heartbeatThread = new Thread(() =>
                    {
                        int count = 0;
                        while (!exitEvent.WaitOne(1000))
                        {
                            try
                            {
                                count++;
                                if (count % 10 == 0)
                                {
                                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 服务运行中，端口: {serverPort}");
                                    LogHelper.LogInfo("服务健康检查 - 运行正常");
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.LogException(ex, "心跳线程异常");
                            }
                        }
                    });
                    heartbeatThread.IsBackground = true;
                    heartbeatThread.Start();
                    
                    Console.ReadKey(true);
                    exitEvent.Set();
                    
                    StopServer();
                    Console.WriteLine("服务已停止");
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "程序启动失败");
                    Console.WriteLine($"程序启动失败: {ex.Message}");
                    AttemptRestart(ex);
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            LogHelper.LogException(ex, "未处理的异常 - 程序即将崩溃");
            Console.WriteLine($"严重错误: {ex?.Message ?? "未知错误"}");
            AttemptRestart(ex);
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            LogHelper.LogInfo("程序正在退出");
            try
            {
                StopServer();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "退出时发生异常");
            }
        }

        private static void AttemptRestart(Exception ex)
        {
            if (restartCount >= maxRestartCount)
            {
                Console.WriteLine($"已达到最大重启次数 ({maxRestartCount})，程序将退出");
                LogHelper.LogInfo($"已达到最大重启次数 ({maxRestartCount})，程序将退出");
                Console.WriteLine("按任意键退出...");
                Console.ReadKey();
                Environment.Exit(1);
                return;
            }

            restartCount++;
            Console.WriteLine($"尝试重启服务 ({restartCount}/{maxRestartCount})...");
            LogHelper.LogInfo($"尝试重启服务 ({restartCount}/{maxRestartCount})，原因: {ex?.Message ?? "未知"}");

            try
            {
                StopServer();
                Thread.Sleep(restartDelay);
                StartServer(serverPort);
            }
            catch (Exception restartEx)
            {
                LogHelper.LogException(restartEx, "重启失败");
                Console.WriteLine($"重启失败: {restartEx.Message}");
                Console.WriteLine("按任意键退出...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        private static void RestartServer()
        {
            try
            {
                apiHandler = new ApiHandler();
                StartServer(serverPort);
                Console.WriteLine($"服务重启成功，端口: {serverPort}");
                LogHelper.LogInfo("服务重启成功");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "重启HTTP服务器失败");
                throw;
            }
        }

        private static void LoadServerConfig()
        {
            try
            {
                string exeDir = AppContext.BaseDirectory;
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

                LogHelper.LogInfo($"尝试加载配置文件: {configFile}");

                if (File.Exists(configFile))
                {
                    string encrypted = File.ReadAllText(configFile).Trim();
                    string decrypted = Decrypt(encrypted);
                    LogHelper.LogInfo($"解密后内容: {decrypted}");
                    if (!string.IsNullOrEmpty(decrypted))
                    {
                        serverPort = decrypted.Trim();
                        LogHelper.LogInfo($"从配置文件加载端口: {serverPort}");
                    }
                }
                else
                {
                    LogHelper.LogInfo("配置文件不存在");
                }

                if (string.IsNullOrEmpty(serverPort))
                {
                    serverPort = "8081";
                    LogHelper.LogInfo("使用默认端口: " + serverPort);
                }
            }
            catch (Exception ex)
            {
                serverPort = "8081";
                LogHelper.LogException(ex, "加载服务器配置失败，使用默认端口");
            }
        }

        public static void StartServer(string port)
        {
            serverPort = port;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            try
            {
                if (apiHandler == null)
                {
                    apiHandler = new ApiHandler();
                    LogHelper.LogInfo("ApiHandler初始化完成");
                }
                
                ApiHandler.RunningPort = serverPort;
                httpListener = new HttpListener();
                httpListener.Prefixes.Add($"http://*:{serverPort}/");
                httpListener.Start();
                isRunning = true;

                LogHelper.LogInfo("HTTP服务器启动成功");
                LogHelper.LogInfo($"HTTP服务器已启动，监听端口 {serverPort}");

                httpServerThread = new Thread(new ThreadStart(Listen));
                httpServerThread.IsBackground = true;
                httpServerThread.Start();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "启动HTTP服务器失败");
                throw;
            }
        }

        public static void StopServer()
        {
            isRunning = false;
            if (httpListener != null)
            {
                httpListener.Stop();
                httpListener.Close();
                httpListener = null;
            }
            LogHelper.LogInfo("HTTP服务器已停止");
        }

        private static void Listen()
        {
            while (isRunning)
            {
                try
                {
                    IAsyncResult result = httpListener.BeginGetContext(new AsyncCallback(ProcessRequest), httpListener);
                    result.AsyncWaitHandle.WaitOne();
                }
                catch (HttpListenerException)
                {
                    if (isRunning)
                    {
                        LogHelper.LogInfo("HTTP监听器异常");
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "HTTP请求处理异常");
                }
            }
        }

        private static void ProcessRequest(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = null;

            try
            {
                context = listener.EndGetContext(result);
            }
            catch (Exception)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(HandleRequest), context);
        }

        private static void HandleRequest(object state)
        {
            HttpListenerContext context = null;
            HttpListenerResponse response = null;

            try
            {
                context = (HttpListenerContext)state;
                HttpListenerRequest request = context.Request;
                response = context.Response;

                string url = request.Url.AbsolutePath;
                if (!string.IsNullOrEmpty(request.Url.Query))
                {
                    url += request.Url.Query;
                }
                string method = request.HttpMethod;

                LogHelper.LogInfo($"请求: {method} {url}");

                if (IsJsonEndpoint(request.Url.AbsolutePath))
                {
                    byte[] responseBytes = apiHandler.HandleRequest(url, method, request.InputStream);
                    response.ContentType = "application/json; charset=utf-8";
                    response.StatusCode = 200;
                    response.ContentLength64 = responseBytes.Length;
                    response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                }
                else
                {
                    string filePath = GetFilePath(url);
                    if (File.Exists(filePath))
                    {
                        SendFileResponse(response, filePath);
                    }
                    else
                    {
                        LogHelper.LogInfo($"文件不存在: {filePath}");
                        SendNotFoundResponse(response);
                    }
                }
            }
            catch (HttpListenerException ex)
            {
                LogHelper.LogException(ex, "HTTP监听器异常");
                SafeSendErrorResponse(response, "服务暂时不可用");
            }
            catch (IOException ex)
            {
                LogHelper.LogException(ex, "IO操作异常");
                SafeSendErrorResponse(response, "网络连接异常");
            }
            catch (NullReferenceException ex)
            {
                LogHelper.LogException(ex, "空引用异常");
                SafeSendErrorResponse(response, "服务器内部错误");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "处理HTTP请求失败");
                SafeSendErrorResponse(response, "服务器内部错误");
            }
            finally
            {
                try
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
                catch
                {
                    // 忽略关闭连接时的异常
                }
            }
        }

        private static void SafeSendErrorResponse(HttpListenerResponse response, string message)
        {
            try
            {
                if (response != null)
                {
                    response.StatusCode = 500;
                    string content = $"<html><body><h1>服务器错误</h1><p>{message}</p></body></html>";
                    byte[] buffer = Encoding.UTF8.GetBytes(content);
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                // 忽略发送错误响应时的异常
            }
        }

        private static bool IsJsonEndpoint(string url)
        {
            if (url.EndsWith(".html")) return false;
            return url == "/call-procedure" ||
                   url == "/get-users" ||
                   url.StartsWith("/get-user") ||
                   url == "/add-user" ||
                   url.StartsWith("/update-user") ||
                   url.StartsWith("/delete-user") ||
                   url == "/get-port" ||
                   url == "/set-port" ||
                   url == "/init-db" ||
                   url == "/update-db-config" ||
                   url == "/test-db-connection" ||
                   url == "/get-db-config" ||
                   url == "/get-roles" ||
                   url.StartsWith("/get-user-roles") ||
                   url == "/assign-role" ||
                   url == "/remove-role" ||
                   url == "/get-menus" ||
                   url.StartsWith("/check-permission") ||
                   url.StartsWith("/get-user-menus") ||
                   url.StartsWith("/check-analysis-permission") ||
                   url.StartsWith("/get-all-options") ||
                   url.StartsWith("/daily-analysis") ||
                   url == "/login" ||
                   url == "/logout" ||
                   url == "/token-login" ||
                   url == "/generate-token" ||
                   url == "/get-tokens" ||
                   url == "/delete-token";
        }

        private static string GetFilePath(string url)
        {
            string exeDir = AppContext.BaseDirectory;
            string templatesDir = Path.Combine(exeDir, "..", "..", "..", "templates");
            templatesDir = Path.GetFullPath(templatesDir);

            if (!Directory.Exists(templatesDir))
            {
                templatesDir = Path.Combine(exeDir, "templates");
            }

            if (!Directory.Exists(templatesDir))
            {
                templatesDir = Path.Combine(Environment.CurrentDirectory, "templates");
            }

            string filePath = url == "/" ? "index.html" : url.TrimStart('/');
            
            if (!filePath.Contains('.'))
            {
                filePath += ".html";
            }

            return Path.Combine(templatesDir, filePath);
        }

        private static void SendJsonResponse(HttpListenerResponse response, string json)
        {
            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = 200;
            
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private static void SendFileResponse(HttpListenerResponse response, string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            
            switch (extension)
            {
                case ".html":
                    response.ContentType = "text/html; charset=utf-8";
                    break;
                case ".js":
                    response.ContentType = "application/javascript; charset=utf-8";
                    break;
                case ".css":
                    response.ContentType = "text/css; charset=utf-8";
                    break;
                case ".json":
                    response.ContentType = "application/json; charset=utf-8";
                    break;
                default:
                    response.ContentType = "application/octet-stream";
                    break;
            }

            response.StatusCode = 200;
            
            using (FileStream fs = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    response.OutputStream.Write(buffer, 0, bytesRead);
                }
            }
        }

        private static void SendNotFoundResponse(HttpListenerResponse response)
        {
            response.StatusCode = 404;
            string content = "<html><body><h1>404 - 页面未找到</h1></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private static void SendErrorResponse(HttpListenerResponse response, string message)
        {
            response.StatusCode = 500;
            string content = $"<html><body><h1>服务器错误</h1><p>{message}</p></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private static string Decrypt(string input)
        {
            try
            {
                byte[] data = Convert.FromBase64String(input);
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                return input;
            }
        }
    }
}