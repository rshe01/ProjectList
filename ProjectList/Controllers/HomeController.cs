using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProjectListData.Models;


namespace ProjectList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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

        //just trying out some routing here - you can find the code in Startup.cs
        public IActionResult Endpoint()
        {
            return Ok("You've reached a hidden endpoint!");
        }
    }
}
