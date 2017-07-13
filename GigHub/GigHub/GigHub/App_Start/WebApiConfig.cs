using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GigHub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Using Camel Notation for JSON data results
            var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Follow",
                routeTemplate: "api/Following/Follow/{id}",
                defaults: new { Controller = "Following", Action = "Follow", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UnFollow",
                routeTemplate: "api/Following/UnFollow/{id}",
                defaults: new { Controller = "Following", Action = "UnFollow", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AttendacesAttend",
                routeTemplate: "api/Attendances/Attend/{id}",
                defaults: new { Controller="Attendances",Action= "Attend", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "AttendacesUnAttend",
                routeTemplate: "api/Attendanes/DeleteAttendance/{id}",
                defaults: new { Controller = "Attendances", Action = "DeleteAttendance", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "Notifications",
                routeTemplate: "api/Notifications/",
                defaults: new { Controller = "Notifications", Action = "GetUserNotifications" }
                );



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
