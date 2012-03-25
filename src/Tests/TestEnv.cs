using Homesite.App.Providers.Sumission;
using Tests.Stubs;

namespace Tests
{
    public class TestEnv
    {
        public readonly StubStorageClient StorageClient = new StubStorageClient();
        public readonly StubGitHubClient GitHubClient = new StubGitHubClient();

        public SubmissionManager CreateSubmissionManager()
        {
            return new SubmissionManager(StorageClient, GitHubClient);
        }

        public SubmissionGitHubGrabber CreateSubmissionGitHubGrabber()
        {
            return new SubmissionGitHubGrabber(GitHubClient);
        }
    }
}