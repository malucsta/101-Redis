using Microsoft.Extensions.DependencyInjection;
using Project.Redis.Connection;
using Project.Redis.Repositories;

namespace Project.Redis;

public static class ExtensionMethods
{
    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services)
    {
        services.AddSingleton<IRedisConnectionManager, RedisConnectionManager>();
        services.AddScoped<ICacheRepository, CacheRepository>();
        return services;
    }
}
