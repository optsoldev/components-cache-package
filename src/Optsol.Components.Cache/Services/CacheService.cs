using Optsol.Components.Cache.Connections;
using Optsol.Components.Cache.Exceptions;
using StackExchange.Redis;
using System;

namespace Optsol.Components.Cache.Services
{
    public class CacheService : ICacheService
    {
        #region Attributes

        private readonly IDatabase database;

        #endregion

        #region Constructors

        public CacheService(ICacheConnection connection)
        {
            database = connection?.GetDatabase() ?? throw new CacheServiceException(nameof(connection));
        }

        #endregion

        #region Methods

        public virtual T? Read<T>(string key) where T : class
        {
            try
            {
                var value = database.StringGet(key);

                if (value.HasValue is false)
                {
                    return default;
                }

                var valueToObject = value.ToString().To<T>();
                return valueToObject;
            }
            catch (Exception ex)
            {
                throw new CacheException("Erro on read the cache", ex);
            }
        }

        public virtual void Save<T>(KeyValuePair<string, T> data) where T : class
        {
            try
            {
                database.StringSet(data.Key, data.Value.ToJson());
            }
            catch (Exception ex)
            {
                throw new CacheException("Erro on save the cache", ex);
            }
        }

        public virtual void Save<T>(KeyValuePair<string, T> data, int expirationInMinutes) where T : class
        {
            try
            {
                database.StringSet(data.Key, data.Value.ToJson(), expiry: new TimeSpan(0, expirationInMinutes, 0));
            }
            catch (Exception ex)
            {
                throw new CacheException("Erro on save the cache", ex);
            }
        }

        public virtual void Delete(string key)
        {
            try
            {
                database.KeyDelete(key);
            }
            catch (Exception ex)
            {
                throw new CacheException("Erro on delete the cache", ex);
            }
        }

        #endregion
    }
}
