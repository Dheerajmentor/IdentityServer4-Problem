using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dheeraj.POC.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using IdentityModel.Client;
using IdentityModel;

namespace Dheeraj.POC.UI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
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

        [Authorize]
        public async Task<IActionResult> Contact()
        {
            var accessToken = await AuthenticationHttpContextExtensions.GetTokenAsync(this.HttpContext, "access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var res = await client.GetAsync("https://localhost:44325/api/custom");
            return Ok(res);
        }
    }
}
