using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RouteStudy.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            var list = new List<string>();
            foreach (var item in RouteData.Values)
            {
                list.Add($"{item.Key}:{item.Value}");
            }
            ViewData["Message"] = string.Join(",  ", list);

            return View();
        }
    }
}
