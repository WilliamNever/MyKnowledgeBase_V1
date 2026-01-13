using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StandardLibrary.Extensions;
using StandardLibrary.IServices;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StandardLibrary.Services
{
    public class HttpSendService : BaseService<HttpSendService>, IHttpSendService
    {
        public HttpSendService(ILogger<HttpSendService> logger) : base(logger)
        {

        }
        public async Task<HttpResponseMessage> SendAsync(Func<HttpContent, Task<HttpResponseMessage>> func, HttpContent sender, bool RaiseExpWhenBadResponse = true)
        {
            return await SendAsync(async () => await func.Invoke(sender), RaiseExpWhenBadResponse);
        }
        public async Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> func, bool RaiseExpWhenBadResponse = true)
        {
            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await func.Invoke();
                if (RaiseExpWhenBadResponse && !httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Call {httpResponse.RequestMessage.RequestUri.AbsoluteUri} - Get Response {httpResponse.StatusCode} - {await httpResponse.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            return httpResponse;
        }

        public async Task<T> SendAndReadFromJsonAsync<T>(Func<Task<HttpResponseMessage>> func, bool RaiseExpWhenBadResponse = true)
        {
            var str = await SendAndReadAsStringAsync(func, RaiseExpWhenBadResponse);
            return DeserializeJson<T>(str);
        }
        public async Task<string> SendAndReadAsStringAsync(Func<Task<HttpResponseMessage>> func, bool RaiseExpWhenBadResponse = true)
        {
            var response = await SendAsync(func, RaiseExpWhenBadResponse);
            var rsp = await response.Content.ReadAsStringAsync();
            return rsp;
        }

        public async Task<T> SendAndReadFromJsonAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> func, HttpClient client, bool RaiseExpWhenBadResponse = true)
        {
            var str = await SendAndReadAsStringAsync(func, client, RaiseExpWhenBadResponse);
            return DeserializeJson<T>(str);
        }
        public async Task<string> SendAndReadAsStringAsync(Func<HttpClient, Task<HttpResponseMessage>> func, HttpClient client, bool RaiseExpWhenBadResponse = true)
        {
            var response = await SendAsync(async () => { return await func.Invoke(client); }, RaiseExpWhenBadResponse);
            var rsp = await response.Content.ReadAsStringAsync();
            return rsp;
        }

        public static T DeserializeJson<T>(string str)
        {
            T result;
            try
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception)
            {
                result = default;
            }
            return result;
        }
    }
}
