using StackExchange.Redis;

namespace Project.Redis.Connection;

internal interface IRedisConnectionManager
{
    ConnectionMultiplexer GetConnection();
}
