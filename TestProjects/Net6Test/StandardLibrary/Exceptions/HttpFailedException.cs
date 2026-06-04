using System;
using System.Net;
using System.Net.Http;

namespace StandardLibrary.Exceptions
{
    public class HttpFailedException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public HttpResponseMessage Response { get; private set; }
        public HttpFailedException(HttpResponseMessage Response, string msg) : base(msg)
        {
            StatusCode = Response.StatusCode;
            this.Response = Response;
        }
    }
}
