using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Project.Infra.Components.Interfaces;
using Project.Infra.Components.Middlewares;
using Project.Redis;

namespace Project.Infra.Components;

public static class ExtensionMethods
{
    public static IServiceCollection BuildInfraComponents(this IServiceCollection services)
    {
        services.ConfigureRateLimiter();
        services.ConfigureCache();
        return services;
    }
    public static void ConfigureMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<RateLimitingMiddleware>();
    }

    private static IServiceCollection ConfigureCache(this IServiceCollection services)
    {
        Redis.ExtensionMethods.ConfigureRedisCache(services);
        return services;
    }

    private static IServiceCollection ConfigureRateLimiter(this IServiceCollection services)
    {
        services.AddSingleton<IRateLimiter, RedisRateLimiter>();
        return services;
    }
}
