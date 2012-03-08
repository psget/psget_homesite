namespace Homesite.App.Providers.Sumission
{
    public class SubmissionDoc
    {
        public string Id { get; set; }

        public string SubmissionSource { get; set; }

        public Contact Contact { get; set; }
        public SubmissionCandidate Candidate { get; set; }

        public SubmissionDoc()
        {
            Contact = new Contact();
            Candidate = new SubmissionCandidate();
        }
    }
}