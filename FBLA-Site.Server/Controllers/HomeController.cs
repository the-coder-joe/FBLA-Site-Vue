using Microsoft.AspNetCore.Mvc;

namespace FBLA_Site.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
