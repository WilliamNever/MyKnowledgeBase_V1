using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6CoreLibrary.Helpers
{
    public static class HttpClientHelper
    {
        /// <summary>
        /// for post/put, it also can work for get/delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_logger"></param>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendHttpClientAsync<T>(
            ILogger<T>? _logger, string url, HttpContent? content,
            Func</*url*/string, HttpContent?, Task<HttpResponseMessage>> func)
        {
            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await func.Invoke(url, content);
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    string context = content == null ? "No Content" : await content.ReadAsStringAsync();
                    _logger.LogError($"Failed to call {url}. \r\n- {context} \r\n- {ex?.Message}");
                }
                throw;
            }
            return httpResponse;
        }
        /// <summary>
        /// for get/delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_logger"></param>
        /// <param name="Url"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendHttpClientAsync<T>(
            ILogger<T>? _logger, string Url, Func</*url*/string, Task<HttpResponseMessage>> func)
        {
            return await SendHttpClientAsync(_logger, Url, null, (url, cont) => func.Invoke(url));
        }
    }
}
