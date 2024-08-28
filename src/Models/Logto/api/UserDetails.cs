using System.Text.Json;
using System.Text.Json.Nodes;

namespace LogtoAuth.Models.Logto.api;

/// <summary>
/// Represents the details of a user.
/// </summary>
public class UserDetails(
    string? sub,
    string? id,
    string? username,
    string primaryEmail,
    string? primaryPhone,
    string name,
    string picture,
    Identities identities,
    long lastSignInAt,
    long createdAt,
    long updatedAt,
    string applicationId,
    bool isSuspended,
    JsonObject? customData
)
{
    /// <summary>
    /// Gets or sets the sub property.
    /// </summary>
    /// <value>The sub property.</value>
    public string? Sub { get; init; } = sub;

    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    /// <value>
    /// The ID of the user.
    /// </value>
    public string? Id { get; init; } = id;

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    /// <remarks>
    /// This property represents the username associated with the user. It is a string value and may be null if no username is provided.
    /// The username is typically used for identification and authentication purposes.
    /// </remarks>
    public string? Username { get; init; } = username;

    /// <summary>
    /// Gets or sets the primary email address of the user.
    /// </summary>
    /// <value>The primary email address.</value>
    public string PrimaryEmail { get; init; } = primaryEmail;

    /// <summary>
    /// Gets or sets the primary phone number of a user.
    /// </summary>
    /// <remarks>
    /// This property represents the primary phone number associated with a user's account.
    /// </remarks>
    public string? PrimaryPhone { get; init; } = primaryPhone;

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    /// <value>
    /// The name of the user.
    /// </value>
    public string Name { get; init; } = name;

    /// <summary>
    /// Represents a user's picture.
    /// </summary>
    public string Picture { get; init; } = picture;

    /// <summary>
    /// Represents the identities associated with a user.
    /// </summary>
    /// <remarks>
    /// Each identity represents a specific authentication provider for the user.
    /// </remarks>
    public Identities Identities { get; init; } = identities;

    /// <summary>
    /// Gets or sets the timestamp of the user's last sign-in.
    /// </summary>
    /// <remarks>
    /// This property represents the timestamp (in milliseconds since the Unix epoch) when the user last signed in to the application.
    /// </remarks>
    /// <value>
    /// The timestamp of the user's last sign-in.
    /// </value>
    public long LastSignInAt { get; init; } = lastSignInAt;

    /// <summary>
    /// Gets the timestamp when the user details were created.
    /// </summary>
    /// <value>
    /// The timestamp when the user details were created.
    /// </value>
    public long CreatedAt { get; init; } = createdAt;

    /// <summary>
    /// Gets or sets the last updated timestamp of the user details.
    /// </summary>
    public long UpdatedAt { get; init; } = updatedAt;

    /// <summary>
    /// Gets or sets the application ID associated with the user.
    /// </summary>
    /// <value>
    /// The application ID.
    /// </value>
    public string ApplicationId { get; init; } = applicationId;

    /// <summary>
    /// Gets or sets a value indicating whether the user account is suspended.
    /// </summary>
    /// <value>
    /// <c>true</c> if the user account is suspended; otherwise, <c>false</c>.
    /// </value>
    public bool IsSuspended { get; init; } = isSuspended;

    /// <summary>
    /// Gets or sets the custom data associated with the user.
    /// </summary>
    /// <remarks>
    /// This property allows you to store additional data specific to a user.
    /// It is represented as a JsonObject, which can contain various key-value pairs.
    /// The custom data can be used to store any additional information that is not covered by the standard user properties.
    /// </remarks>
    public JsonObject? CustomData { get; init; } = customData;

    /// <summary>
    /// Converts the UserDetails object to a JsonObject using the native serialization library.
    /// </summary>
    /// <returns>The converted JsonObject.</returns>
    public JsonObject ToJsonObject()
    {
        if (JsonSerializer.Serialize(this, NativeSerialization.Default.UserDetails) is not { } json)
            throw new JsonException();

        if (JsonSerializer.Deserialize(json, NativeSerialization.Default.JsonObject) is { } jsonObject)
            return jsonObject;
        throw new JsonException();
    }
}