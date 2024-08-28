using System.Text.Json.Nodes;

namespace LogtoAuth.Models.Logto.api;

/// <summary>
/// Represents the details of a user.
/// </summary>
public record Details(
    string Id,
    string Name,
    string Email,
    string Avatar,
    JsonObject RawData
);