namespace EmployeeManagementSystemAuth.CustomMiddleware
{
    public class RequestResponseLoggingMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log incoming request
            _logger.LogInformation("Request: {method} {url}", context.Request.Method, context.Request.Path);

            // Read and log request body (if applicable)
            if (context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();
                var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset stream position for the next middleware or controller to read
                _logger.LogInformation("Request Body: {body}", requestBody);
            }

            // Capture response body
            var originalResponseBodyStream = context.Response.Body;
            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                // Call the next middleware
                await _next(context);

                // Read response body and log it
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                _logger.LogInformation("Response: {statusCode} - {body}", context.Response.StatusCode, responseBody);

                // Reset stream position and copy to the original response stream
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }
    }
}
