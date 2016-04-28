using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;
using Structura.Shared.Utilities;
using Structura.WebApiOwinBoilerPlate.WebService.WebApiInstrumentation;

namespace Structura.WebApiOwinBoilerPlate.WebService
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
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

            // StructureMap:
            // config.DependencyResolver = ServiceLocator.Get<StructureMapWebApiDependencyResolver>();

            // Elmah
            // config.Services.Add(typeof(IExceptionLogger), new ElmahWebApi2ExceptionLogger());

            // Make ./public the default root of the static files in our Web Application.
            //app.UseFileServer(new FileServerOptions
            //{
            //    RequestPath = new PathString(string.Empty),
            //    FileSystem = new PhysicalFileSystem("./public"),
            //    EnableDirectoryBrowsing = true,
            //});

            appBuilder.Use(async (env, next) =>
            {
                FormatLoggerAccessor.Locate().Debug($"{env.Request.Method} {env.Request.Path}");
                await next();
                FormatLoggerAccessor.Locate().Debug($"Response code: {env.Response.StatusCode}");
            });

            appBuilder.UseWebApi(configuration);

            configuration.EnsureInitialized();
        }
    }
}