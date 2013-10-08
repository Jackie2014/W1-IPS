using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using IPDetectServer.Lib.APIHandlers;

namespace IPDetectServer.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                            name: "DefaultApi",
                            routeTemplate: "apis/{controller}/{id}",
                            defaults: new { id = RouteParameter.Optional });
            config.MessageHandlers.Add(new MessageContextHandler());

            config.Filters.Add(new MobileExceptionFilter());

            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
