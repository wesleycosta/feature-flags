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
        await _next(httpContext);
    }
}
