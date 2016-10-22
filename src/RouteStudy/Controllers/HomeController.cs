using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RouteStudy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string id, string controller, string action)
        {
            ViewData["Message"] = "id is " + id + ",  controller is " + controller + ",  action is " + action;
            return View();
        }

        public IActionResult About()
        {
            var list = new List<string>();

            foreach (var item in RouteData.Values)
            {
                list.Add($"{item.Key}:{item.Value}");
            }
            ViewData["Message"] = string.Join(",  ", list);

            return View();
        }

        public IActionResult Contact(string id,string controller, string action)
        {
            ViewData["Message"] = "id is " + id +",  controller is "+ controller + ",  action is " + action;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
