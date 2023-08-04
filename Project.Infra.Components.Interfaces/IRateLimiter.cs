namespace Project.Infra.Components.Interfaces;

public interface IRateLimiter
{
    Task<bool> IsLimited(string key, int limit);
}
