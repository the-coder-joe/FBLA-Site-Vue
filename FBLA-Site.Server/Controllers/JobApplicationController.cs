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
            List<Posting> postings;
            try
            {
                using (StreamReader r = new StreamReader("/ApplicationData/postings.json"))
                {
                    string json = r.ReadToEnd();
                    postings = JsonSerializer.Deserialize<List<Posting>>(json) ?? new List<Posting>();
                }
            } catch (Exception e)
            {
                postings = new List<Posting>();
            }

            postings.Add(posting);

            using (StreamWriter File1 = new("./ApplicationData/postings.json"))
            {
                string newJson = JsonSerializer.Serialize(postings);
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
