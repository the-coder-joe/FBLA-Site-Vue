using FBLA_Site.Server.Repositories;

namespace FBLA_Site
{
    public class JobApplicationService
    {
        private readonly JobPostingQueueRepository jobPostingQueueRepository = new JobPostingQueueRepository();
        private readonly JobPostingRepository jobPostingRepository = new JobPostingRepository();

        public void AddPosting(Posting posting)
        {
            // 2. Load existing postings from JSON
            List<Posting> postings = jobPostingRepository.GetPostings() ?? new List<Posting>();
            List<Posting> postingQ = jobPostingQueueRepository.getPostingQueue() ?? new List<Posting>();

            // 3. Assign an ID if none is set yet (or if it’s -1)
            if (posting.Id <= 0)
            {
                if (postings.Count == 0 && postingQ.Count == 0)
                    posting.Id = 1;
                else
                    posting.Id = postings.Concat(postingQ).Max(p => p.Id) + 1;
            }

            // 4. Add the new posting

            jobPostingQueueRepository.AddPosting(posting);
        }

        public List<Posting> GetPostings()
        {
            return this.jobPostingRepository.GetPostings() ?? new List<Posting>();
        }

        public List<Posting> GetPostingsQueue()
        {
            return this.jobPostingQueueRepository.getPostingQueue() ?? new List<Posting>();
        }

        public void ApprovePosting(int id, bool isApproved)
        {
            List<Posting> postingsQ = this.GetPostingsQueue();
            List<Posting> postings = this.GetPostings();

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
    }
}
