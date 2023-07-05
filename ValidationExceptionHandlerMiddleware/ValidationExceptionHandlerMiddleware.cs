using FluentValidation;
using Newtonsoft.Json;

public class ValidationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationExceptionHandlerMiddleware> _logger;

    public ValidationExceptionHandlerMiddleware(RequestDelegate next, ILogger<ValidationExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.Select(error => new { Field = error.PropertyName, Message = error.ErrorMessage });
            var response = new { Errors = errors };

            _logger.LogError(ex, "Validation error occurred");

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}