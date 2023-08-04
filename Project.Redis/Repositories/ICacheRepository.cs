namespace Project.Redis.Repositories;

public interface ICacheRepository
{
    Task<T?> Get<T>(string key);
    Task Set<T>(string key, T value, TimeSpan expiry);
    Task<bool> Remove(string key);
}
