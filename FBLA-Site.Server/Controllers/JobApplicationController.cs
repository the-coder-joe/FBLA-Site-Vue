using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace FBLA_Site
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class JobApplicationController : Controller

    {
        private readonly JobApplicationService jobApplicationService = new JobApplicationService();

        [HttpGet]
        public JsonResult GetJobApplications(Application app)
        {
            return Json(new { });
        }

        [HttpPost]
        public JsonResult AddPosting([FromBody] Posting posting)
        {
            jobApplicationService.AddPosting(posting);

            return Json(new { Success = true, CreatedId = posting.Id });
        }


        [HttpGet]
        public JsonResult GetPostings()
        {
            List<Posting> postings = this.jobApplicationService.GetPostings();

            return Json(postings);
        }

        [HttpGet]
        public JsonResult GetPostingQueue()
        {
            List<Posting> postingQ = this.jobApplicationService.GetPostingsQueue();

            return Json(postingQ);
        }

        [HttpPost]
        public IActionResult PostingApproval(ApplicationApproval approval)
        {
            try
            {
                this.jobApplicationService.ApprovePosting(approval.Id, approval.IsApproved);

            }
            catch (Exception ex)
            {
                return Json(new ErrorResponse { Success = false, Message = $"Application with Id {approval.Id} does not exist." });
            }

            var postings = this.jobApplicationService.GetPostings();

            return Json(postings);
        }
    }
}
