namespace FBLA_Site
{
    public class JobPostingQueueRepository
    {
        private JsonUtils<List<Posting>> postingQueue = new JsonUtils<List<Posting>>("postingQueue");

        public List<Posting>? getPostingQueue()
        {
            return postingQueue.GetData();
        }

        public void AddPosting(Posting posting)
        {
            List<Posting> postingsQ = this.getPostingQueue() ?? new List<Posting>();


            postingsQ.Add(posting);

            postingQueue.SetData(postingsQ);
        }

        public void RemovePosting(Posting posting)
        {
            var postingsQ = this.getPostingQueue() ?? new List<Posting>();
            postingsQ.Remove(posting);
            postingQueue.SetData(postingsQ);
        }
    }
}
