using System;
using System.Net;
using System.Text;
using System.Threading;

class TestHttpServer
{
    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Prefixes.Add("http://127.0.0.1:8080/");
        
        try
        {
            listener.Start();
            Console.WriteLine("服务器已启动，监听8080端口...");
            
            while (true)
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("错误: " + ex.Message);
        }
        finally
        {
            listener.Stop();
        }
    }
}