using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Logging;
using WindowAuthorization.Models;

namespace WindowAuthorization.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string role = "XXXX";
            var user = ControllerContext.HttpContext.User;
            var uidentity = HttpContext.User.Identity;
            var inGrp = user.IsInRole(role);

            
            var userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, "usddd"));
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            //ControllerContext.HttpContext.User = userPrincipal;

            //var ticket = new AuthenticationTicket(userPrincipal, 
            //    new AuthenticationProperties
            //{
            //    IsPersistent = true,
            //    AllowRefresh = true,
            //}, IISDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal
                ,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                }
                );

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            //var cookie = Request.Cookies[".AspNetCore.Cookies"];
            //HttpContext.Request.Cookies.TryGetValue(key, out value);
            //var str = await Task.Run(() => { return 1; });

            //await HttpContext.SignOutAsync("Cookies"); //"Cookie"
            //HttpContext.User=new System.Security.Claims.ClaimsPrincipal()
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //return View("Index");
            return RedirectToAction("Privacy");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
