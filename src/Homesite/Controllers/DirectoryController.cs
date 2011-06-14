using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homesite.Application.Providers.DirectoryProvider;
using Homesite.Models.Directory;

namespace Homesite.Controllers
{
    public class DirectoryController : Controller
    {
        //
        // GET: /Directory/
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
