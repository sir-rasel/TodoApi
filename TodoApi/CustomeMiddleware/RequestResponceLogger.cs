using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TodoApi.CustomeMiddleware
{
    public class RequestResponceLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponceLogger(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponceLogger>();
        }

        public async Task Invoke(HttpContext context)
        {
            RequestLoggingInfo(context);
            await _next(context);
            ResponceLoggingInfo(context);
        }

        private void RequestLoggingInfo(HttpContext context)
        {
            _logger.LogInformation(
                "Request {0} {1} => {2}, Request incoming time = {}",
                context.Request?.Method,
                context.Request?.Path.Value,
                context.Response?.StatusCode,
                DateTime.Now);
        }

        private void ResponceLoggingInfo(HttpContext context)
        {
            _logger.LogInformation(
                "Request {0} {1} => {2}, Responce time / Request outgoing time = {}",
                context.Request?.Method,
                context.Request?.Path.Value,
                context.Response?.StatusCode,
                DateTime.Now);
        }

    }
}
