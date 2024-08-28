using System.Text.Json.Serialization;
using LogtoAuth.Models.Logto.api;

namespace LogtoAuth.Models.Logto.oidc;

/// <summary>
/// Provides serialization and deserialization options for OIDC response objects.
/// </summary>
[JsonSerializable(typeof(TokenResponse[]))]
[JsonSerializable(typeof(UserDetails[]))]
[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
public partial class OidcResponseSerialization : JsonSerializerContext;