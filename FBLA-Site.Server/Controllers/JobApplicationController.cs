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
        public JsonResult AddPosting(Posting posting)
        {
            List<Posting> postings = postingUtils.GetData() ?? new List<Posting>();
            postings.Add(posting);

            postingUtils.SetData(postings);
            return Json(new { Success = true });
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
