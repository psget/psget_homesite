using NUnit.Framework;
using Homesite.App.Providers.GitHub;
using Should.Fluent;

namespace Tests.App.GitHub
{
    public class When_retrieve_repository_information_repo_not_exitis
    {
        private RepositoryInfo _result;

        [TestFixtureSetUp]
        public void Given_one_of_my_repos()
        {
            var client = new GitHubClient();
            _result = client.GetRepositoryInfo("https://api.github.com/repos/chaliy/psget12");
        }

        [Test]
        public void Should_return_nothing()
        {
            _result.Should().Be.Null();
        }        
    }
}
