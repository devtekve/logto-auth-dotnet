using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LogtoAuth.Options;

/// <summary>
/// Options for the LogtoJwt authentication scheme.
/// </summary>
public class LogtoJwtOptions
{
    /// <summary>
    /// Provides options for customizing the JwtBearer authentication scheme.
    /// </summary>
    public Action<JwtBearerOptions> overridenJwtBearerOptions { get; set; } = _ => { };

    /// <summary>
    /// Gets or sets the delegate that will be invoked when a token is successfully validated.
    /// </summary>
    /// <remarks>
    /// This delegate can be used to perform additional actions or customize the behavior of the authentication process
    /// after the token has been successfully validated.
    /// </remarks>
    /// <example>
    /// <code>
    /// var options = new LogtoJwtOptions
    /// {
    ///     onTokenValidatedDelegate = async (context) =>
    ///     {
    ///         // Perform additional actions or customize behavior
    ///         // after the token has been successfully validated.
    ///         // For example, you can access the validated token using
    ///         // context.SecurityToken:
    ///         var token = context.SecurityToken;
    ///         // You can also access the claims of the authenticated user using
    ///         // context.Principal.Claims:
    ///         var claims = context.Principal.Claims;
    ///         // You can then perform any necessary actions or operations
    ///         // based on the validated token and claims.
    ///         // Ensure the delegate returns a Task to support asynchronous operations.
    ///         await Task.CompletedTask;
    ///     }
    /// };
    /// </code>
    /// </example>
    public Func<TokenValidatedContext, Task> OnTokenValidatedDelegate { get; set; } = _ => Task.CompletedTask;
}