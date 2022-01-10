using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Optsol.Components.Cache.Resolvers;

public class IgnorePropertiesResolver : DefaultContractResolver
{
    private readonly HashSet<string> ignoreProps;
    public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
    {
        this.ignoreProps = new HashSet<string>(propNamesToIgnore.Select(s => s.ToLower()));
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);
        
        if (property.PropertyName is null)
            throw new InvalidOperationException($"{nameof(CreateProperty)}: PropertyName is null");

        if (this.ignoreProps.Contains(property.PropertyName.ToLower()))
        {
            property.ShouldSerialize = _ => false;
        }
        return property;
    }
}
