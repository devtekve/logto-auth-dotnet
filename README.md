# logto-auth-dotnet

Project to help developers add Logto authentication to their APIs.

## Important Note
This project is currently under development and is not yet ready for production use. I use it for my personal projects and will continue to improve it over time.

## Overview

This project provides a set of tools and libraries to integrate Logto authentication into your .NET applications. It includes middleware for ASP.NET Core to handle Logto opaque bearer tokens and JWT tokens, making it easier to secure your APIs with Logto.

## Features

- Easy integration with ASP.NET Core
- Support for both opaque and JWT tokens
- Customizable token validation
- Built-in serialization options for OIDC responses
- Always ready for NativeAOT
- Uses System.Text.Json for JSON serialization

## License

This project is licensed under the Mozilla Public License 2.0 (MPL-2.0). For more details, see the `LICENSE` file.

## Contributions

We welcome contributions from the community! If you have any ideas, suggestions, or bug reports, please open an issue or submit a pull request. We appreciate your help in making this project better.

## Acknowledgements

This project uses generative AI for documentation and code examples. We thank the contributors and the community for their support.

## Quick Examples

\<!-- Add your quick examples here -->

## Getting Started

To get started with integrating Logto authentication into your .NET application, follow these steps:

1. Install the necessary NuGet packages.
2. Configure the authentication middleware in your `Startup.cs` or `Program.cs`.
3. Customize the token validation options as needed.

For detailed instructions, refer to the documentation.

## Installation

To install the necessary packages, run the following command:

```shell
dotnet add package LogtoAuth
```

## Configuration
In your Startup.cs or Program.cs, configure the authentication middleware:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add services for Authentication
    var serviceAuthentication = services.AddAuthentication();
    
    // Configure Logto Authentication mechanism
    var logtoAuth = serviceAuthentication.AddLogtoAuth("https://logto.<your-domain>");
    
    // Register JWT and/or Opaque Token handlers
    var logtoJwt = logtoAuth.RegisterLogtoJwt(serviceAuthentication);
    var logtoOpaque = logtoAuth.RegisterLogtoOpaqueToken(serviceAuthentication, async context =>
    {
        // Custom logic for Opaque token validation
        Console.WriteLine(context.UserDetails.Identities.GitHub?.UserId);
        await Task.CompletedTask;
    });
    
    // Set default authorization policy that requires a user to be authenticated using ApiTokenSchemeHandler, JWT, or Opaque tokens
    var defaultAuthorizationPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(logtoJwt, logtoOpaque)
        .Build();
    
    // Set the default policy and add more, if necessary
    services.AddAuthorization(options => 
    {
        options.DefaultPolicy = defaultAuthorizationPolicy;
    });
}
```