using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace OllamaDevHelper
{
    class Program
    {
        static readonly HttpClient _httpClient = new HttpClient();
        static readonly string _baseUrl = "http://localhost:11434";

        static void Main(string[] args)
        {
            Console.WriteLine("========== Ollama 开发助手 ==========");
            Console.WriteLine("输入 'exit' 退出");
            Console.WriteLine("输入 'help' 显示帮助");
            Console.WriteLine("====================================\n");

            while (true)
            {
                Console.Write("您的问题: ");
                string input = Console.ReadLine();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    ShowHelp();
                    continue;
                }

                string response = CallOllama("llama3", input);
                Console.WriteLine("\nAI 回复:\n" + response);
                Console.WriteLine("\n" + new string('=', 50) + "\n");
            }
        }

        static string CallOllama(string model, string prompt)
        {
            try
            {
                var requestBody = new
                {
                    model = model,
                    prompt = prompt,
                    stream = false
                };

                string json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync($"{_baseUrl}/api/generate", content).Result;
                response.EnsureSuccessStatusCode();

                string responseJson = response.Content.ReadAsStringAsync().Result;
                dynamic result = JsonConvert.DeserializeObject(responseJson);

                return result.response.ToString();
            }
            catch (Exception ex)
            {
                return $"调用失败: {ex.Message}\n请确保 Ollama 服务已启动!";
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("\n常用命令:");
            Console.WriteLine("  exit    - 退出程序");
            Console.WriteLine("  help    - 显示帮助");
            Console.WriteLine("\n示例问题:");
            Console.WriteLine("  帮我写一个 C# 的 HTTP 请求工具类");
            Console.WriteLine("  解释一下什么是依赖注入");
            Console.WriteLine("  如何优化 SQL 查询性能");
            Console.WriteLine();
        }
    }
}