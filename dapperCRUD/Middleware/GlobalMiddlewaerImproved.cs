using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace dapperCRUD.Middleware
{
    public class GlobalMiddlewareImproved
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalMiddlewaer> _logger;

        public GlobalMiddlewareImproved(
            RequestDelegate next,
            ILogger<GlobalMiddlewaer> logger)
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
                _logger.LogError(ex, ex.Message, "custom error");
                //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "My own type",
                    Title = "An error occured",
                    Detail = "A server error appeared"

                };
                string json = JsonSerializer.Serialize(problem);

                await context.Response.WriteAsync(json);
                context.Response.ContentType = "application/json";


            }
            //_logger.LogInformation("Before request");

            //await next(context);

            //_logger.LogInformation("After request");
        }
    }
}
