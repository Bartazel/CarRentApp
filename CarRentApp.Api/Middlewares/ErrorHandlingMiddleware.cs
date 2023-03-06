using CarRentApp.Api.Models;
using CarRentApp.Exceptions;
using FluentValidation;
using System.Text.Json;

namespace CarRentApp.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AddingReservationException ex)
            {
                await WriteResponse(400, ex.Message, context);
            }
            catch (AddingCarException ex)
            {
                await WriteResponse(400, ex.Message, context);
            }
            catch (ValidationException ex)
            {
                await WriteResponse(400, string.Join(" ", ex.Errors.Select(e => e.ErrorMessage)), context);
            }
            catch (Exception ex)
            {
                await WriteResponse(500, ex.Message, context);
            }
        }

        async Task WriteResponse(int statusCode, string message, HttpContext context)
        {
            ErrorResponse errorResponse = new(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            await context.Response.CompleteAsync();
        }
    }
}
