using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace DbProcedureCaller.Services
{
    public class OllamaTestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OllamaTestService(string baseUrl = "http://localhost:11434")
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _baseUrl = baseUrl;
        }

        public (bool Success, string Message) TestConnection()
        {
            try
            {
                var response = _httpClient.GetAsync($"{_baseUrl}/api/tags").Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    return (true, $"连接成功！可用模型: {content}");
                }
                return (false, $"连接失败，HTTP状态码: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return (false, $"连接失败: {ex.Message}");
            }
        }

        public (bool Success, string Response) TestModel(string modelName, string prompt)
        {
            try
            {
                var requestBody = new
                {
                    model = modelName,
                    prompt = prompt,
                    stream = false
                };

                string json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync($"{_baseUrl}/api/generate", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = response.Content.ReadAsStringAsync().Result;
                    dynamic result = JsonConvert.DeserializeObject(responseJson);
                    return (true, result.response?.ToString() ?? "无响应内容");
                }
                return (false, $"调用失败，HTTP状态码: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return (false, $"调用失败: {ex.Message}");
            }
        }

        public string GetModelList()
        {
            try
            {
                var response = _httpClient.GetAsync($"{_baseUrl}/api/tags").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return $"获取失败，HTTP状态码: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                return $"获取失败: {ex.Message}";
            }
        }
    }
}