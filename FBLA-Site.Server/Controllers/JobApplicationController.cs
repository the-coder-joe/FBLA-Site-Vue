using FBLA_Site.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace FBLA_Site.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : Controller
    {
        [HttpGet]
        public JsonResult GetJobApplications(Application app)
        {
            return Json(new { });
        }
    }
}
