using System.Text.Json.Serialization;

namespace LogtoAuth.Models.Logto.api;

/// <summary>
/// Represents a user's identities.
/// </summary>
public record Identities(
    [property: JsonPropertyName("github")] GitHub? GitHub,
    [property: JsonPropertyName("patreon")] Patreon? Patreon
);