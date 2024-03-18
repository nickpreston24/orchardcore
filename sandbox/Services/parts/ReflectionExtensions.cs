using System.Reflection;
using CodeMechanic.Diagnostics;

namespace Orchard.Sandbox.Services;

public static class ReflectionExtensions
{
    public static ICollection<PropertyInfo> TryGetProperties<T>(
        this IDictionary<Type, ICollection<PropertyInfo>> property_cache
        , bool ignore_case = true
        , BindingFlags flags = BindingFlags.Default
        , params string[] exclusions
    )
    {
        var type = typeof(T);
        return TryGetProperties(
            property_cache,
            type
            , ignore_case
            , flags
            , exclusions
        );
    }


    public static ICollection<PropertyInfo> TryGetProperties(
        this IDictionary<Type, ICollection<PropertyInfo>> property_cache
        , Type objType
        , bool ignore_case = true
        , BindingFlags flags = BindingFlags.Default
        , params string[] exclusions
    )
    {
        ICollection<PropertyInfo> properties;

        lock (property_cache)
        {
            if (!property_cache.TryGetValue(objType, out properties))
            {
                $"Prop not found for {objType.Name} so running reflection".Dump();

                var type_props = objType.GetProperties();

                var lowercased_prop_names = type_props.Select(p => p.Name.ToLowerInvariant());

                var joined_names = lowercased_prop_names.Except(exclusions);

                properties = type_props
                    .Where(
                        property => property.CanWrite
                                    && joined_names.Contains(property.Name.ToLowerInvariant())
                    )
                    .ToList();

                property_cache.Add(objType, properties);

                property_cache.Count.Dump("propcache size");
            }
        }

        return properties;
    }

    public static object GetPropertyValue<T>(this T self, string propertyName)
    {
        Type type = self.GetType();
        PropertyInfo property = type.GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        var value = property.GetValue(self, null);
        return value;
    }
}