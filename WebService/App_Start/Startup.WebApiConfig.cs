using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;
using Structura.WebApiOwinBoilerPlate.WebService.WebApiInstrumentation;

namespace Structura.WebApiOwinBoilerPlate.WebService
{
    public partial class Startup
    {
        public void ConfigureWebApi(IAppBuilder appBuilder)
        {
			// Configure Web API for self-host. 
            var configuration = new HttpConfiguration();

            //  Enable attribute based routing
            //  http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            // Disable the Xml formatter
            configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // Use camel case for JSON data.
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            configuration.Filters.Add(new ExceptionLogger());
            configuration.Filters.Add(new ModelStateValidation());
            appBuilder.UseWebApi(configuration);
            configuration.EnsureInitialized();
        }
    }
}
