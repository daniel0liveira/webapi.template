using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

           // var formatters = config.Formatters;
           // formatters.Remove(formatters.XmlFormatter);

           // var jsonSettings = formatters.JsonFormatter.SerializerSettings;
           // jsonSettings.Formatting = Formatting.Indented;
           // jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

           // formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
           //     = ReferenceLoopHandling.Ignore;
           // formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
           //     = PreserveReferencesHandling.None;

           // formatters.JsonFormatter.SerializerSettings.Culture = new CultureInfo("pt-BR");

           //// config.MapHttpAttributeRoutes();

           // config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
           //     (new Newtonsoft.Json.Converters.StringEnumConverter());

           // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
