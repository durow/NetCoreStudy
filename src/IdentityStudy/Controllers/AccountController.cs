using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityStudy.Services;
using Microsoft.Extensions.Configuration;
using IdentityStudy.ViewModels;

namespace IdentityStudy.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IdentityService _identityService;

        public AccountController(
            ILoggerFactory loggerFactory,
            IdentityService identityService)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _identityService = identityService;
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(model);

            //使用用户名和密码获取用户信息
            var user = await _identityService.CheckUserAsync(model.UserName, model.Password);
            if(user == null)
            {
                //无用户返回错误
                ModelState.AddModelError(string.Empty, "用户名或密码错误!");
                return View(model);
            }
            //登录，在Response中设置Cookie
            await HttpContext.Authentication.SignInAsync(IdentityService.AuthenticationScheme, user);
            return RedirectToLocal(returnUrl);
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(model);

            //添加新用户
            var result = await _identityService.RegisterAsync(model.UserName, model.Password);
            if(!result.IsSuccess)
            {
                //失败返回错误
                ModelState.AddModelError(string.Empty, result.ErrorString);
                return View(model);
            }
            //登录
            await HttpContext.Authentication.SignInAsync(IdentityService.AuthenticationScheme, result.User);
            return RedirectToLocal(returnUrl);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            //登出
            await HttpContext.Authentication.SignOutAsync(IdentityService.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
