using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.IServices
{
    public interface IHttpSendService
    {
        Task<HttpResponseMessage> SendAsync(Func<HttpContent, Task<HttpResponseMessage>> func, HttpContent sender, bool RaiseExpWhenBadResponse = true);
        Task<HttpResponseMessage> SendAsync(Func<Task<HttpResponseMessage>> func, bool RaiseExpWhenBadResponse = true);

        Task<T> SendAndReadFromJsonAsync<T>(Func<Task<HttpResponseMessage>> func, bool RaiseExpWhenBadResponse = true);
        Task<string> SendAndReadAsStringAsync(Func<Task<HttpResponseMessage>> func, bool RaiseExpWhenBadResponse = true);

        Task<T> SendAndReadFromJsonAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> func, HttpClient client, bool RaiseExpWhenBadResponse = true);
        Task<string> SendAndReadAsStringAsync(Func<HttpClient, Task<HttpResponseMessage>> func, HttpClient client, bool RaiseExpWhenBadResponse = true);
    }
}
