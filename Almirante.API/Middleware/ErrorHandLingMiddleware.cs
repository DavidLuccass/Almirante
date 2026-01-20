using System.Net;
using System.Text.Json;

namespace Almirante.API.Middleware
{
    public class ErrorHandLingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandLingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var responde = new
            {
                error = "Ocorreu um erro inesperado no servidor.",
                details = exception.Message
            };

            var playload = JsonSerializer.Serialize(responde);
            return context.Response.WriteAsync(playload);
        }
    }
}