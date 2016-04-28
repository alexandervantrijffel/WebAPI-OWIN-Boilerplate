using System;
using System.IO;
using System.Reflection;

namespace Structura.WebApiOwinBoilerPlate.WebService.JsonConfiguration
{
    /// <summary>
    /// Service locator for accessing settings defined in configuration.json.
    /// Allows for manipulating settings in unit tests through the Config property.
    /// </summary>

    public static class JsonConfigAccessor
    {
        public static WebServiceConfig Config { get; private set; } = new WebServiceConfig();

        public static void Initialize()
        {
            var currentFolderUri = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)));
            dynamic config = JsonConfig.Config.ApplyJsonFromPath(Path.Combine(currentFolderUri.LocalPath, "configuration.json"));
            Config = new WebServiceConfig(config);
        }
    }
}