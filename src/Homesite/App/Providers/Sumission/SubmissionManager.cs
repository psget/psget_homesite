using System.Collections.Generic;
using Homesite.App.Providers.GitHub;
using Homesite.App.Providers.Storage;

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

        public bool SubmitGitHubModule(string homeUrl, string contactName, string contactEmail, List<string> feedback)
        {
            if (!homeUrl.StartsWith("https://github.com/")) 
            {
                feedback.Add("You should provide GitHub home URL something like: https://github.com/chaliy/psget");
                return false;
            }

            var repoUrl = homeUrl
                .Replace("https://github.com/", "https://api.github.com/repos/")
                .Trim('/');

            var repo = _gitHub.GetRepositoryInfo(repoUrl);

            if (repo == null)
            {
                feedback.Add("GitHub repository of the module was not found. Please check URL you provided.");
                return false;
            }

            if (repo.IsPrivate)
            {
                feedback.Add("GitHub repository of the module should not be private.");
                return false;
            }

            var submission = new SubmissionDoc
            {
                SubmissionSource = "GitHub: " + homeUrl,
                Candidate = new SubmissionCandidate
                {
                    Title = repo.Name,
                    Description = repo.Description,
                    ProjectUrl = repo.HtmlUrl,
                    ModuleId = repo.Name,
                    Author = new Contact
                    {
                        Name = repo.Owner.Login
                    }
                }
            };

            _storage.Store(submission);

            return true;
        }

        public IList<SubmissionDoc> QeurySubmissions()
        {
            return _storage.Query<SubmissionDoc>();
        }
    }
}