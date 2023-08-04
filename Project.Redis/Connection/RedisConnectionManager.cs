using StackExchange.Redis;

namespace Project.Redis.Connection;

public class RedisConnectionManager : IRedisConnectionManager
{
    private readonly Lazy<ConnectionMultiplexer> lazyConnection;

    public RedisConnectionManager(RedisSettings settings)
    {
        lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(settings.ConnectionString);
        });
    }

    public ConnectionMultiplexer GetConnection() => lazyConnection.Value;
}
