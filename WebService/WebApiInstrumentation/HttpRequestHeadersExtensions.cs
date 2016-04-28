using System.Net.Http.Headers;
using System.Text;

namespace Structura.WebApiOwinBoilerPlate.WebService.WebApiInstrumentation
{
    public static class HttpRequestHeadersExtensions
    {
        public static string ToLogString(this HttpRequestHeaders headers)
        {
            var sbHeaders = new StringBuilder();
            foreach (var header in headers)
                sbHeaders.Append($"{header.Key}: {string.Join(",", header.Value)}\r\n");
            return sbHeaders.ToString();
        }
    }
}