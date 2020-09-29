using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.SessionState;
using System.Web.Routing;

namespace AcmeFood
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}",
                new { id = RouteParameter.Optional },
                new MyHttpControllerRouteHandler()
            );

            config.MessageHandlers.Add(new Handler.CookieHandler());

        }
    }

    public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState {
        public MyHttpControllerHandler(RouteData routeData) : base(routeData) { }
    }
    public class MyHttpControllerRouteHandler : HttpControllerRouteHandler {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext) {
            return new MyHttpControllerHandler(requestContext.RouteData);
        }
    }

}
