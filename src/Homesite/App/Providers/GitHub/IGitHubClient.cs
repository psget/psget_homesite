using System.Collections.ObjectModel;

namespace Homesite.App.Providers.GitHub
{
    public interface IGitHubClient
    {
        RepositoryInfo GetRepositoryInfo(string repoUrl);

        UserInfo GetUserInfo(string userUrl);

        ReadOnlyCollection<string> GetRepositoryFileNames(string repoHtmlUrl);    
    }
}