using System;
using System.Net;
using System.Text;
using System.Threading;
using System.IO;

class SimpleHttpServer
{
    static void Main()
    {
        try
        {
            int port = 8888;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:" + port + "/");
            listener.Prefixes.Add("http://127.0.0.1:" + port + "/");
            
            Console.WriteLine("启动HTTP服务器，监听端口 " + port);
            listener.Start();
            Console.WriteLine("HTTP服务器启动成功！");
            
            Thread thread = new Thread(new ThreadStart(() => {
                while (true)
                {
                    try
                    {
                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;
                        
                        string responseString = "<html><body><h1>测试服务器</h1><p>Hello World!</p></body></html>";
                        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                        
                        response.ContentType = "text/html; charset=utf-8";
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        response.OutputStream.Close();
                        
                        Console.WriteLine("处理了一个请求: " + request.RawUrl);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("处理请求时出错: " + ex.Message);
                    }
                }
            }));
            
            thread.IsBackground = true;
            thread.Start();
            
            Console.WriteLine("服务器运行中，按任意键退出...");
            Console.ReadKey();
            
            listener.Stop();
            Console.WriteLine("服务器已停止");
        }
        catch (Exception ex)
        {
            Console.WriteLine("启动服务器失败: " + ex.Message);
        }
    }
}