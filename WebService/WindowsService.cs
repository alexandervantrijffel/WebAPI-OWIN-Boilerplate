using System;
using Microsoft.Owin.Hosting;
using Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration;

namespace Structura.WebApiOwinBoilerPlate.WebService
{
    public class WindowsService : IService
    {
        private IDisposable _server;
        public void Start()
        {
            _server = WebApp.Start<Startup>(JsonConfigAccessor.Config.FullHostUrl);
#if DEBUG
            try
            {
                System.Diagnostics.Process.Start(JsonConfigAccessor.Config.FullHostUrl + "/api/testrest");
            }
            catch (Exception)
            { }
#endif
        }
        public void Stop()
        {
            _server.Dispose();
        }
    }
}