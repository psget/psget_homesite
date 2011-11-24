using System.Collections.Generic;
using System.Linq;
using Homesite.App.Providers.DirectoryProvider;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Integration.App.Providers.Directory
{
    public class When_downloading_directory
    {
        private List<Module> _resultModules;

        [TestFixtureSetUp]
        public void Given_downloaded_directory()
        {
            var provider = new DirectoryProvider();
            _resultModules = provider.ToList();
        }

        [Test]
        public void Should_download_something()
        {
            _resultModules.Should().Not.Be.Empty();
        }

        [Test]
        public void Should_serialize_psget_properties()
        {
            _resultModules.First(x => x.Id == "PsGet").ProjectUrl.Should().Equal("https://github.com/chaliy/psget/");
        }

        [Test]
        public void Should_read_author_information()
        {
            var module = _resultModules.First(x => x.Id == "PsGet");
            module.Author.Name.Should().Equal("Mike Chaliy");
            module.Author.Uri.Should().Equal("http://chaliy.name");
            module.Author.Email.Should().Equal("mike@chaliy.name");
        }

        [Test]
        public void Should_read_updated_information()
        {
            var module = _resultModules.First(x => x.Id == "PsGet");
            module.Updated.Should().Be.InRange(System.DateTimeOffset.Now.AddYears(-2), System.DateTimeOffset.Now.AddYears(2));            
        }
    }
}
