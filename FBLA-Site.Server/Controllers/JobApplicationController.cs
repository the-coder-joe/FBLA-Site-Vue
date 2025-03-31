using FBLA_Site.Server.Models;
using FBLA_Site.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace FBLA_Site.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class JobApplicationController : Controller

    {
        private JsonUtils<List<Posting>> postingUtils = new JsonUtils<List<Posting>>("postings");
        private JsonUtils<List<Posting>> postingQueue = new JsonUtils<List<Posting>>("postingQueue");

        [HttpGet]
        public JsonResult GetJobApplications(Application app)
        {
            return Json(new { });
        }

        [HttpPost]
        public JsonResult AddPosting([FromBody] Posting posting)
        {
            // 1. Log the incoming data
            Console.WriteLine("Posting data: " + JsonSerializer.Serialize(posting));

            // 2. Load existing postings from JSON
            List<Posting> postings = postingUtils.GetData() ?? new List<Posting>();
            List<Posting> postingQ = postingQueue.GetData() ?? new List<Posting>();

            // 3. Assign an ID if none is set yet (or if it’s -1)
            if (posting.Id <= 0)
            {
                if (postings.Count == 0 && postingQ.Count == 0)
                    posting.Id = 1;
                else
                    posting.Id = postings.Concat(postingQ).Max(p => p.Id) + 1;
            }

            // 4. Add the new posting


            postingQ.Add(posting);

            // 5. Save postings back to JSON
            postingQueue.SetData(postingQ);

            // 6. Return success
            return Json(new { Success = true, CreatedId = posting.Id });
        }



        [HttpGet]
        public JsonResult GetPostings()
        {
            List<Posting> postings = postingUtils.GetData() ?? new List<Posting>();

            return Json(postings);
        }

        [HttpGet]
        public JsonResult GetPostingQueue()
        {
            List<Posting> postingQ = postingQueue.GetData() ?? new List<Posting>();

            return Json(postingQ);
        }

        [HttpPost]
        public IActionResult PostingApproval(ApplicationApproval approval)
        {
            List<Posting> postingsQ = postingQueue.GetData() ?? new List<Posting>();
            List<Posting> postings = postingUtils.GetData() ?? new List<Posting>();

            Posting? posting = postingsQ.FirstOrDefault(p => p.Id == approval.Id);

            if (posting == null)
            {
                return Json(new ErrorResponse { Success = false, Message = $"Application with Id {approval.Id} does not exist." });
            }

            if (approval.IsApproved)
            {
                postings.Add(posting);
                postingUtils.SetData(postings);

            }

            postingsQ.Remove(posting);
            postingQueue.SetData(postingsQ);

            return Json(postings);
        }
    }
}
