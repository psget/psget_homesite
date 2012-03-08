using System.Collections.Generic;
using Homesite.App.Providers.GitHub;

namespace Homesite.App.Providers.Sumission
{
    public class SubmissionManager
    {        
        public void SubmitGitHubModule(string homeUrl)
        {            
            var feedback = new List<string>();
            //https://api.github.com/repos/octocat/Hello-World
            if (!homeUrl.StartsWith("https://github.com/")) 
            {
                feedback.Add("You should provide GitHub home URL something like: https://github.com/chaliy/psget");
                return;
            }
            
            var client = new GitHubClient();

            var repo = client.GetRepositoryInfo(homeUrl).Result;

            if (repo.IsPrivate)
            {
                feedback.Add("GitHub repository of the module should not be private.");
                return;
            }

            return;
        }        
    }
}