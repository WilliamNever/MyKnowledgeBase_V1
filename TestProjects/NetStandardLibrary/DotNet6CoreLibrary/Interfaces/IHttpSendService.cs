using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6CoreLibrary.Interfaces
{
    public interface IHttpSendService
    {
        Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> func);
        Task<T?> SendAndReadFromJsonAsync<T>(Func<Task<HttpResponseMessage>> func);
        Task<string> SendAndReadAsStringAsync(Func<Task<HttpResponseMessage>> func);
    }
}
