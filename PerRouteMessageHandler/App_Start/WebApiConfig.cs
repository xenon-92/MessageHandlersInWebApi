using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace PerRouteMessageHandler
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            /*******************************************************/
            DelegatingHandler[] handlers = new DelegatingHandler[]
            {
                new PerRouteMessageHandler()
            };
            var routeHandlers = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config),handlers);
            /*******************************************************/
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new AllRouteMessageHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Route2",
                routeTemplate: "api2/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: routeHandlers//<<<<<<<<<<<***********************************
            );
        }
    }
}
