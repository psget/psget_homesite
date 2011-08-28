using System.Linq;
using System.Web.Mvc;
using Homesite.App.Providers.DirectoryProvider;
using Homesite.Models.Directory;

namespace Homesite.Controllers
{
    public class DirectoryController : Controller
    {        
        //
        // GET: /Directory/
        [OutputCache(Duration = 60*60 /*60 minutes*/)]
        public ActionResult Index()
        {
        	var provider = new DirectoryProvider();
			return View(new IndexModel
							{
                        		Modules = provider.QueryModules().ToList()
                        	});
        }

    }
}
