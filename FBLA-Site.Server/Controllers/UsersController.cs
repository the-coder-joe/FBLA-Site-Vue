using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using FBLA_Site.Server.Models;
using FBLA_Site.Server.Utils;
using Microsoft.AspNetCore.Mvc;
namespace FBLA_Site.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class UsersController : Controller
    {
        [HttpPost]
        public JsonResult Authenticate()
        {
            return Json(new { });
        }

    }
}
