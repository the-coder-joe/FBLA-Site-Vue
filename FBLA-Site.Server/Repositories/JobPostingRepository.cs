namespace FBLA_Site.Server.Repositories
{
    public class JobPostingRepository
    {
        private readonly JsonUtils<List<Posting>> postingUtils = new("postings");


        public List<Posting>? GetPostings()
        {
            return this.postingUtils.GetData();
        }

        public void AddPosting(Posting posting)
        {
            List<Posting> postings = this.postingUtils.GetData() ?? new List<Posting>();
            postings.Add(posting);
            this.postingUtils.SetData(postings);
        }

        public void DeletePosting(int id)
        {
            List<Posting> postings = this.postingUtils.GetData() ?? new List<Posting>();
            int index = postings.FindIndex((x) => x.Id == id);
            if (index < 0) return;

            postings.RemoveAt(index);

            this.postingUtils.SetData(postings);
        }
    }
}
