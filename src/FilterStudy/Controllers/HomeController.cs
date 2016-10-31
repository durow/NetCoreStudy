using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilterStudy.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilterStudy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [MyAuth]
        [MyResult]
        [MyAction]
        public void Contact()
        {
            // ViewData["Message"] = "Your contact page.";
            // return View();
            ResponseHelper.Write("Home.Contact Start!",HttpContext.Response);
            ResponseHelper.Write("Home.Contact Do something!",HttpContext.Response);
            ResponseHelper.Write("Home.Contact End!",HttpContext.Response);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
