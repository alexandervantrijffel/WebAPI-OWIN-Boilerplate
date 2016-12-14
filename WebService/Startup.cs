using Owin;
using Structura.Shared.Utilities;

namespace Structura.WebApiOwinBoilerPlate.WebService
{
    public partial class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            ConfigureWebApi(appBuilder);

            appBuilder.Use(async (env, next) =>
            {
                FormatLoggerAccessor.Locate().Debug($"{env.Request.Method} {env.Request.Path}");
                await next();
                FormatLoggerAccessor.Locate().Debug($"Response code: {env.Response.StatusCode}");
            });

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
        }
    }
}