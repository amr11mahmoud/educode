using Educode.Presentation.ApiModels;
using Newtonsoft.Json;

namespace Educode.Web.Middlewares
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;
            ApiResponse response = new ApiResponse();

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                await _next(context);
                context.Response.Body = currentBody;

                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                var objResult = JsonConvert.DeserializeObject(readToEnd);

                if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                {
                    response = ApiResponse.Create(objResult, true);
                }
                else if (context.Response.StatusCode >= 400)
                {
                    response = ApiResponse.Create(objResult, false);
                }

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }

    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapper>();
        }
    }
}
