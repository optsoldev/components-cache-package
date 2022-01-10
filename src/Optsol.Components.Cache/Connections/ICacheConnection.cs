using StackExchange.Redis;

namespace Optsol.Components.Cache.Connections;

public interface ICacheConnection
{
    IDatabase GetDatabase();
}
