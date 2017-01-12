using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Globalization;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(WebApi.Startup))]
namespace WebApi
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //throw new Exception("teste");
            var config = new HttpConfiguration();
            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureWebApi(config);
            app.UseWebApi(config);
            GlobalConfiguration.Configuration.Formatters.FormUrlEncodedFormatter.ReadBufferSize = 256 * 1024; // 256 KB
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            var formatters = config.Formatters;
            
            formatters.Remove(formatters.XmlFormatter);

            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.None;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = ReferenceLoopHandling.Ignore;
            formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
                = PreserveReferencesHandling.None;
            jsonSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            formatters.JsonFormatter.SerializerSettings.Culture = new CultureInfo("pt-BR");

            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
                (new Newtonsoft.Json.Converters.StringEnumConverter());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var enableCors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(enableCors);
            //config.EnableCors();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationProvider.AuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}