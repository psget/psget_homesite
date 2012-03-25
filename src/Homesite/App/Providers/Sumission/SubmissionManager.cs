using System.Collections.Generic;
using System.IO;
using System.Net;
using Homesite.App.Providers.GitHub;
using Homesite.App.Providers.Storage;
using Ionic.Zip;

namespace Homesite.App.Providers.Sumission
{
    public class SubmissionManager
    {        
        readonly IStorageClient _storage;
        readonly IGitHubClient _gitHub;

        public SubmissionManager(IStorageClient storage = null, IGitHubClient gitHub = null)
        {
            _gitHub = gitHub ?? new GitHubClient();
            _storage = storage ?? new StorageClient();
        }

        public bool SubmitModule(SubmissionCandidate candidate, string contactName, string contactEmail, List<string> feedback)
        {
            var submission = new SubmissionDoc
            {
                SubmissionSource = "Website",
                Contact = new Contact
                {
                    Name = contactName,
                    Email = contactEmail
                },
                Candidate = candidate
            };

            _storage.Store(submission);

            return true;
        }
        
        public IList<SubmissionDoc> QuerySubmissions()
        {
            return _storage.Query<SubmissionDoc>();
        }
    }
}