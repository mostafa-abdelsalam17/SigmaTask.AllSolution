using SigmaTaskAPI.Errors;
using System.Net;
using System.Text.Json;

namespace SigmaTaskAPI.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        //===========================
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                //1.log Exception in console App
                logger.LogError(ex, ex.Message);
                //2.log Exception in Data Base [Producton] only
                //3.select some feature for response[Content Type, status Code, Body]
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var responseObject = (env.IsDevelopment()) ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                //4.convert object to json
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(responseObject, options);

                //5.return body with json
                await context.Response.WriteAsync(json);
            }
        }
    }
}
