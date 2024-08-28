namespace LogtoAuth.Models.Logto.api;

/// <summary>
/// Represents a social identity from GitHub.
/// </summary>
public class GitHub(string userId, Details details) : SocialIdentity(userId, details);
