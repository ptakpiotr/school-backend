using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace SchoolAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                HttpStatusCode code;

                switch (true)
                {
                    case bool _ when context.Exception is UnauthorizedAccessException:
                        code = HttpStatusCode.Unauthorized;
                        break;

                    case bool _ when context.Exception is InvalidOperationException:
                        code = HttpStatusCode.BadRequest;
                        break;

                    default:
                        code = HttpStatusCode.InternalServerError;
                        break;
                }
                _logger.LogError(context.Exception, context.Exception.Message);
                context.Result = new ObjectResult(context.Exception.Message) { StatusCode = (int)code };
            }
        }
    }

}