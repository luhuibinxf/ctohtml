using System;
using System.Threading;
using DbProcedureCaller.API;
using DbProcedureCaller.Core;

namespace DbProcedureCaller
{
    class Program
    {
        private static HttpListenerWrapper httpListener;
        private static Thread httpServerThread;
        private static ApiHandler apiHandler;
        private static string serverPort;

        static void Main(string[] args)
        {
            bool createdNew;
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, "DbProcedureCallerModularMutex", out createdNew))
            {
                if (!createdNew)
                {
                    Console.WriteLine("错误: 程序已经在运行中，不能启动多个实例。");
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
                    
                    StartHttpServer();
                    
                    Console.WriteLine($"服务已启动，端口: {serverPort}");
                    Console.WriteLine($"本地访问: http://localhost:{serverPort}/");
                    Console.WriteLine("按任意键停止服务...");
                    Console.ReadKey();
                    
                    StopHttpServer();
                    Console.WriteLine("服务已停止");
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "程序启动失败");
                    Console.WriteLine($"程序启动失败: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        private static void LoadServerConfig()
        {
            try
            {
                string exeDir = AppContext.BaseDirectory;
                string configFile = System.IO.Path.Combine(exeDir, "server_config.dat");

                if (!System.IO.File.Exists(configFile))
                {
                    string cwdConfigFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "server_config.dat");
                    if (System.IO.File.Exists(cwdConfigFile))
                    {
                        configFile = cwdConfigFile;
                    }
                }

                LogHelper.LogInfo($"尝试加载配置文件: {configFile}");

                if (System.IO.File.Exists(configFile))
                {
                    string encrypted = System.IO.File.ReadAllText(configFile).Trim();
                    string decrypted = Decrypt(encrypted);
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
                    serverPort = "9095";
                    LogHelper.LogInfo("使用默认端口: " + serverPort);
                }
            }
            catch (Exception ex)
            {
                serverPort = "9095";
                LogHelper.LogException(ex, "加载服务器配置失败，使用默认端口");
            }
        }

        private static void StartHttpServer()
        {
            try
            {
                httpListener = new HttpListenerWrapper(serverPort, apiHandler);
                httpServerThread = new Thread(new ThreadStart(httpListener.StartListening));
                httpServerThread.IsBackground = true;
                httpServerThread.Start();
                
                LogHelper.LogInfo("HTTP服务器启动成功");
                Console.WriteLine($"HTTP服务器已启动，监听端口 {serverPort}");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "启动HTTP服务器失败");
                throw;
            }
        }

        private static void StopHttpServer()
        {
            try
            {
                if (httpListener != null)
                {
                    httpListener.StopListening();
                }
                if (httpServerThread != null && httpServerThread.IsAlive)
                {
                    httpServerThread.Join(5000);
                }
                LogHelper.LogInfo("HTTP服务器已停止");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "停止HTTP服务器失败");
            }
        }

        private static string Decrypt(string input)
        {
            try
            {
                byte[] data = Convert.FromBase64String(input);
                return System.Text.Encoding.UTF8.GetString(data);
            }
            catch
            {
                return input;
            }
        }
    }
}