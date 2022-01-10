namespace Optsol.Components.Cache.Exceptions;

public class CacheConnectionException : ArgumentNullException
{
    public CacheConnectionException(string? paramName) : base(paramName)
    {
    }
}
