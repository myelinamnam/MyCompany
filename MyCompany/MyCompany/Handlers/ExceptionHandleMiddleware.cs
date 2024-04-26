using System.Text.Json;
using MyCompany.Core.Models;
using MyCompany.Core.Enums;

namespace MyCompany.Api.Handlers
{
    /// <summary>
    /// The exception handle middleware.
    /// </summary>
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandleMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandleMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The new instance of logger.</param>
        public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var result = new ProblemDetailsDto
            {
                Title = "Internal Server Error",
                Detail = ex.Message,
                Type = ex.GetType().ToString(),
                Instance = GetRoute(context.GetRouteData()),
            };

            if (ex is BadRequestException badinput)
            {
                result.Title = badinput.Title;
                result.ErrorCode = badinput.ErrorCode;
                result.Status = context.Response.StatusCode = StatusCodes.Status400BadRequest;

                _logger.LogDebug(ex, badinput.Message);
            }
            else if (ex is BadRequestException badrequest)
            {
                result.Title = badrequest.Title;
                result.ErrorCode = badrequest.ErrorCode;
                result.Status = context.Response.StatusCode = StatusCodes.Status400BadRequest;

                _logger.LogDebug(ex, badrequest.Message);
            }
            else if (ex is NotFoundException notfound)
            {
                result.Title = notfound.Title;
                result.ErrorCode = notfound.ErrorCode;
                result.Status = context.Response.StatusCode = StatusCodes.Status404NotFound;

                _logger.LogDebug(ex, notfound.Message);
            }
            else if (ex is UnauthorizedAccessException unauthorizedAccessException)
            {
                result.Title = "Unauthorized access.";
                result.ErrorCode = (int)ErrorTypes.UnauthorizedError;
                result.Status = context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                _logger.LogDebug(ex, unauthorizedAccessException.Message);
            }
            else if (ex is TimeoutException timeout)
            {
                result.Detail = "Database connection timeout.";
                result.ErrorCode = (int)ErrorTypes.UnhandledError;
                result.Status = context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                _logger.LogError(ex, timeout.Message);
            }
            else
            {
                result.Status = context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                result.ErrorCode = (int)ErrorTypes.UnhandledError;
                _logger.LogError(ex, ex.Message);
            }

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            };

            // write the response
            await context.Response.WriteAsync(JsonSerializer.Serialize(result, serializeOptions));
        }

        private string GetRoute(RouteData routeData)
        {
            return $"{routeData.Values["controller"]}/{routeData.Values["action"]}";
        }
    }
}
