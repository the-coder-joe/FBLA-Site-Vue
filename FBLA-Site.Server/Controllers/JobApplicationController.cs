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

        [HttpPost("{id}")]
        public JsonResult AddPosting(int id, [FromBody] Posting posting)
        {
            jobApplicationService.AddPosting(posting, id);

            return Json(new { Success = true });
        }


        [HttpGet]
        public JsonResult GetPostings()
        {
            List<Posting> postings = this.jobApplicationService.GetPostings();

            return Json(postings);
        }

        [HttpGet("{id}")]
        public JsonResult GetPostingById(int id)
        {
            Posting? posting = this.jobApplicationService.GetPostingById(id);
            if (posting == null)
            {
                return Json(new ErrorResponse { Success = false, Message = $"Posting with Id {id} does not exist." });
            }
            return Json(posting);
        }

        [HttpGet("{id}")]
        public JsonResult GetPostingsByEmployerId(int id)
        {
            List<Posting> postings = this.jobApplicationService.GetPostingsByUserId(id);

            if (postings == null)
            {
                return Json(new ErrorResponse { Success = false, Message = $"Posting with user Id {id} does not exist." });
            }
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

        [HttpPost]
        public IActionResult SubmitJobApplication(Application application)
        {
            try
            {
                this.jobApplicationService.SubmitJobApplication(application);
            }
            catch (Exception ex)
            {
                return Json(new ErrorResponse { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }

        [HttpGet]
        public IActionResult GetJobApplications()
        {
            List<Application> applications = this.jobApplicationService.GetJobApplications();

            return Json(applications);
        }

        [HttpGet("{id}")]
        public IActionResult GetJobApplicationsByEmployerId(int id)
        {
            List<Application> applications = this.jobApplicationService.GetJobApplicationsByEmployerUserId(id);

            return Json(applications);
        }
    }
}
