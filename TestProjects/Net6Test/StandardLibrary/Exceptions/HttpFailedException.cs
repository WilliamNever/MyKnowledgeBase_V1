using System;
using System.Net;
using System.Net.Http;

namespace StandardLibrary.Exceptions
{
    public class HttpFailedException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        /// <summary>
        /// It is not a good idea to keep HttpResponseMessage in the Exception.
        /// </summary>
        public HttpResponseMessage Response { get; private set; }
        public HttpFailedException(HttpResponseMessage Response, string msg) : base(msg)
        {
            StatusCode = Response.StatusCode;
            this.Response = Response;
        }
    }
}
