using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Homesite.Application.Providers.DirectoryProvider
{
	public class DirectoryProvider
	{
		public IQueryable<Module> QueryModules()
		{
			using (var reader = XmlReader.Create("https://raw.github.com/chaliy/psget/master/Directory.xml"))
			{
				var feed = SyndicationFeed.Load(reader);

				return feed.Items
					.Select(x => new Module
					             	{
					             		Id = x.Id,
					             		Title = x.Summary.Text,										
										PsGetCommandLine = "Install-Module " + x.Id										
					             	})
					.AsQueryable();				
			}			
		}
	}
}
