namespace Optsol.Components.Cache.Exceptions;

public class CacheServiceException : ArgumentNullException
{
    public CacheServiceException(string? paramName) : base(paramName)
    {
    }
}
