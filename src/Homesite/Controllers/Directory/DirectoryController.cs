using System.Linq;
using System.Web.Mvc;

namespace Homesite.Controllers.Directory
{
    //[OutputCache(Duration = 60 * 60 /*60 minutes*/)]
    public class DirectoryController : Controller
    {                        
        public ActionResult Index()
        {
        	var provider = HomeApplication.Directory;
			return View(new IndexModel
			{
                Modules = provider.ToList()
            });
        }
        
        public ActionResult Details(string id)
        {
            var provider = HomeApplication.Directory;
            var module = provider.FindById(id);
            return View(new DetailsModel
            {
                Module = module
            });
        }

    }
}
