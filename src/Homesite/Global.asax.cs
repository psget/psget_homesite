using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Homesite.App.Providers.DirectoryProvider;

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
                "Default", // Route name
                "{controller}/", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Details", // Route name
                "{controller}/{id}/", // URL with parameters
                new { controller = "Home", action = "Details" } // Parameter defaults
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