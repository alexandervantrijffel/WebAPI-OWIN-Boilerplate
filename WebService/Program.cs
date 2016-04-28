using System;
using System.Diagnostics;
using System.Reflection;
using log4net;
using Structura.Shared.Utilities;
using Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration;
using Topshelf;

namespace Structura.WebApiOwinBoilerPlate.WebService
{
    public class Program
    {
        static void Main(string[] args)
        {
            JsonConfigAccessor.Initialize();
            FormatLoggerAccessor.Initialize(() => LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;
            HostFactory.Run(x =>
            {
                x.Service<IService>(s =>
                {
                    s.ConstructUsing(name => new WindowsService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.UseLog4Net("log4net.config");
                x.SetDescription("Project template for Web API OWIN service application.");
                x.SetDisplayName("WebApiOwinBoilerplate service");
                x.SetServiceName("WebApiOwinBoilerplateService");
            });
        }

        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            FormatLoggerAccessor.Locate().Error("Global exception handler error", (Exception)e.ExceptionObject);

            if (Debugger.IsAttached)
                Debugger.Break();
        }
    }
}
