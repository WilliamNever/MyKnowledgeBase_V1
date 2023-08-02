using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrjNetCore6Test.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PrjNetCore6Test.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var listInfors = TempData["aaa"] as string[];
            HttpContext.Session.SetString("axxm", "A String Value-");
            var usr = HttpContext.User.Identities.First();
            usr.AddClaims(new List<Claim> { new Claim("ax", "axxx") });
            return View();
        }

        public IActionResult Privacy()
        {
            HttpContext.Session.SetString("axxm", "A String Value-");
            var usr = HttpContext.User.Identities.First();
            usr.AddClaims(new List<Claim> { new Claim("ax", "axxx") });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}