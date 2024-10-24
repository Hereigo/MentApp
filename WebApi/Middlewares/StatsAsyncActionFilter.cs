using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Middlewares
{
    public class StatsAsyncActionFilter : IAsyncActionFilter
    {
        private readonly StatsService _statsService;

        public StatsAsyncActionFilter(StatsService statsService)
        {
            _statsService = statsService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            _statsService.IncrementStatsCounter();

            await next();

            // Do something after the action executes.
            // ...
        }
    }
}
