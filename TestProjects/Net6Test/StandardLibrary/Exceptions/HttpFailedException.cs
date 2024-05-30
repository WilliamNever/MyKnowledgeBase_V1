using System;
using System.Net;

namespace StandardLibrary.Exceptions
{
    public class HttpFailedException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpFailedException(HttpStatusCode StatusCode, string content) : base(content)
        {
            this.StatusCode = StatusCode;
        }
    }
}
