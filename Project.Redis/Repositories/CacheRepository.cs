using Project.Redis.Connection;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace Project.Redis.Repositories;

internal class CacheRepository : ICacheRepository
{
    private readonly IDatabase _database;
    private readonly IRedisConnectionManager _connectionManager;

    public CacheRepository(IRedisConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
        var connection = _connectionManager.GetConnection();
        _database = connection.GetDatabase();
    }

    public async Task<T?> Get<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (!value.IsNull)
            return Deserialize<T>(value!);

        return default;
    }

    public async Task Set<T>(string key, T value, TimeSpan expiry)
    {
        await _database.StringSetAsync(key, Serialize(value), expiry);
    }

    public async Task<bool> Remove(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }
    private byte[] Serialize<T>(T value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        return Encoding.UTF8.GetBytes(jsonString);
    }

    private T? Deserialize<T>(byte[] bytes)
    {
        var jsonString = Encoding.UTF8.GetString(bytes);
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}
