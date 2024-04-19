
using Dynamic.Json;

namespace MiddlewareDemo
{
    public class CheckAndParsingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;


        public async Task InvokeAsync(HttpContext context)
        {
            string pwd = context.Request.Query["password"]!;
            if (pwd == "123")
            {
                if (context.Request.HasJsonContentType())
                {
                    var reqStream = context.Request.BodyReader.AsStream();
                    dynamic? jsonObj = DJson.Parse(reqStream);
                    context.Items["BodyJson"] = jsonObj;
                }
                await next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
