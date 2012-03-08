using System.Collections.Generic;
using Homesite.App.Providers.GitHub;

namespace Tests.Stubs
{
    public class StubGitHubClient : IGitHubClient
    {
        public readonly IDictionary<string, RepositoryInfo> Repositories = new Dictionary<string, RepositoryInfo>();

        public RepositoryInfo GetRepositoryInfo(string repoUrl)
        {
            if (Repositories.ContainsKey(repoUrl))
            {
                return Repositories[repoUrl];
            }
            return null;
        }
    }
}