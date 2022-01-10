namespace Optsol.Components.Cache.Services;

public interface ICacheService
{
    T? Read<T>(string key) where T : class;

    void Save<T>(KeyValuePair<string, T> data) where T : class;

    void Save<T>(KeyValuePair<string, T> data, int expirationInMinutes) where T : class;

    void Delete(string key);
}
