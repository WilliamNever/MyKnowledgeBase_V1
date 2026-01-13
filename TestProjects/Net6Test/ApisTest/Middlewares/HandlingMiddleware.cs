using System.Buffers;
using System.Text;

namespace ApisTest.Middlewares
{
    public class HandlingMiddleware
    {
        private readonly RequestDelegate next;
        public HandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await next(context);
            //var PostTxt = await ReadHttpRequestBodyByBodyReaderAsync(context.Request);
        }
        private static async Task<string> ReadHttpRequestBodyByBodyReaderAsync(HttpRequest request)
        {
            //request.EnableBuffering();
            var reader = request.BodyReader;
            var buffer = new ArrayBufferWriter<byte>();
            var result = await reader.ReadAsync();
            while (!result.IsCompleted)
            {
                buffer.Write(result.Buffer.ToArray());
                reader.AdvanceTo(result.Buffer.End);
                result = await reader.ReadAsync();
            }
            var bties = buffer.WrittenMemory.ToArray();
            var data = Encoding.UTF8.GetString(bties);
            return data;
        }
    }
}
