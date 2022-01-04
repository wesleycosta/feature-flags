namespace FeatureFlags;

public class FeatureFlagMiddleware
{

    private readonly RequestDelegate _next;

    public FeatureFlagMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Headers["version"] != "2.0")
        {
            httpContext.Response.StatusCode = 404;
            return;
        }

        await _next(httpContext);
    }
}
