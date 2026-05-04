using System;
using System.Diagnostics;
using System.Threading;
using System.Net;

class AutoTest
{
    static void Main()
    {
        Console.WriteLine("=== 8080端口自动测试 ===");

        // 清理旧进程
        Console.WriteLine("\n[1] 清理旧进程...");
        try
        {
            Process[] processes = Process.GetProcessesByName("DbProcedureCaller");
            foreach (Process p in processes)
            {
                p.Kill();
                Console.WriteLine("已关闭旧进程: " + p.Id);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("清理进程时出错: " + ex.Message);
        }

        // 启动程序
        Console.WriteLine("\n[2] 启动程序...");
        ProcessStartInfo psi = new ProcessStartInfo("d:\\AI\\tran\\DbProcedureCaller.exe");
        psi.UseShellExecute = false;
        psi.CreateNoWindow = false;
        Process programProcess = null;

        try
        {
            programProcess = Process.Start(psi);
            Console.WriteLine("程序已启动, PID: " + programProcess.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("启动程序失败: " + ex.Message);
            return;
        }

        // 等待程序初始化
        Console.WriteLine("\n[3] 等待程序初始化...");
        Thread.Sleep(15000); // 给足够时间完成登录和服务器启动

        // 测试8080端口
        Console.WriteLine("\n[4] 测试8080端口...");
        bool portAccessible = false;
        string errorMessage = "";

        for (int i = 0; i < 3; i++)
        {
            try
            {
                Console.WriteLine("尝试访问 http://localhost:8080/ (第" + (i + 1) + "次)...");
                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/");
                request.Timeout = 5000;
                request.UserAgent = "AutoTest";
                
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("✅ 访问成功!");
                    Console.WriteLine("   状态码: " + (int)response.StatusCode + " " + response.StatusCode);
                    Console.WriteLine("   内容类型: " + response.ContentType);
                    
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        string content = reader.ReadToEnd();
                        Console.WriteLine("   内容长度: " + content.Length + " 字符");
                        if (content.Length > 0)
                        {
                            Console.WriteLine("   内容预览: " + content.Substring(0, Math.Min(200, content.Length)));
                        }
                    }
                    portAccessible = true;
                    break;
                }
            }
            catch (WebException ex)
            {
                errorMessage = ex.Message;
                if (ex.Response != null)
                {
                    HttpWebResponse errorResponse = (HttpWebResponse)ex.Response;
                    errorMessage += " (状态码: " + (int)errorResponse.StatusCode + ")";
                }
                Console.WriteLine("❌ 访问失败: " + errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                Console.WriteLine("❌ 访问失败: " + errorMessage);
            }
            
            if (i < 2)
            {
                Console.WriteLine("等待3秒后重试...");
                Thread.Sleep(3000);
            }
        }

        // 检查进程状态
        Console.WriteLine("\n[5] 检查程序状态...");
        if (programProcess != null && !programProcess.HasExited)
        {
            Console.WriteLine("程序仍在运行 (PID: " + programProcess.Id + ")");
        }
        else
        {
            Console.WriteLine("程序已退出");
        }

        // 检查端口监听
        Console.WriteLine("\n[6] 检查8080端口监听...");
        try
        {
            Process netstat = Process.Start(new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = "-an",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            });
            string output = netstat.StandardOutput.ReadToEnd();
            netstat.WaitForExit();
            
            if (output.Contains(":8080"))
            {
                Console.WriteLine("✅ 8080端口正在监听");
                string[] lines = output.Split('\n');
                foreach (string line in lines)
                {
                    if (line.Contains(":8080"))
                    {
                        Console.WriteLine("   " + line.Trim());
                    }
                }
            }
            else
            {
                Console.WriteLine("❌ 8080端口未被监听");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("检查端口时出错: " + ex.Message);
        }

        // 清理
        Console.WriteLine("\n[7] 清理...");
        try
        {
            if (programProcess != null && !programProcess.HasExited)
            {
                programProcess.Kill();
                Console.WriteLine("已关闭程序");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("关闭程序时出错: " + ex.Message);
        }

        // 总结
        Console.WriteLine("\n=== 测试结果 ===");
        if (portAccessible)
        {
            Console.WriteLine("✅ 8080端口可以正常访问");
        }
        else
        {
            Console.WriteLine("❌ 8080端口无法访问");
            Console.WriteLine("   错误: " + errorMessage);
            Console.WriteLine("   可能原因: 1. 程序需要管理员权限 2. 数据库配置缺失 3. 端口被占用");
        }

        Console.WriteLine("\n=== 测试完成 ===");
        Console.WriteLine("按任意键退出...");
        Console.ReadKey();
    }
}