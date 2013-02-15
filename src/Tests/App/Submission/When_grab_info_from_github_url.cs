using System.Collections.Generic;
using Homesite.App.Providers.GitHub;
using Homesite.App.Providers.Sumission;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.App.Submission
{
    public class When_grab_info_from_github_url
    {
        SubmissionCandidate _submission;

        [TestFixtureSetUp]
        public void Given_exisiting_github_repo()
        {
            var env = new TestEnv();
            env.GitHubClient.FileNames["https://github.com/foo/bar/"] = new List<string>
            {
                "File1.md",
                "File2.md"
            }.AsReadOnly();      
      
            env.GitHubClient.Users["https://api.github.com/users/faa"] = new UserInfo
            {
                Name = "Faa"                
            };

            env.GitHubClient.Repositories["https://api.github.com/repos/foo/bar"] = new RepositoryInfo
            {
                Name = "Bar",
                HtmlUrl = "https://github.com/foo/bar/",
                Owner = new UserRefInfo
                {
                    Url = "https://api.github.com/users/faa"
                }
            };

            var manager = env.CreateSubmissionGitHubGrabber();

            var feedback = new List<string>();
            _submission = manager.RetrieveGitHubModule("https://github.com/foo/bar/", feedback);
            
        }

        [Test]
        public void Should_grab_something()
        {
            _submission.Should().Not.Be.Null();
        }

        [Test]
        public void Should_correctly_find_title()
        {
            _submission.Title.Should().Equal("Bar");
        }
    }
}
