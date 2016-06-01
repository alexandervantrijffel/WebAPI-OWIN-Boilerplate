using System;
using System.Linq;
using Microsoft.Owin.Hosting;
using Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration;

namespace Structura.WebApiOwinBoilerPlate.WebService
{
    public class WindowsService : IService
    {
        private IDisposable _server;
        public void Start()
        {
	        var so = new StartOptions();
			foreach(var url in JsonConfigAccessor.Config.FullHostUrls)
				so.Urls.Add(url);
            _server = WebApp.Start<Startup>(so);
#if DEBUG
            try
            {
                System.Diagnostics.Process.Start(so.Urls.First() + "/api/testrest");
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