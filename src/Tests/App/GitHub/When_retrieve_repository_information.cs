using NUnit.Framework;
using Homesite.App.Providers.GitHub;
using Should.Fluent;

namespace Tests.App.GitHub
{
    public class When_retrieve_repository_information
    {
        private RepositoryInfo _result;

        [TestFixtureSetUp]
        public void Given_one_of_my_repos()
        {
            var client = new GitHubClient();
            _result = client.GetRepositoryInfo("https://api.github.com/repos/chaliy/psget");
        }

        [Test]
        public void Should_return_something()
        {
            _result.Should().Not.Be.Null();
        }

        [Test]
        public void Should_return_repository_object_url()
        {
            _result.Url.Should().Equal("https://api.github.com/repos/chaliy/psget");
        }

        [Test]
        public void Should_return_repository_url()
        {
            _result.HtmlUrl.Should().Equal("https://github.com/chaliy/psget");
        }        

        [Test]
        public void Should_return_repository_name()
        {
            _result.Name.Should().Equal("psget");
        }

        [Test]
        public void Should_return_repository_description()
        {
            _result.Description.Should().Contain("PowerShell");
        }

        [Test]
        public void Should_return_repository_owner()
        {
            _result.Owner.Should().Not.Be.Null();
        }

        [Test]
        public void Should_return_repository_owner_name()
        {
            _result.Owner.Login.Should().Equal("chaliy");
        }
    }
}
