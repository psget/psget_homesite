using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Homesite.App.Providers.DirectoryProvider;
using System.ComponentModel.DataAnnotations;
using Homesite.App.Providers.Sumission;

namespace Homesite.Controllers.Api.Modules
{
    public class ModulesController : ApiController
    {
        readonly DirectoryProvider _provider;

        public ModulesController()
        {
            _provider = HomeApplication.Directory;
        }

        public class ModulesIndexModel
        {
            public List<Module> Modules { get; set; }
        }

        public ModulesIndexModel GetModules() 
        {
            return new ModulesIndexModel
            {
                Modules = _provider.ToList()
            };            
        }

        public class ModuleModel
        {
            public Module Module { get; set; }
        }

        public ModuleModel Get(string id)
        {
            return new ModuleModel
            {
                Module = _provider.FindById(id)
            };
        }

        public class SubmissionStatus 
        {
            public string Status { get; set; }            
        }

        public SubmissionStatus Status(string id)
        {
            return new SubmissionStatus
            {
            };
        }

        public class GitHubModuleSumissionModel 
        {
            [Required]
            public string HomeUrl { get; set; }            
        }
        
        public void PostGitHubModule(GitHubModuleSumissionModel model)
        {
            new SubmissionManager().SubmitGitHubModule(model.HomeUrl);
        }
    }
}