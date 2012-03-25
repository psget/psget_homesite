using System.Collections.Generic;
using System.Collections.ObjectModel;
using Homesite.App.Providers.GitHub;

namespace Tests.Stubs
{
    public class StubGitHubClient : IGitHubClient
    {
        public readonly IDictionary<string, RepositoryInfo> Repositories = new Dictionary<string, RepositoryInfo>();
        public readonly IDictionary<string, UserInfo> Users = new Dictionary<string, UserInfo>();
        public readonly IDictionary<string, ReadOnlyCollection<string>> FileNames = new Dictionary<string, ReadOnlyCollection<string>>();

        public RepositoryInfo GetRepositoryInfo(string repoUrl)
        {
            if (Repositories.ContainsKey(repoUrl))
            {
                return Repositories[repoUrl];
            }
            return null;
        }

        public UserInfo GetUserInfo(string userUrl)
        {
            if (Users.ContainsKey(userUrl))
            {
                return Users[userUrl];
            }
            return null;
        }

        public ReadOnlyCollection<string> GetRepositoryFileNames(string repoHtmlUrl)
        {
            if (FileNames.ContainsKey(repoHtmlUrl))
            {
                return FileNames[repoHtmlUrl];
            }
            return null;
        }
    }
}