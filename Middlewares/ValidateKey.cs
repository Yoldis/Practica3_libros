public class ValidateKeyMiddleware
{
    private const string ApiKeyHeader = "X-API-Key";

    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ValidateKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var apiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("La Api-key es requerida.");
            return;
        }

        var validKey = _configuration["ApiKey"];

        if (apiKey != validKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("La Api-key es inválida.");
            return;
        }

        await _next(context);
    }
}