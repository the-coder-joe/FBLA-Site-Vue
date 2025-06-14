namespace FBLA_Site.Server.Repositories
{
    public class JobPostingRepository
    {
        private JsonUtils<List<Posting>> postingUtils = new JsonUtils<List<Posting>>("postings");


        public List<Posting>? GetPostings()
        {
            return postingUtils.GetData();
        }

        public void AddPosting(Posting posting)
        {
            var postings = postingUtils.GetData() ?? new List<Posting>();
            postings.Add(posting);
            postingUtils.SetData(postings);
        }
    }
}
