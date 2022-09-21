using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ServiceProvider
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "OneParameter",
                routeTemplate: "api/{controller}/{num}/{token}",
                defaults: new { num = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "TwoParameters",
                routeTemplate: "api/{controller}/{num1}/{num2}/{token}",
                defaults: new { num1 = RouteParameter.Optional, num2 = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ThreeParameters",
                routeTemplate: "api/{controller}/{num1}/{num2}/{num3}/{token}",
                defaults: new { num1 = RouteParameter.Optional, num2 = RouteParameter.Optional, num3 = RouteParameter.Optional }
            );
        }
    }
}
