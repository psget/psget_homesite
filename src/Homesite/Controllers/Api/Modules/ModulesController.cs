using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Homesite.App.Providers.DirectoryProvider;

namespace Homesite.Controllers.Api.Directory
{
    public class ModulesController : ApiController
    {
        readonly DirectoryProvider _provider;

        public ModulesController()
        {
            _provider = HomeApplication.Directory;
        }

        public IEnumerable<Module> GetModules() 
        {            
            return _provider.ToList(); 
        }

        public Module Get(string id)
        {            
            return _provider.FindById(id);
        }
    }
}