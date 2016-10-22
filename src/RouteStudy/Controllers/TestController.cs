using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RouteStudy.Areas.Admin.Controllers
{
    [Route("durow/Test/[action]/{id=0}")]
    public class TestController : Controller
    {
        // GET: /<controller>/
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
        
        public IActionResult Show()
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
