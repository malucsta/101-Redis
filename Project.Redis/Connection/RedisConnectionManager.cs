using StackExchange.Redis;

namespace Project.Redis.Connection;

public class RedisConnectionManager : IRedisConnectionManager
{
    private static readonly Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
    {
        return ConnectionMultiplexer.Connect("redis"); // Substitua "localhost" pelo endereço do seu servidor Redis
    });

    public ConnectionMultiplexer GetConnection() => lazyConnection.Value;
}
