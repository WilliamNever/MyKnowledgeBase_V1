using DotNet6CoreLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6CoreLibrary.Services
{
    [ExcludeFromCodeCoverage]
    public class HttpSendService : IHttpSendService
    {
        protected readonly ILogger<HttpSendService> _logger;
        public HttpSendService(ILogger<HttpSendService> logger)
        {
            _logger = logger;
        }
        public async Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> func)
        {
            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await func.Invoke();
                if (!httpResponse.IsSuccessStatusCode)
                {
                    _logger.LogError(await httpResponse.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw;
            }
            return httpResponse;
        }

        public async Task<T?> SendAndReadFromJsonAsync<T>(Func<Task<HttpResponseMessage>> func)
        {
            var response = await SendAsync(func);
            var rsp = await response.Content.ReadFromJsonAsync<T>();
            return rsp;
        }
        public async Task<string> SendAndReadAsStringAsync(Func<Task<HttpResponseMessage>> func)
        {
            var response = await SendAsync(func);
            var rsp = await response.Content.ReadAsStringAsync();
            return rsp;
        }
    }

}
