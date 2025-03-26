using FBLA_Site.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FBLA_Site.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class JobApplicationController : Controller

    {
        [HttpGet]
        public JsonResult GetJobApplications(Application app)
        {
            return Json(new { });
        }

        [HttpPost]
        public JsonResult AddPosting(Posting posting)
        {
            Posting[] postings;
            using (StreamReader r = new StreamReader("postings.json"))
            {
                string json = r.ReadToEnd();
                postings = JsonSerializer.Deserialize<Posting[]>(json) ?? [];
            }


            postings.Append(posting);

            using (StreamWriter File1 = new("postings.json"))
            {
                string newJson = JsonSerializer.Serialize<Posting[]>(postings, new JsonSerializerOptions() { WriteIndented = true });
                File1.Write(newJson);
            }
            return Json(new { Success = true });
        }

        [HttpGet]
        public JsonResult GetPostings()
        {
            Posting[] postings;
            using (StreamReader r = new StreamReader("postings.json"))
            {
                string json = r.ReadToEnd();
                postings = JsonSerializer.Deserialize<Posting[]>(json) ?? [];
            }

            return Json(postings);
        }
    }
}
