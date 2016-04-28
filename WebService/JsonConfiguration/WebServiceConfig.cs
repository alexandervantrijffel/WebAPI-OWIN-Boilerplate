using CuttingEdge.Conditions;

namespace Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration
{
    /// <summary>
    /// Helper for retrieving settings defined in configuration file.
    /// </summary>
    public class WebServiceConfig
    {
        public string BaseUrl { get; set; }
        public int? Port { get; set; }

        public string FullHostUrl
        {
            get
            {
                var portArg = Port.HasValue ? ":" + Port : string.Empty;
                return $"{BaseUrl}{portArg}";
            }
        }

        public WebServiceConfig()
        {
        }

        public WebServiceConfig(dynamic config)
        {
            Condition.Requires(config.BaseUrl as string).IsNotNullOrEmpty(
                "Required BaseUrl setting not found in configuration file.");
            BaseUrl = config.BaseUrl;
            Port = config.Port;
        }
    }
}