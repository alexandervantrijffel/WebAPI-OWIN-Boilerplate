using System.Web.Http.Filters;
using Structura.Shared.Utilities;

namespace Structura.WebApiOwinBoilerPlate.WebService.WebApiInstrumentation
{
    public class ExceptionLogger : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var content = AsyncHelper.RunSync(() => actionExecutedContext.Request.Content.ReadAsStringAsync());
            FormatLoggerAccessor.Locate().Error(actionExecutedContext.Exception, 
                $"\r\n{actionExecutedContext.Request.Method} {actionExecutedContext.Request.RequestUri}\r\n" +
                $"{actionExecutedContext.Request.Headers.ToLogString()}" +
                $" {content}\r\n");
        }
    }
}