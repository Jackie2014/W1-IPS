using System.Reflection;
using System.Web.Http;
using AttributeRouting.Web.Http.WebHost;

[assembly: WebActivator.PreApplicationStartMethod(typeof(IPDetectServer.Web.App_Start.AttributeRoutingHttp), "Start")]

namespace IPDetectServer.Web.App_Start
{
    public static class AttributeRoutingHttp {
		public static void RegisterRoutes(HttpRouteCollection routes) {
            
			// See http://github.com/mccalltd/AttributeRouting/wiki for more options.
			// To debug routes locally using the built in ASP.NET development server, go to /routes.axd

            Assembly assembly = Assembly.GetAssembly(typeof(IPDetectServer.Services.ServiceAssemble));
            routes.MapHttpAttributeRoutes(config=>config.AddRoutesFromAssembly(assembly));
		}

        public static void Start() {
            RegisterRoutes(GlobalConfiguration.Configuration.Routes);
        }
    }
}
