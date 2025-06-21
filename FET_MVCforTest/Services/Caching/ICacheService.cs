namespace FET_MVCforTest.Services.Caching
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expirationTime = null);
        void Remove(string key);
        bool Exists(string key);
    }
} 