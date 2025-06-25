using FBLA_Site.Server.Repositories;

namespace FBLA_Site
{
    public class JobApplicationService
    {
        private readonly JobPostingQueueRepository jobPostingQueueRepository = new();
        private readonly JobPostingRepository jobPostingRepository = new();
        private readonly JobApplicationRepository jobApplicationRepository = new();

        public void AddPosting(Posting posting, int submittedBy)
        {
            // 2. Load existing postings from JSON
            List<Posting> postings = this.jobPostingRepository.GetPostings() ?? new List<Posting>();
            List<Posting> postingQ = this.jobPostingQueueRepository.getPostingQueue() ?? new List<Posting>();

            // 3. Assign an ID if none is set yet (or if it’s -1)
            if (posting.Id <= 0)
            {
                if (postings.Count == 0 && postingQ.Count == 0)
                    posting.Id = 1;
                else
                    posting.Id = postings.Concat(postingQ).Max(p => p.Id) + 1;
            }

            // 4. Add the new posting

            this.jobPostingQueueRepository.AddPosting(posting);
        }

        public List<Posting> GetPostings()
        {
            return this.jobPostingRepository.GetPostings() ?? new List<Posting>();
        }

        public List<Posting> GetPostingsQueue()
        {
            return this.jobPostingQueueRepository.getPostingQueue() ?? new List<Posting>();
        }

        public Posting? GetPostingById(int id)
        {
            List<Posting> postings = GetPostings();
            Posting? posting = postings.FirstOrDefault(p => p.Id == id);
            return posting;
        }

        public List<Posting> GetPostingsByUserId(int id)
        {
            List<Application> applications = this.jobApplicationRepository.GetApplications() ?? [];
            List<Posting> postings = this.jobPostingRepository.GetPostings() ?? [];

            List<Posting> postingsOfEmployer = postings
                .Where((x) => x.SubmittedById == id)
                .ToList();

            return postingsOfEmployer;
        }

        public void ApprovePosting(int id, bool isApproved)
        {
            List<Posting> postingsQ = GetPostingsQueue();
            List<Posting> postings = GetPostings();

            Posting? posting = postingsQ.FirstOrDefault(p => p.Id == id);

            if (posting == null)
            {
                throw new KeyNotFoundException();
            }

            if (isApproved)
            {
                this.jobPostingRepository.AddPosting(posting);
                this.jobPostingQueueRepository.RemovePosting(posting);
            }
        }

        public void SubmitJobApplication(Application application)
        {
            List<Application> applications = this.jobApplicationRepository.GetApplications();

            int nextId = applications.Count > 0 ? applications.Max(a => a.Id) + 1 : 1;

            application.Id = nextId;

            this.jobApplicationRepository.AddAppliction(application);
        }

        public List<Application> GetJobApplications()
        {
            return this.jobApplicationRepository.GetApplications();
        }

        public List<Application> GetJobApplicationsByEmployerUserId(int id)
        {
            List<Application> applications = this.jobApplicationRepository.GetApplications() ?? [];
            List<Posting> postings = this.jobPostingRepository.GetPostings() ?? [];

            List<int> postingIdsOfEmployer = postings
                .Where((x) => x.SubmittedById == id)
                .Select((x) => x.Id)
                .ToList();

            List<Application> applicationsFromEmployer = applications
                .Where((x) => postingIdsOfEmployer.Contains(x.Id))
                .ToList();

            return applicationsFromEmployer;
        }
    }
}
