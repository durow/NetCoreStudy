using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingStudy.Controllers
{
    public class HomeController : Controller
    {
        private ILogger _logger;

        public HomeController(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<HomeController>();
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            _logger.LogInformation($"{HttpContext.Connection.RemoteIpAddress.ToString()}访问Home/Index页面！");
            return View();
        }

        public IActionResult About()
        {
            _logger.LogInformation($"{HttpContext.Connection.RemoteIpAddress.ToString()}访问Home/About页面！");
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogInformation($"{HttpContext.Connection.RemoteIpAddress.ToString()}访问Home/Contact页面！");
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            _logger.LogError($"{HttpContext.Connection.RemoteIpAddress.ToString()}引发错误!");
            return View();
        }
    }
}
