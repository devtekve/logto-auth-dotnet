namespace LogtoAuth.Models.Logto.oidc;

/// <summary>
/// Represents a token response object received from the server.
/// </summary>
public class TokenResponse
{
    /// <summary>
    /// Represents an access token.
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the token in seconds.
    /// </summary>
    /// <value>
    /// The expiration time in seconds.
    /// </value>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Represents the type of token.
    /// </summary>
    public required string TokenType { get; set; }

    /// <summary>
    /// Gets or sets the scope requested by the client.
    /// </summary>
    public required string Scope { get; set; }
}