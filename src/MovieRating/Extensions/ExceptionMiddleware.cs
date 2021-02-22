using System;
using System.Net;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MovieRating.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}", ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception.GetType() == typeof(ShowException))
            {
                return SetContext(context, HttpStatusCode.BadRequest, exception.Message);
            }
            else
            {
                return SetContext(context, HttpStatusCode.InternalServerError, "Internal Server Error.");
            }
        }

        private Task SetContext(HttpContext context, HttpStatusCode code, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            }.ToString());
        }
    }
}
