using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace dapperCRUD.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigurationExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            //if (app.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler(options =>
            //    {
            //        options.Run(async context =>
            //        {
            //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //            var ex = context.Features.Get<IExceptionHandlerFeature>();
            //            if (ex != null)
            //            {
            //                await context.Response.WriteAsync(ex.Error.Message);
            //            }
            //        });
            //    });
            //}
        }
    }
}
