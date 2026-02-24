namespace Prueba_ProductsEF.Middleware
{
    public class RequestLogsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogsMiddleware> _logger;

        public RequestLogsMiddleware(RequestDelegate next, ILogger<RequestLogsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //_logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
            Console.WriteLine($"Request: {context.Request.Method} in {context.Request.Path}");

            await _next(context);

            //_logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }
}
