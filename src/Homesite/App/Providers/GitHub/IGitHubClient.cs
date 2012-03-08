namespace Homesite.App.Providers.GitHub
{
    public interface IGitHubClient
    {
        RepositoryInfo GetRepositoryInfo(string repoUrl);
    }
}