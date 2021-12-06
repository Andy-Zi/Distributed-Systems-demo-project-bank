using Microsoft.Owin.Hosting;
using MyBank.Server.Backend;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace MyBank.Server.RestAPI
{
    public static class BankServiceConfiguration
    {
        public static void StartWebApp(string url,IUnityContainer container)
        {
            WebApp.Start(url, (appbuilder) =>
            {
                var config = new HttpConfiguration();
                config.DependencyResolver = new UnityDependencyResolver(container);
                Configure(config);
                appbuilder.UseWebApi(config);
            });
        }

        public static HttpConfiguration Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            return config;
        }
    }
}