using FluentValidation;
using InnoShop.ProductManagment.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace InnoShop.ProductManagment.WebApi.ExceptionHandling
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ProblemDetails
            {
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier,
                Instance = context.Request.Path
            };

            if (exception is DomainException domainException)
            {
                var status = MapDomainExceptionToStatusCode(domainException);
                response.Status = status;
                response.Title = GetTitle(status);
                response.Detail = exception.Message;
                response.ErrorCode = ToErrorCode(domainException.GetType().Name);
                response.Type = $"https://httpwg.org/specs/rfc7231.html#status.{status}";
            }
            else if (exception is ValidationException fluentValidationException)
            {
                response.Status = StatusCodes.Status400BadRequest;
                response.Title = GetTitle(StatusCodes.Status400BadRequest);
                response.Detail = "One or more validation errors occurred.";
                response.ErrorCode = "VALIDATION_ERROR";
                response.Type = "https://httpwg.org/specs/rfc7231.html#status.400";

                var errors = fluentValidationException.Errors
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(f => f.ErrorMessage).ToArray()
                    );
                response.Errors = errors;
            }
            else
            {
                response.Status = StatusCodes.Status500InternalServerError;
                response.Title = GetTitle(StatusCodes.Status500InternalServerError);
                response.Detail = "An internal server error occurred.";
                response.ErrorCode = "INTERNAL_SERVER_ERROR";
                response.Type = "https://httpwg.org/specs/rfc7231.html#status.500";
            }

            context.Response.StatusCode = response.Status;
            return context.Response.WriteAsJsonAsync(response);
        }

        private static string GetTitle(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                422 => "Unprocessable Entity",
                500 => "Internal Server Error",
                _ => "An error occurred"
            };
        }

        private static int MapDomainExceptionToStatusCode(DomainException ex)
        {
            return ex switch
            {
                ProductNotFoundException _ => StatusCodes.Status404NotFound,
                UnauthorizedProductAccessException _ => StatusCodes.Status403Forbidden,
                InvalidPriceException _ => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };
        }

        private static string ToErrorCode(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return "UNKNOWN_ERROR";
            if (typeName.EndsWith("Exception"))
                typeName = typeName.Substring(0, typeName.Length - "Exception".Length);

            var snake = Regex.Replace(typeName, "([a-z0-9])([A-Z])", "$1_$2");
            var upper = snake.Replace("-", "_").ToUpperInvariant();
            return upper;
        }
    }
}