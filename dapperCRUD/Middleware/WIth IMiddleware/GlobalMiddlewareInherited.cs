using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace dapperCRUD.Middleware
{
    public class GlobalMiddlewareInherited : IMiddleware
    {
        private readonly ILogger<GlobalMiddlewaer> _logger;

        public GlobalMiddlewareInherited()
        {
        }

        public GlobalMiddlewareInherited(
            ILogger<GlobalMiddlewaer> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                //_logger.LogError("Succesfully done","Hi");
                _logger.LogInformation("Succesfully done");
                await next(context);
            }

            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            ///////////////////////////////////////////////////////////////////////////

            //catch(Exception ex)
            //{
            //    _logger.LogError(ex, ex.Message, "custom error");
            //    ProblemDetails problem = new()
            //    {
            //        Status = (int)HttpStatusCode.InternalServerError,
            //        Type = "My own type",
            //        Title = "An error occured",
            //        Detail = "A server error appeared"

            //    };
            //    string json = JsonSerializer.Serialize(problem);

            //    await context.Response.WriteAsync(json);
            //    context.Response.ContentType = "application/json";
            //}

        }



        //public async Task InvokeAsync(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message, "custom error");
        //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        //context.Response.StatusCode = (int) HttpStatusCode.ServiceUnavailable;

        //    }
        //    //_logger.LogInformation("Before request");

        //    //await next(context);

        //    //_logger.LogInformation("After request");
        //}
    }
}
