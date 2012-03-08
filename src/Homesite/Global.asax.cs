using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Homesite.App.Providers.DirectoryProvider;
using System.Web.Http;

namespace Homesite
{	
	public class HomeApplication : HttpApplication
	{
        public static DirectoryProvider Directory;

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GetPsGet",
                url: "GetPsGet.ps1",
                defaults: new { controller = "GetPsGet", action = "Index" }
            );
            
            routes.MapHttpRoute(
                "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                "DirectoryModule",
                url: "directory/{id}/",
                defaults: new { controller = "Directory", action = "Details" }
            );
			
            routes.MapRoute(
                "Default",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" }
            );            

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
            
            // TODO Should be lazy, and reload sometimes.
            Directory = new DirectoryProvider();
		}
	}
}