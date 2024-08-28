using JetBrains.Annotations;
using LogtoAuth.Models;
using LogtoAuth.Options;
using LogtoAuth.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace LogtoAuth;

/// <summary>
/// Provides extension methods to configure LogtoAuth authentication.
/// </summary>
[UsedImplicitly]
public static class LogtoAuthExtensions
{
    /// <summary>
    /// Adds the LogtoAuth authentication scheme to the provided <see cref="AuthenticationBuilder"/> instance.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> instance to configure.</param>
    /// <param name="baseAddress">The base address to use when making requests.</param>
    /// <returns>The <see cref="LogtoAuthBuilder"/> instance.</returns>
    public static LogtoAuthBuilder AddLogtoAuth(this AuthenticationBuilder builder, string baseAddress) =>
        AddLogtoAuth(builder, options => options.BaseAddress = new Uri(baseAddress));

    /// <summary>
    /// Adds LogtoAuth authentication to the AuthenticationBuilder with the specified base address.
    /// </summary>
    /// <param name="builder">The AuthenticationBuilder.</param>
    /// <param name="baseAddress">The base address as a string.</param>
    /// <returns>A LogtoAuthBuilder object.</returns>
    public static LogtoAuthBuilder AddLogtoAuth(this AuthenticationBuilder builder, Uri baseAddress) =>
        AddLogtoAuth(builder, options => options.BaseAddress = baseAddress);

    /// <summary>
    /// Adds LogtoAuth authentication to the authentication pipeline.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> instance.</param>
    /// <param name="baseAddress">The base address of the LogtoAuth service.</param>
    /// <returns>A <see cref="LogtoAuthBuilder"/> instance.</returns>
    public static LogtoAuthBuilder AddLogtoAuth(this AuthenticationBuilder builder, Action<LogtoAuthOptions> configureOptions)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<LogtoSchemeOptions>, PostConfigureLogtoSchemeOptions>());
        builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<ILogtoService, LogtoService>());
        builder.Services.AddHttpClient(nameof(LogtoAuth)); // Include HttpClient registration
        var config = new LogtoAuthOptions();
        configureOptions(config);

        ValidateOptions(config);
        builder.Services.AddSingleton<LogtoAuthOptions>(_ => config);
        return new LogtoAuthBuilder(config);
    }

    /// <summary>
    /// Validates the LogtoAuth options.
    /// </summary>
    /// <param name="config">The LogtoAuthOptions to validate.</param>
    private static void ValidateOptions(LogtoAuthOptions config)
    {
        if (config.BaseAddress == null)
            throw new ArgumentNullException(nameof(config.BaseAddress), "BaseAddress is required");
    }
}

/// <summary>
/// Class responsible for post configuration of LogtoSchemeOptions.
/// </summary>
public class PostConfigureLogtoSchemeOptions : IPostConfigureOptions<LogtoSchemeOptions>
{
    /// <summary>
    /// Performs post-configuration of <see cref="LogtoSchemeOptions"/> after it has been bound to the specified name and options instance.
    /// </summary>
    /// <param name="name">The name of the authentication scheme being configured.</param>
    /// <param name="options">The <see cref="LogtoSchemeOptions"/> to be configured.</param>
    public void PostConfigure(string? name, LogtoSchemeOptions options)
    {
        // Ensure the Events property is not null
        options.Events ??= new LogtoTokenEvents();
    }
}