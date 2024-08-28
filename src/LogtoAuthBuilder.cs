using LogtoAuth.Models;
using LogtoAuth.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LogtoAuth;

/// <summary>
/// The LogtoAuthBuilder class provides methods to register LogtoAuth authentication schemes and options.
/// </summary>
public class LogtoAuthBuilder(LogtoAuthOptions logtoAuthOptionsOptions)
{
    /// <summary>
    /// Represents the name of the Logto JWT authentication scheme.
    /// </summary>
    private const string LogtoJwt = "Logto";

    /// <summary>
    /// Registers the LogtoJwt authentication scheme with the given <see cref="AuthenticationBuilder"/> and default options.
    /// </summary>
    /// <param name="serviceAuthentication">The <see cref="AuthenticationBuilder"/> used to register the authentication scheme.</param>
    /// <returns>The authentication scheme name.</returns>
    public string RegisterLogtoJwt(AuthenticationBuilder serviceAuthentication) =>
        RegisterLogtoJwt(serviceAuthentication, new LogtoJwtOptions());

    /// <summary>
    /// Registers the LogtoJwt authentication scheme with the specified custom options.
    /// </summary>
    /// <param name="serviceAuthentication">The authentication builder.</param>
    /// <param name="customOptions">The custom LogtoJwt options.</param>
    /// <returns>A string representing the registered authentication scheme.</returns>
    public string RegisterLogtoJwt(AuthenticationBuilder serviceAuthentication, Func<TokenValidatedContext, Task> onTokenValidatedDelegate) =>
        RegisterLogtoJwt(serviceAuthentication, new LogtoJwtOptions() { OnTokenValidatedDelegate = onTokenValidatedDelegate });

    /// <summary>
    /// Registers LogtoJwt authentication scheme with the specified options.
    /// </summary>
    /// <param name="serviceAuthentication">The authentication builder.</param>
    /// <param name="customOptions">The custom LogtoJwt options.</param>
    /// <returns>The name of the authentication scheme.</returns>
    public string RegisterLogtoJwt(AuthenticationBuilder serviceAuthentication, LogtoJwtOptions customOptions)
    {
        serviceAuthentication.AddJwtBearer(LogtoJwt, options =>
        {
            options.Authority = logtoAuthOptionsOptions.AuthorityEndpoint.ToString();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = logtoAuthOptionsOptions.AuthorityEndpoint.ToString(),
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                IssuerSigningKeyResolver = (_, _, _, _) =>
                {
                    var client = new HttpClient();
                    var keySetJson = client.GetStringAsync(logtoAuthOptionsOptions.JwksEndpoint).Result;
                    var keySet = new JsonWebKeySet(keySetJson);
                    return keySet.Keys;
                }
            };
            customOptions.overridenJwtBearerOptions(options);
            options.Events = new JwtBearerEvents { OnTokenValidated = customOptions.OnTokenValidatedDelegate };
        });

        // builder.AddScheme<LogtoSchemeOptions, LogtoSchemeHandler>(authenticationScheme, displayName, configureOptions);
        return LogtoJwt;
    }

    /// <summary>
    /// Registers the LogtoOpaqueToken authentication scheme.
    /// </summary>
    /// <param name="serviceAuthentication">The authentication builder.</param>
    /// <returns>The authentication scheme name.</returns>
    public string RegisterLogtoOpaqueToken(AuthenticationBuilder serviceAuthentication)
        => RegisterLogtoOpaqueToken(serviceAuthentication, new LogtoOpaqueTokenOptions());

    /// <summary>
    /// Registers LogtoOpaqueToken with the provided authentication service using the given options.
    /// </summary>
    /// <param name="serviceAuthentication">The authentication builder service.</param>
    /// <param name="onTokenValidatedDelegate">The delegate to be executed when a token is validated.</param>
    /// <returns>The opaque token registration result.</returns>
    public string RegisterLogtoOpaqueToken(AuthenticationBuilder serviceAuthentication, Func<LogtoTokenValidatedContext, Task> onTokenValidatedDelegate) 
        => RegisterLogtoOpaqueToken(serviceAuthentication, new LogtoOpaqueTokenOptions() { OnTokenValidatedDelegate = onTokenValidatedDelegate });

    /// <summary>
    /// Registers the LogtoOpaque authentication scheme with the provided service authentication.
    /// </summary>
    /// <param name="serviceAuthentication">The service authentication builder.</param>
    /// <returns>The name of the registered authentication scheme ("LogtoOpaque").</returns>
    public string RegisterLogtoOpaqueToken(AuthenticationBuilder serviceAuthentication, LogtoOpaqueTokenOptions customOptions)
    {
        serviceAuthentication.AddScheme<LogtoSchemeOptions, LogtoSchemeHandler>("LogtoOpaque",
            opt => { opt.Events = new LogtoTokenEvents { OnTokenValidated = customOptions.OnTokenValidatedDelegate }; });

        return "LogtoOpaque";
    }
}