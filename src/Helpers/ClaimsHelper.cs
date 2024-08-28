using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace LogtoAuth.Helpers;

/// <summary>
/// Helper class for converting objects to <see cref="Claim"/> instances.
/// </summary>
public static class ClaimsHelper
{
    /// <summary>
    /// Converts an instance of type T to a list of Claims using the specified JsonTypeInfo.
    /// </summary>
    /// <typeparam name="T">The type of object to convert.</typeparam>
    /// <param name="instance">The instance of type T to convert.</param>
    /// <param name="jsonTypeInfo">The JsonTypeInfo used for serialization and deserialization.</param>
    /// <returns>A list of Claims representing the converted instance.</returns>
    public static List<Claim> ConvertToClaims<T>(T instance, JsonTypeInfo<T> jsonTypeInfo)
    {
        var json = JsonSerializer.Serialize(instance, jsonTypeInfo);
        var dictionary = JsonSerializer.Deserialize(json, NativeSerialization.Default.DictionaryStringObject);

        if (dictionary == null)
            return [];

        var claims = dictionary.Where(kvp => (object?)kvp.Value is not null).Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        return claims.ToList();
    }
}