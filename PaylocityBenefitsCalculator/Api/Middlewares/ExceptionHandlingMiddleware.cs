using Api.Models;
using Newtonsoft.Json;

namespace Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    // Constructor to initialize the middleware with the request delegate
    public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    // Middleware pipeline method to handle exceptions
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Invoke the next middleware in the pipeline
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            // Handle the exception
            await HandleExceptionAsync(context, ex);
        }
    }

    // Method to handle exceptions and return a JSON response
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Create an API response with error details
        var response = new ApiResponse<object>
        {
            Data = null,
            Success = false,
            Error = "Failed",
            Message = exception.Message
        };

        // Set the HTTP status code to 500 (Internal Server Error)
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        // Set the response content type to JSON
        context.Response.ContentType = "application/json";

        // Write the JSON response to the HTTP response stream
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}
