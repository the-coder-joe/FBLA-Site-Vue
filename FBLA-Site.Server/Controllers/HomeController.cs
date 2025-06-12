using Microsoft.AspNetCore.Mvc;

namespace FBLA_Site
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult thing()
        {
            return new JsonResult(new { });
        }
    }
}
