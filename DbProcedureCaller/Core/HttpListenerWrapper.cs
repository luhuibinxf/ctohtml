using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using DbProcedureCaller.API;

namespace DbProcedureCaller.Core
{
    public class HttpListenerWrapper
    {
        private HttpListener httpListener;
        private ApiHandler apiHandler;
        private string prefix;
        private bool isListening;

        public HttpListenerWrapper(string port, ApiHandler handler)
        {
            prefix = $"http://localhost:{port}/";
            apiHandler = handler;
            isListening = false;
        }

        public void StartListening()
        {
            try
            {
                httpListener = new HttpListener();
                httpListener.Prefixes.Add(prefix);
                httpListener.Start();
                isListening = true;
                
                LogHelper.LogInfo($"HTTP服务器启动成功，监听地址: {prefix}");

                while (isListening)
                {
                    try
                    {
                        IAsyncResult result = httpListener.BeginGetContext(new AsyncCallback(ProcessRequest), httpListener);
                        result.AsyncWaitHandle.WaitOne();
                    }
                    catch (HttpListenerException)
                    {
                        if (isListening)
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
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "启动HTTP监听器失败");
                throw;
            }
        }

        public void StopListening()
        {
            isListening = false;
            if (httpListener != null)
            {
                try
                {
                    httpListener.Stop();
                    httpListener.Close();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException(ex, "停止HTTP监听器失败");
                }
            }
            LogHelper.LogInfo("HTTP服务器已停止");
        }

        private void ProcessRequest(IAsyncResult result)
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

        private void HandleRequest(object state)
        {
            HttpListenerContext context = null;
            HttpListenerResponse response = null;

            try
            {
                context = (HttpListenerContext)state;
                HttpListenerRequest request = context.Request;
                response = context.Response;

                string url = request.Url.PathAndQuery;
                string method = request.HttpMethod;
                string absolutePath = request.Url.AbsolutePath;

                LogHelper.LogInfo($"=====DEBUG_START===== Method:{method}, Url:{url}, AbsolutePath:{absolutePath}");

                if (absolutePath == "/test-api" && method == "POST")
                {
                    byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes("{\"success\":true,\"message\":\"Test API works!\"}");
                    response.ContentType = "application/json; charset=utf-8";
                    response.StatusCode = 200;
                    response.ContentLength64 = responseBytes.Length;
                    response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                    response.Close();
                    return;
                }

                if (absolutePath == "/search-stored-proc" && method == "POST")
                {
                    LogHelper.LogInfo("=====Handling search-stored-proc=====");
                    byte[] responseBytes = apiHandler.HandleRequest(url, method, request.InputStream);
                    response.ContentType = "application/json; charset=utf-8";
                    response.StatusCode = 200;
                    response.ContentLength64 = responseBytes.Length;
                    response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                    response.Close();
                    return;
                }

                if ((absolutePath == "/" || absolutePath == "/login") && method == "GET")
                {
                    string loginHtml = @"<!DOCTYPE html>
<html lang=""zh-CN"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>登录 - 统计分析系统</title>
    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"" rel=""stylesheet"">
    <style>
        body { width: 100vw; height: 100vh; background: linear-gradient(135deg, #0f172a 0%, #1e293b 50%, #0f172a 100%); display: flex; align-items: center; justify-content: center; font-family: 'Microsoft YaHei', sans-serif; }
        .login-card { background: linear-gradient(145deg, #1e293b, #263348); border-radius: 24px; padding: 2.5rem; width: 90%; max-width: 420px; }
        .login-header { text-align: center; margin-bottom: 2rem; }
        .login-header h2 { color: white; font-weight: 700; }
        .form-control { background: rgba(15,23,42,0.6); border: 1px solid rgba(255,255,255,0.1); color: white; }
        .btn-login { background: linear-gradient(135deg, #3b82f6, #2563eb); width: 100%; height: 50px; font-weight: 700; }
    </style>
</head>
<body>
    <div class=""login-card"">
        <div class=""login-header"">
            <div style=""font-size: 3rem; margin-bottom: 1rem;"">🔐</div>
            <h2>统计分析系统</h2>
        </div>
        <div class=""mb-3""><label class=""text-white mb-2"">用户名</label><input type=""text"" class=""form-control"" id=""username"" placeholder=""请输入用户名""></div>
        <div class=""mb-3""><label class=""text-white mb-2"">密码</label><input type=""password"" class=""form-control"" id=""password"" placeholder=""请输入密码""></div>
        <button class=""btn btn-login"" onclick=""doLogin()"">登录</button>
        <div id=""errorMsg"" class=""mt-3 text-center text-danger"" style=""display:none;""></div>
    </div>
    <script src=""https://code.jquery.com/jquery-3.6.0.min.js""></script>
    <script>
        function doLogin() {
            $.ajax({
                url: '/login', type: 'POST', contentType: 'application/json',
                data: JSON.stringify({username: $('#username').val(), password: $('#password').val()}),
                success: function(r) { if(r.success) { sessionStorage.setItem('user', $('#username').val()); window.location.href='/index'; } else $('#errorMsg').text('用户名或密码错误').show(); },
                error: function() { $('#errorMsg').text('连接失败').show(); }
            });
        }
        $('#password').keypress(function(e) { if(e.which==13) doLogin(); });
    </script>
</body>
</html>";
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(loginHtml);
                    response.ContentType = "text/html; charset=utf-8";
                    response.StatusCode = 200;
                    response.ContentLength64 = bytes.Length;
                    response.OutputStream.Write(bytes, 0, bytes.Length);
                    response.Close();
                    return;
                }

                if (absolutePath.EndsWith(".html"))
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
                else if (absolutePath == "/search-stored-proc" || 
                         absolutePath == "/get-proc-metadata" || 
                         absolutePath == "/execute-stored-proc" || 
                         absolutePath == "/save-proc-config" || 
                         absolutePath == "/get-proc-configs" || 
                         absolutePath.StartsWith("/delete-proc-config"))
                {
                    byte[] responseBytes = apiHandler.HandleRequest(url, method, request.InputStream);
                    response.ContentType = "application/json; charset=utf-8";
                    response.StatusCode = 200;
                    response.ContentLength64 = responseBytes.Length;
                    response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                }
                else if (IsJsonEndpoint(absolutePath))
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
                SendErrorResponse(response, "服务暂时不可用");
            }
            catch (IOException ex)
            {
                LogHelper.LogException(ex, "IO操作异常");
                SendErrorResponse(response, "网络连接异常");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "处理HTTP请求失败");
                try
                {
                    SendErrorResponse(response, "服务器内部错误");
                }
                catch
                {
                    // 忽略发送错误响应时的异常
                }
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

        private bool IsJsonEndpoint(string url)
        {
            LogHelper.LogInfo($"=====IsJsonEndpoint called with url: {url}");
            if (url.EndsWith(".html")) 
            {
                LogHelper.LogInfo($"=====URL ends with .html, returning false");
                return false;
            }
            return url == "/call-procedure" ||
                   url == "/get-users" ||
                   url == "/get-port" ||
                   url == "/set-port" ||
                   url == "/init-db" ||
                   url == "/update-db-config" ||
                   url.StartsWith("/get-all-options") ||
                   url == "/daily-analysis" ||
                   url == "/login" ||
                   url == "/logout" ||
                   url == "/token-login" ||
                   url == "/generate-token" ||
                   url == "/get-tokens" ||
                   url == "/delete-token" ||
                   url == "/test-api" ||
                   url == "/search-stored-proc" ||
                   url == "/get-proc-metadata" ||
                   url == "/execute-stored-proc" ||
                   url == "/save-proc-config" ||
                   url == "/get-proc-configs" ||
                   url.StartsWith("/delete-proc-config");
        }

        private string GetFilePath(string url)
        {
            string templatesDir = Path.Combine(AppContext.BaseDirectory, "templates");
            LogHelper.LogInfo($"=====GetFilePath called with url: {url}");
            LogHelper.LogInfo($"模板目录: {templatesDir} - 存在: {Directory.Exists(templatesDir)}");

            string path = url;
            if (path.Contains('?'))
            {
                path = path.Substring(0, path.IndexOf('?'));
            }
            
            string filePath;
            if (path == "/")
            {
                filePath = "login.html";
                LogHelper.LogInfo($"=====Returning login.html for /");
            }
            else if (path == "/login")
            {
                filePath = "login.html";
                LogHelper.LogInfo($"=====Returning login.html for /login");
            }
            else if (path == "/index")
            {
                filePath = "index.html";
            }
            else
            {
                filePath = path.TrimStart('/');
                if (!filePath.Contains('.'))
                {
                    filePath += ".html";
                }
            }

            string fullPath = Path.Combine(templatesDir, filePath);
            LogHelper.LogInfo($"请求: {url} -> 文件路径: {fullPath} -> 存在: {File.Exists(fullPath)}");
            
            return fullPath;
        }

        private void SendJsonResponse(HttpListenerResponse response, string json)
        {
            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = 200;
            
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void SendFileResponse(HttpListenerResponse response, string filePath)
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

        private void SendNotFoundResponse(HttpListenerResponse response)
        {
            response.StatusCode = 404;
            string content = "<html><body><h1>404 - 页面未找到</h1></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void SendErrorResponse(HttpListenerResponse response, string message)
        {
            response.StatusCode = 500;
            string content = $"<html><body><h1>服务器错误</h1><p>{message}</p></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }
    }
}