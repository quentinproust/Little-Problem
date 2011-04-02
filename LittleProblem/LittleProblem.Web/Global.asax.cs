using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LittleProblem.Quartz;

namespace LittleProblem.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "ResponseNoteUp", // Route name
                "Problem/Up/{id}-{responseId}", // URL with parameters
                new { controller = "Problem", action = "Up", id = "", responseId = "" } // Parameter defaults
            );

            routes.MapRoute(
                "ResponseNoteDown", // Route name
                "Problem/Down/{id}-{responseId}", // URL with parameters
                new { controller = "Problem", action = "Down", id = "", responseId = "" } // Parameter defaults
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            
            
        }

        protected void Application_Start()
        {
            var bootStrapper = new WebBootStrapper();
            bootStrapper.Initialize();

            ControllerBuilder.Current.SetControllerFactory(new SMControllerFactory());

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
    }
}