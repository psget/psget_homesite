namespace Homesite.App.Providers.Sumission
{
    public class SubmissionCandidate
    {
        public string ModuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }

        public Contact Author { get; set; }
        public Content Content { get; set; }

        public SubmissionCandidate()
        {
            Author = new Contact();
            Content = new Content();
        }
    }
}