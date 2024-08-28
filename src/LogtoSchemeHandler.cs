using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using LogtoAuth.Helpers;
using LogtoAuth.Models;
using LogtoAuth.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace LogtoAuth;

/// <summary>
/// Represents an authentication handler for LogtoScheme.
/// </summary>
internal class LogtoSchemeHandler(
    IOptionsMonitor<LogtoSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ILogtoService logtoService, 
    IServiceProvider serviceProvider)
    : AuthenticationHandler<LogtoSchemeOptions>(options, logger, encoder)
{
    /// <summary>
    /// Represents an instance of the JwtSecurityTokenHandler class.
    /// This class is responsible for handling JSON Web Tokens (JWT) from the provided authorization header.
    /// </summary>
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    // private readonly IOptionsMonitor<LogtoSchemeOptions> _options = options;

    /// <summary>
    /// The handler calls methods on the events which give the application control at certain points where processing is occurring.
    /// If it is not provided a default instance is supplied which does nothing when the methods are called.
    /// </summary>
    protected new LogtoTokenEvents Events
    {
        get => (LogtoTokenEvents)base.Events!;
        set => base.Events = value;
    }

    /// <summary>
    /// Creates an instance of <see cref="LogtoTokenEvents"/>.
    /// </summary>
    /// <returns>An instance of <see cref="Task{TResult}"/> with the created <see cref="LogtoTokenEvents"/> object.</returns>
    protected override Task<object> CreateEventsAsync() => Task.FromResult<object>(new LogtoTokenEvents());

    /// <summary>
    /// Handles the authentication process for Logto scheme.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous authentication operation.
    /// The task result contains the authentication result, which indicates whether the authentication was successful or failed.
    /// </returns>
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var value = Context.Request.Headers.Authorization.ToString().Split(" ").Last();
        var jwtToken = value;
        if (string.IsNullOrEmpty(jwtToken))
        {
            Logger.LogDebug("{JwtToken} was null!", nameof(jwtToken));
            return AuthenticateResult.Fail("Token was null!");
        }

        if (_jwtSecurityTokenHandler.CanReadToken(jwtToken))
            return AuthenticateResult.Fail("Token is a valid JWT token. We expected an opaque token.");
        
        var userInfoAsync = await logtoService.FetchUserInfoAsync(jwtToken);
        if (!userInfoAsync.TryPickT0(out var userDetails, out var exception))
            return AuthenticateResult.Fail(exception);
        
        if(userDetails is null)
            return AuthenticateResult.Fail("User info is null!");
        
        var userInfoClaims = ClaimsHelper.ConvertToClaims(userDetails, NativeSerialization.Default.UserDetails);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "Logto"),
            new(ClaimTypes.AuthenticationMethod, "Logto"),
            new(ClaimTypes.Sid, userDetails.Sub ?? throw new NullReferenceException("sub is null!")),
        }.Concat(userInfoClaims).ToList();
        
        var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
        
        var logtoTokenValidatedContext = new LogtoTokenValidatedContext(userDetails, claimsIdentity, serviceProvider);
        await Events.OnTokenValidated(logtoTokenValidatedContext);
        
        var principal = new ClaimsPrincipal(claimsIdentity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}

/// <summary>
/// Represents the options for the Logto authentication scheme.
/// </summary>
public class LogtoSchemeOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// The handler calls methods on the events which give the application control at certain points where processing is occurring.
    /// If it is not provided a default instance is supplied which does nothing when the methods are called.
    /// </summary>
    public new LogtoTokenEvents Events
    {
        get => (LogtoTokenEvents)base.Events!;
        set => base.Events = value;
    }
}