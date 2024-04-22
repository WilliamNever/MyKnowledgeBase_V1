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
    }

    public record AX (string Name);
}
