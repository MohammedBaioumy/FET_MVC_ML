using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Logging;

namespace FET_MVCforTest.Services.ErrorHandling
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var errorDetails = exception switch
            {
                UnauthorizedAccessException _ => new ErrorDetails(
                    (int)HttpStatusCode.Unauthorized,
                    "Unauthorized access",
                    "You do not have permission to access this resource"),

                KeyNotFoundException _ => new ErrorDetails(
                    (int)HttpStatusCode.NotFound,
                    "Resource not found",
                    exception.Message),

                ArgumentException _ => new ErrorDetails(
                    (int)HttpStatusCode.BadRequest,
                    "Invalid argument",
                    exception.Message),

                _ => new ErrorDetails(
                    (int)HttpStatusCode.InternalServerError,
                    "Internal Server Error",
                    "An unexpected error occurred. Please try again later.")
            };

            context.Response.StatusCode = errorDetails.StatusCode;
            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
} 