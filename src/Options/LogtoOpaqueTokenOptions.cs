using LogtoAuth.Models;

namespace LogtoAuth.Options;

/// <summary>
/// Represents the options for LogtoOpaqueToken authentication scheme.
/// </summary>
public class LogtoOpaqueTokenOptions
{
    /// <summary>
    /// Delegate for executing custom logic when a token is validated in the Logto authentication process.
    /// </summary>
    /// <param name="context">The context for token validation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Func<LogtoTokenValidatedContext, Task> OnTokenValidatedDelegate { get; set; } = _ => Task.CompletedTask;
}