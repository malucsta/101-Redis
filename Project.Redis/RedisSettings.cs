namespace Project.Redis;

public class RedisSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public int RateLimitExpirationInSeconds { get; set; } = 300;
}
