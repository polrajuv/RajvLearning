using System.Net;
using System.Text.Json;
namespace RajvLearning.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                var controller = context.GetRouteValue("controller");
                var action = context.GetRouteValue("action");

                _logger.LogError(
                    ex,
                    "Unhandled exception in Controller: {Controller}, Action: {Action}",
                    controller,
                    action);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred. Please try again later...."
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
