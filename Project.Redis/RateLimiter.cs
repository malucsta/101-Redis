using Project.Infra.Components.Interfaces;
using Project.Redis.Connection;
using StackExchange.Redis;

namespace Project.Redis;
public class RedisRateLimiter : IRateLimiter
{
    private readonly IDatabase _database;
    private readonly TimeSpan _expirationTime; 

    public RedisRateLimiter()
    {
        var connection = new RedisConnectionManager().GetConnection();
        _database = connection.GetDatabase();

        var time = Environment.GetEnvironmentVariable("RateLimitExpirationInSeconds");
        
        _expirationTime = int.TryParse(time, out int expiration) 
            ? TimeSpan.FromSeconds(expiration) 
            : _expirationTime = TimeSpan.FromSeconds(300);

    }

    public async Task<bool> IsLimited(string key, int limit)
    {
        var currentCount = await _database.StringIncrementAsync(key);

        if (currentCount == 1)
            _database.KeyExpire(key, _expirationTime);

        return currentCount <= limit;
    }
}
