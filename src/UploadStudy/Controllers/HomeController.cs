using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment env;
        private HttpContext context;

        public HomeController(IHostingEnvironment env)
        {
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }

        public IActionResult Contact()
        {
            //ViewBag.Message = context.Request.Path;
            return View();
        }

        [HttpPost]
        public IActionResult File()
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file == null)
                return RedirectToAction("Fail");

            var filePath = SaveFile(file);
            return Redirect("/" + filePath);
        }

        [HttpPost]
        public IActionResult Files()
        {
            foreach (var file in HttpContext.Request.Form.Files)
            {
                SaveFile(file);
            }
            return RedirectToAction("Success");
        }

        private string SaveFile(IFormFile file)
        {
            if (file == null) return string.Empty;
            if (string.IsNullOrEmpty(file.FileName)) return string.Empty;

            var name = Path.Combine("Upload", DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName);
            var filename = Path.Combine(env.WebRootPath, name);
            CheckFoler(filename);

            using (var stream = new FileStream(filename, FileMode.CreateNew))
            {
                file.CopyTo(stream);
            }

            return name.Replace("\\", "/");
        }

        private void CheckFoler(string filename)
        {
            var dir = Path.GetDirectoryName(filename);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}
