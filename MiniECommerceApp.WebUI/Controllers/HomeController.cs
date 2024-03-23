using Microsoft.AspNetCore.Mvc;

namespace MiniECommerceApp.WebUI.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
