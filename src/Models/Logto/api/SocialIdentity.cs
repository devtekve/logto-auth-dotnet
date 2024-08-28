using System.Text.Json.Serialization;

namespace LogtoAuth.Models.Logto.api;

/// <summary>
/// Abstract class representing a social identity.
/// </summary>
public abstract class SocialIdentity(string userId, Details details)
{
    /// <summary>
    /// Gets the user ID.
    /// </summary>
    /// <remarks>
    /// This property represents the unique identifier of a user.
    /// </remarks>
    [JsonPropertyName("userid")] public string UserId { get; } = userId;

    /// <summary>
    /// Represents the details of a social identity.
    /// </summary>
    [JsonPropertyName("details")] public Details Details { get; } = details;
};