using System.Net;
using System.Text.Json;
using CQRS.Shared.Exceptions;
using CQRS.Shared.Exceptions;

namespace CQRS.Api.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            var response = JsonSerializer.Serialize(new {Errors = ex.Message});
            await context.Response.WriteAsync(response);
        }
        catch (UnproccessableEntityException ex)
        {
            context.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
            context.Response.ContentType = "application/json";

            var response = JsonSerializer.Serialize(new {Errors = ex.Message});
            await context.Response.WriteAsync(response);
        }
        catch (ExistingEntityException ex)
        {
            context.Response.StatusCode = (int) HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            var response = JsonSerializer.Serialize(new {Errors = ex.Message});
            await context.Response.WriteAsync(response);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Error = "Une erreur interne est survenue." });
        }
    }
}