using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.WebUI.Models;
using System.Security.Claims;

namespace MiniECommerceApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private static readonly HttpClient _accountHttpClient = new HttpClient();

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var reqParameter = new
                {
                    Email = userLoginModel.Email,
                    Password = userLoginModel.Password
                };
                var isLoggedIn = await _accountHttpClient.PostAsJsonAsync(@"https://localhost:7123/api/identity/login?useCookies=true&useSessionCookies=true", reqParameter);
                if (isLoggedIn.IsSuccessStatusCode)
                {

                    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, reqParameter.Email.Substring(0,reqParameter.Email.IndexOf("@")))
    };
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(userIdentity), authProperties);
                    return returnUrl == null ? RedirectToAction("Index", "Home") : Redirect(returnUrl);
                }
                else
                {
                    ViewData["LoginError"] = " Kullanici adinizi veya şifreniz yanlış !.";
                }
            }
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            var isLoggedIn = await _accountHttpClient.GetAsync(@"https://localhost:7123/api/identity/manage/info");
            return View();
        }


    }
}
