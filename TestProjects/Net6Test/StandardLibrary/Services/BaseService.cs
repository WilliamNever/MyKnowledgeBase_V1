using StandardLibrary.IServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.Services
{
    public class BaseService<TService>
    {
        //protected static TD MapToModel<TE, TD>(TE model, IMapper _mapper) => _mapper.Map<TE, TD>(model);
        protected static async Task<T> GetFromAPIAsync<T>(string url, IHttpSendService _httpSendService, HttpClient _client)
        {
            var rsl = await _httpSendService.SendAndReadFromJsonAsync<T>(
                async client => { return await client.GetAsync(url); }, _client);
            return rsl;
        }
    }
}