using Microsoft.Extensions.Logging;
using StandardLibrary.Exceptions;
using StandardLibrary.IServices;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StandardLibrary.Services
{
    public class BaseService<TService>
    {
        protected ILogger<TService> _logger;
        public BaseService(ILogger<TService> logger)
        {
            _logger = logger;
        }

        //protected static TD MapToModel<TE, TD>(TE model, IMapper _mapper) => _mapper.Map<TE, TD>(model);
        protected virtual async Task<(HttpStatusCode, string)> GetStringFromAPIAsync(string url, IHttpSendService _httpSendService, HttpClient _client)
        {
            try
            {
                var rsl = await GetStringAsync(url, _httpSendService, _client);
                return (HttpStatusCode.OK, rsl);
            }
            catch (HttpFailedException exhf)
            {
                return (exhf.StatusCode, exhf.Message);
            }
            catch (Exception exp)
            {
                exp.Data.Add("endpoint", url);
                _logger.LogError(exp, exp.Message);
                throw exp;
            }
        }
        protected virtual async Task<T> GetModelFromAPIAsync<T>(string url, IHttpSendService _httpSendService, HttpClient _client)
        {
            try
            {
                var rsl = await GetModelAsync<T>(url, _httpSendService, _client);
                return rsl;
            }
            catch (HttpFailedException exhf)
            {
                exhf.Data.Add("endpoint", url);
                _logger.LogError(exhf, exhf.Message);
                throw exhf;
            }
            catch (Exception exp)
            {
                exp.Data.Add("endpoint", url);
                _logger.LogError(exp, exp.Message);
                throw exp;
            }
        }

        protected static async Task<T> GetModelAsync<T>(string url, IHttpSendService _httpSendService, HttpClient _client)
        {
            var rsl = await _httpSendService.SendAndReadFromJsonAsync<T>(
                async client => { return await client.GetAsync(url); }, _client);
            return rsl;
        }
        protected static async Task<string> GetStringAsync(string url, IHttpSendService _httpSendService, HttpClient _client)
        {
            var rsl = await _httpSendService.SendAndReadAsStringAsync(
                async client => { return await client.GetAsync(url); }, _client);
            return rsl;
        }
    }
}