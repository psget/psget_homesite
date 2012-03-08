using System.Collections.Generic;
using System.Linq;
using Homesite.App.Providers.GitHub;
using Homesite.App.Providers.Sumission;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.App.Sumission
{
    public class When_submit_from_github_url
    {
        SubmissionDoc _submission;

        [TestFixtureSetUp]
        public void Given_exisiting_github_repo()
        {
            var env = new TestEnv();
            env.GitHubClient.Repositories["https://api.github.com/repos/foo/bar"] = new RepositoryInfo
            {
                Name = "Bar"
            };

            var manager = env.CreateSubmissionManager();

            var feedback = new List<string>();
            manager.SubmitGitHubModule("https://github.com/foo/bar/", "Foofov", "foofov@example.com", feedback);

            _submission = env.StorageClient.Query<SubmissionDoc>().FirstOrDefault();
        }

        [Test]
        public void Should_add_submission()
        {
            _submission.Should().Not.Be.Null();
        }

        [Test]
        public void Should_correctly_find_title()
        {
            _submission.Candidate.Title.Should().Equal("Bar");
        }
    }
}
