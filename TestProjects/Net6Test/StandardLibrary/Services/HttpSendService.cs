using Newtonsoft.Json;
using StandardLibrary.IServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.Services
{
    public class HttpSendService : IHttpSendService
    {
        //protected readonly ILogger<HttpSendService> _logger;
        public HttpSendService()
        {
            
        }
        public async Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> func)
        {
            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await func.Invoke();
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception(await httpResponse.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, ex.Message);
                throw;
            }
            return httpResponse;
        }

        public async Task<T> SendAndReadFromJsonAsync<T>(Func<Task<HttpResponseMessage>> func)
        {
            var str = await SendAndReadAsStringAsync(func);
            return JsonConvert.DeserializeObject<T>(str);
        }
        public async Task<string> SendAndReadAsStringAsync(Func<Task<HttpResponseMessage>> func)
        {
            var response = await SendAsync(func);
            var rsp = await response.Content.ReadAsStringAsync();
            return rsp;
        }

        public async Task<T> SendAndReadFromJsonAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> func, HttpClient client)
        {
            var str = await SendAndReadAsStringAsync(func, client);
            return JsonConvert.DeserializeObject<T>(str);
        }
        public async Task<string> SendAndReadAsStringAsync(Func<HttpClient, Task<HttpResponseMessage>> func, HttpClient client)
        {
            var response = await SendAsync(async () => { return await func.Invoke(client); });
            var rsp = await response.Content.ReadAsStringAsync();
            return rsp;
        }
    }
}
