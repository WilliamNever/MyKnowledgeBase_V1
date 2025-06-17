using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApisTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpPost]
        [Route("Infor")]
        public string Infor([FromBody] AX ax)
        {
            return ax.Name;
        }

        [HttpGet]
        [Route("ToInfor")]
        public async Task<string> ToInfor([FromQuery] string ax)
        {
            var client = new HttpClient();

            //var mc = new MultipartFormDataContent("boundary-Name");
            //mc.Add(new StringContent(ax), "ax");
            //mc.Add(new StringContent("bx_string"), "bx");
            //mc.Add(new StringContent("cx_string"), "cx");

            var mc = new FormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("ax", ax),
                        new KeyValuePair<string, string>("bx", "bx_string"),
                        new KeyValuePair<string, string>("cx", "cx_string"),
                     });

            var url = $"{(HttpContext.Request.IsHttps ? "https://" : "http://")}" +
                $"{HttpContext.Request.Host}" +
                $"{Url.Action("InforForm", "Api")}";
            var rt = await client.PostAsync(url, mc);
            var str = await rt.Content.ReadAsStringAsync();
            return str;
        }


        [HttpPost]
        [Route("InforForm")]
        public string InforForm()
        {
            string ax = this.ControllerContext.HttpContext.Request.Form["ax"];
            string bx = this.ControllerContext.HttpContext.Request.Form["bx"];
            string cx = this.ControllerContext.HttpContext.Request.Form["cx"];
            return $"{ax}-{bx}-{cx}";
        }
    }

    public record AX (string Name);
}
