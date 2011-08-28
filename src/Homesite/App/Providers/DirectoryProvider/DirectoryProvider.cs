using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Homesite.App.Providers.DirectoryProvider
{
	public class DirectoryProvider
	{
        [DataContract(Namespace = "urn:psget:v1.0")]
        public class PsGetProperties
        {
            [DataMember]
            public string ProjectUrl { get; set; }
        }

	    private readonly string _directoryUrl;

        public DirectoryProvider(string directoryUrl = "https://raw.github.com/chaliy/psget/master/Directory.xml")
	    {
	        _directoryUrl = directoryUrl;
	    }

	    public IQueryable<Module> QueryModules()
		{
            using (var reader = XmlReader.Create(_directoryUrl))
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
                                                   ProjectUrl = properties.ProjectUrl
					                           };
					            })
					.AsQueryable();				
			}			
		}

	    private ModuleAuthor ResolveAuthor(SyndicationItem syndicationItem)
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
	}
}
