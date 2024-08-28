namespace LogtoAuth.Models;

/// <summary>
/// Represents the event handler for token validation in the Logto authentication scheme.
/// </summary>
public class LogtoTokenEvents
{
    /// <summary>
    /// Gets or sets the callback function that is invoked after the security token has passed validation and a ClaimsIdentity has been generated.
    /// </summary>
    public Func<LogtoTokenValidatedContext, Task> OnTokenValidated { get; set; } = _ => Task.CompletedTask;
}