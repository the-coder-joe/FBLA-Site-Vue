using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using FBLA_Site.Server.Models;
using FBLA_Site.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using FBLA_Site.Server.Services;
namespace FBLA_Site.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class UsersController : Controller
    {
        private UserAuthenticationService userAuthentication = new UserAuthenticationService();


        [HttpPost]
        public JsonResult Authenticate(AuthenticationRequest request)
        {
            User? user = userAuthentication.Authenticate(request.Username, request.PartiallyHashedPassword);
            bool authenticated = user is not null;
            return Json(new { success = authenticated, role = user?.Role });
        }

        [HttpPost]
        public JsonResult AddUser([FromBody] NewUserRequest newUserRequest)
        {
            // Validate the request
            if (string.IsNullOrEmpty(newUserRequest.Email) || string.IsNullOrEmpty(newUserRequest.PartiallyHashedPassword))
            {
                return Json(new { success = false, message = "Username and password are required." });
            }

            userAuthentication.CreateUser(newUserRequest.Email, newUserRequest.PartiallyHashedPassword, newUserRequest.Role);

            return Json(new { success = true, message = "User added successfully." });
        }
    }
}
