using Microsoft.AspNetCore.Mvc;

namespace PrjNetCore6Test.Areas.AHelpArea.Controllers
{
    [Area("AHelpArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
