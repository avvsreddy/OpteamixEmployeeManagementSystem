using System.Text.Json;

namespace OpteamixEmployeeManagementSystem.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
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
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    Message = ex.Message
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
