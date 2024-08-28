using System.Security.Claims;
using LogtoAuth.Models.Logto.api;

namespace LogtoAuth.Models;

/// <summary>
/// Represents the context for token validation in Logto.
/// </summary>
public class LogtoTokenValidatedContext(UserDetails userDetails, ClaimsIdentity claimsIdentity, IServiceProvider serviceProvider)
{
    /// <summary>
    /// Represents the details of a user.
    /// </summary>
    public UserDetails UserDetails { get; } = userDetails;

    /// <summary>
    /// Represents the claims associated with a user identity.
    /// </summary>
    public ClaimsIdentity ClaimsIdentity { get;  } = claimsIdentity;

    /// <summary>
    /// Represents a service provider.
    /// </summary>
    public IServiceProvider ServiceProvider { get; } = serviceProvider;
}