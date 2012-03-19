using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Homesite.App.Providers.DirectoryProvider
{
	public class DirectoryProvider : IEnumerable<Module>
	{
        [DataContract(Namespace = "urn:psget:v1.0")]
        public class PsGetProperties
        {
            [DataMember]
            public string ProjectUrl { get; set; }
        }

	    private readonly string _directoryUrl;
        private readonly IList<Module> _modules;

        public DirectoryProvider(string directoryUrl = "https://raw.github.com/psget/psget/master/Directory.xml")
	    {
	        _directoryUrl = directoryUrl;
            _modules = QueryModules(directoryUrl);
	    }

	    private static IList<Module> QueryModules(string url)
		{
            using (var reader = XmlReader.Create(url))
			{
				var feed = SyndicationFeed.Load(reader);

				return feed.Items
					.Select(x =>
					            {
					                var properties = x.ElementExtensions
                                        .ReadElementExtensions<PsGetProperties>("properties", "urn:psget:v1.0")
                                        .FirstOrDefault() ?? new PsGetProperties();

					                return new Module
					                           {
					                               Id = x.Id,
					                               Title = x.Summary.Text,
					                               PsGetCommandLine = "Install-Module " + x.Id,
					                               Author = ResolveAuthor(x),
                                                   ProjectUrl = properties.ProjectUrl,
                                                   Updated = x.LastUpdatedTime
					                           };
					            })
					.ToList();
			}			
		}

	    private static ModuleAuthor ResolveAuthor(SyndicationItem syndicationItem)
	    {
	        if (syndicationItem.Authors.Count > 0)
	        {
	            var person = syndicationItem.Authors[0];

	            return new ModuleAuthor
	                       {
	                           Name = person.Name,
	                           Email = person.Email,
	                           Uri = person.Uri
	                       };
	        }
	        return new ModuleAuthor();
	    }

        public Module FindById(string id)
        {
            return this.FirstOrDefault(x => x.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerator<Module> GetEnumerator()
        {
            return _modules.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
