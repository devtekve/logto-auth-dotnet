using System.Text.Json.Serialization;
using LogtoAuth.Models.Logto.api;

namespace LogtoAuth;

/// <summary>
/// Provides native serialization functionality for the Logto authentication library.
/// </summary>
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(UserDetails[]))]
[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class NativeSerialization : JsonSerializerContext;