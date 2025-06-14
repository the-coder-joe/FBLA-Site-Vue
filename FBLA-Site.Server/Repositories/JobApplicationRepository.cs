namespace FBLA_Site
{
    public class JobApplicationRepository
    {
        private readonly JsonUtils<List<Application>> jobApplicationUtils = new JsonUtils<List<Application>>("jobApplications");

        public void AddAppliction(Application application)
        {
            var applications = jobApplicationUtils.GetData() ?? new List<Application>();

            applications.Add(application);

            jobApplicationUtils.SetData(applications);
        }

        public List<Application> GetApplications()
        {
            return jobApplicationUtils.GetData() ?? new List<Application>();
        }
    }
}
