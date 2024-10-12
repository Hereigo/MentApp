using Microsoft.Extensions.DependencyInjection;

namespace Services.MediatR
{
    public static class MediatRDependencyHandler
    {
        public static IServiceCollection RegisterRequestHandlers(
        this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(MediatRDependencyHandler).Assembly));
        }
    }
}
