using Optsol.Components.Cache.Exceptions;
using Optsol.Components.Cache.Settings;
using StackExchange.Redis;

namespace Optsol.Components.Cache.Connections;

public class DefaultConnection : ICacheConnection, IDisposable
{
    #region Attributes

    private bool disposed;
    private readonly ConnectionMultiplexer connectionMultiplexer;

    #endregion

    #region Constructors

    public DefaultConnection(CacheSettings settings)
        : this(settings.ConnectionString) { }

    public DefaultConnection(string? connectionString)
    {
        if (connectionString is null || string.IsNullOrEmpty(connectionString))
            throw new CacheConnectionException(nameof(connectionString));

        connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
    }

    #endregion

    #region Methods

    public IDatabase GetDatabase() => connectionMultiplexer.GetDatabase();

    public bool IsConnected() => connectionMultiplexer.IsConnected;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposed is false && disposing)
        {
            connectionMultiplexer.Dispose();
        }

        disposed = true;
    }

    #endregion
}
