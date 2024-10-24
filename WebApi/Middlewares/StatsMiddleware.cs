using WebApi.Services;

namespace WebApi.Middlewares;

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseStatsMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<StatsMiddleware>();
}

public class StatsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly StatsService _statsService;

    public StatsMiddleware(RequestDelegate next, StatsService statsService)
    {
        _next = next;
        _statsService = statsService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value.Contains("Tasks"))
        {
            _statsService.IncrementStatsCounter();
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}


