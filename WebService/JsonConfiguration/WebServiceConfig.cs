using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;

namespace Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration
{
    /// <summary>
    /// Helper for retrieving settings defined in configuration file.
    /// </summary>
    public class WebServiceConfig
    {
        public string[] BaseUrls { get; set; }
        public int? Port { get; set; }

        public IEnumerable<string> FullHostUrls
        {
            get
            {
                var portArg = Port.HasValue ? ":" + Port : string.Empty;
	            return BaseUrls.Select(b => b + portArg);
            }
        }

        public WebServiceConfig()
        {
        }

        public WebServiceConfig(dynamic config)
        {
			Condition.Requires((string[])config.BaseUrls).IsNotNull(
				"Required BaseUrls configuration file setting is not found.");
			Condition.Requires((string[])config.BaseUrls).IsNotEmpty(
                "Required BaseUrls configuration file setting is empty.");
            BaseUrls = config.BaseUrls;
            Port = config.Port;
        }
    }
}