using Project.Infra.Components.Interfaces;
using Project.Redis.Connection;
using StackExchange.Redis;

namespace Project.Redis;
public class RedisRateLimiter : IRateLimiter
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expirationTime; 

    public RedisRateLimiter(RedisSettings settings)
    {
        var connection = new RedisConnectionManager(settings).GetConnection();
        _database = connection.GetDatabase();

        _expirationTime = TimeSpan.FromSeconds(settings.RateLimitExpirationInSeconds);
    }

    public async Task<bool> IsLimited(string key, int limit)
    {
        var currentCount = await _database.StringIncrementAsync(key);

        if (currentCount == 1)
            _database.KeyExpire(key, _expirationTime);

        return currentCount <= limit;
    }
}
