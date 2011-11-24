using System;
namespace Homesite.App.Providers.DirectoryProvider
{
	public class Module
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string PsGetCommandLine { get; set; }
        public string ProjectUrl { get; set; }
        public ModuleAuthor Author { get; set; }
        public DateTimeOffset Updated { get; set; }
	}

}