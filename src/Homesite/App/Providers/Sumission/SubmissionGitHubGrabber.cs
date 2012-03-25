using System.Collections.Generic;
using System.IO;
using System.Net;
using Homesite.App.Providers.GitHub;
using Ionic.Zip;

namespace Homesite.App.Providers.Sumission
{
    public class SubmissionGitHubGrabber
    {                
        readonly IGitHubClient _gitHub;

        public SubmissionGitHubGrabber(IGitHubClient gitHub = null)
        {
            _gitHub = gitHub ?? new GitHubClient();
        }        

        public SubmissionCandidate RetrieveGitHubModule(string homeUrl, List<string> feedback)
        {            
            if (!homeUrl.StartsWith("https://github.com/")) 
            {
                feedback.Add("You should provide GitHub home URL something like: https://github.com/chaliy/psget");
                return null;
            }

            var repoUrl = homeUrl
                .Replace("https://github.com/", "https://api.github.com/repos/")
                .Trim('/');

            var repo = _gitHub.GetRepositoryInfo(repoUrl);

            if (repo == null)
            {
                feedback.Add("GitHub repository of the module was not found. Please check URL you provided.");
                return null;
            }

            if (repo.IsPrivate)
            {
                feedback.Add("GitHub repository of the module should not be private.");
                return null;
            }

            var user = _gitHub.GetUserInfo(repo.Owner.Url);

            if (user == null)
            {
                feedback.Add("GitHub user of the module was not found. Please check URL you provided.");
                return null;
            }
                        
            var repoHtmlUrl = repo.HtmlUrl;
            var fileNames = _gitHub.GetRepositoryFileNames(repoHtmlUrl);

            return new SubmissionCandidate
            {
                Title = repo.Name,
                Description = repo.Description,
                ProjectUrl = repo.HtmlUrl,
                ModuleID = repo.Name,
                Content = new Content
                {
                    //Source = 
                },
                Author = new Contact
                {
                    Name = user.Name,
                    Email = user.Email,
                    Uri = user.Blog
                }
            };
        }

            
    }
}