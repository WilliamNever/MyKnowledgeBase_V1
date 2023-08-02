using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PrjNetCore6Test.ContactModels;
using PrjNetCore6Test.Models;
using System.Web;

namespace PrjNetCore6Test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TVTController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult ReadFromId([FromRoute] int id)
        {
            var sessStr = HttpContext.Session.GetString("axxm");
            var callRoute = Url.RouteUrl("Apis", new { action = "ReadFromSId", controller = "TVT", id });
            TempData["aaa"] = new List<string> { "aaaaX", "bbbbX" };
            var notis =
                //new List<Notification>
                //{
                //    new Notification { NotificationStyle = "ssss"/*, Message = "sssss", Dismissable = true*/ }
                //}
                new Notification { NotificationStyle = "ssss"/*, Message = "sssss", Dismissable = true*/ }
            ;
            TempData["bbb"] = Newtonsoft.Json.JsonConvert.SerializeObject(notis);
            return Json(new
            {
                id,
                value = $"{id} - {new Random().Next(10)}",
                Url = $"{callRoute}",
                Session = sessStr,
            });
        }

        [HttpGet("{id}")]
        public IActionResult ReadFromSId([FromRoute] int id)
        {
            var callRoute = Url.RouteUrl("Apis", new { action = "ReadFromId", controller = "TVT", id = id });
            return Json(new
            {
                id = id,
                value = $"{id} - {new Random().Next(10)}",
                Url = $"{callRoute}"
            });
        }

        [HttpPost]
        public IActionResult PostRecord([FromBody] CommRequestModel md)
        {
            return Json(md);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> PostRecordById([FromRoute] int id)
        {
            string response;
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                response = await reader.ReadToEndAsync();
                var parameters = HttpUtility.ParseQueryString(response);
                return Json(new { id, response, parameters });
            }
        }
        [HttpPut]
        public IActionResult PutRecord([FromBody] CommRequestModel md)
        {
            return Json(md);
        }
    }
}
