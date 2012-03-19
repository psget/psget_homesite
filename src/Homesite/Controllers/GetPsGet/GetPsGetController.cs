using System.Net;
using System.Web.Mvc;
using Homesite.Fx;

namespace Homesite.Controllers.GetPsGet
{
    public class GetPsGetController : Controller
    {
        public ActionResult Index() 
        {
            var client = new WebClient();
            var result = client.DownloadString("https://github.com/chaliy/psget/raw/master/GetPsGet.ps1");
            GoogleAnalytics.SendEvent("/GetPsGet.ps1");
            return Content(result, "text/plain");
        }
    }
}