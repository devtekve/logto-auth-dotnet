namespace LogtoAuth.Models.Logto.api;

/// <summary>
/// Represents a Patreon social identity.
/// </summary>
public class Patreon(string userId, Details details) : SocialIdentity(userId, details);
