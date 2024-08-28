namespace LogtoAuth.Options;

/// <summary>
/// The <see cref="Endpoints"/> class represents the endpoints used for authentication and authorization.
/// </summary>
public class Endpoints
{
    /// <summary>
    /// The default endpoint for the JSON Web Key Set (JWKS) document.
    /// </summary>
    private const string DefaultJwksEndpoint = "/oidc/jwks";

    /// <summary>
    /// The default URL endpoint for retrieving user information from the OIDC server.
    /// </summary>
    private const string DefaultUserinfoEndpoint = "/oidc/me";

    /// <summary>
    /// The default endpoint for authorization.
    /// </summary>
    private const string DefaultAuthorizeEndpoint = "/oidc/authorize";

    /// <summary>
    /// The default login endpoint for OIDC server.
    /// </summary>
    private const string DefaultLoginEndpoint = "/oidc/login";

    /// <summary>
    /// The default token endpoint path ("/oidc/token") relative to the base address of the OIDC server.
    /// </summary>
    private const string DefaultTokenEndpoint = "/oidc/token";


    /// <summary>
    /// The authorize endpoint of the OIDC server.
    /// This endpoint is used to initiate the authorization process.
    /// </summary>
    private readonly Uri? _authorizeEndpoint;

    /// <summary>
    /// Represents the login endpoint URL for the OIDC server.
    /// </summary>
    private readonly Uri? _loginEndpoint;

    /// <summary>
    /// The token endpoint of the OIDC server. This is the endpoint where the client can send requests to obtain access tokens.
    /// </summary>
    private readonly Uri? _tokenEndpoint;

    /// <summary>
    /// The endpoint for retrieving user information from the OIDC server.
    /// It defaults to "/oidc/me" if not specified.
    /// </summary>
    private readonly Uri? _userinfoEndpoint;

    /// <summary>
    /// Represents the URI endpoint for retrieving JWKS (JSON Web Key Set) from the OIDC (OpenID Connect) server.
    /// </summary>
    private readonly Uri? _jwksEndpoint;

    /// <summary>
    /// The base URL of the OIDC server. This property represents the root URL for all other endpoints.
    /// This property won't be null when the options are validated.
    /// </summary>
    public Uri? BaseAddress { get; set; }


    /// <summary>
    /// Gets or sets the JSON Web Key Set (JWKS) endpoint.
    /// This endpoint will be used to retrieve the public keys needed to validate the signatures of ID tokens received from the OIDC server.
    /// </summary>
    public Uri JwksEndpoint
    {
        get => new(BaseAddress!, _jwksEndpoint ?? new Uri(DefaultJwksEndpoint, UriKind.Relative));
        init => _jwksEndpoint = value;
    }

    /// <summary>
    /// The token endpoint of the OIDC server.
    /// </summary>
    /// <remarks>
    /// This property represents the URI of the token endpoint. It is used to request access tokens from the OIDC server.
    /// The token endpoint is typically used by clients with the "client_credentials" or "authorization_code" grant types to exchange client credentials or authorization codes for access tokens.
    /// </remarks>
    public Uri TokenEndpoint
    {
        get => new(BaseAddress!, _tokenEndpoint ?? new Uri(DefaultTokenEndpoint, UriKind.Relative));
        init => _tokenEndpoint = value;
    }

    /// <summary>
    /// The URL endpoint for login. This endpoint is used for initiating the login process with the OIDC server.
    /// </summary>
    public Uri LoginEndpoint
    {
        get => new(BaseAddress!, _loginEndpoint ?? new Uri(DefaultLoginEndpoint, UriKind.Relative));
        init => _loginEndpoint = value;
    }

    /// <summary>
    /// The authorization endpoint of the OIDC server.
    /// </summary>
    public Uri AuthorizeEndpoint
    {
        get => new(BaseAddress!, _authorizeEndpoint ?? new Uri(DefaultAuthorizeEndpoint, UriKind.Relative));
        init => _authorizeEndpoint = value;
    }

    /// <summary>
    /// Gets or sets the userinfo endpoint of the OIDC server. This endpoint is used to retrieve user information after authentication.
    /// </summary>
    /// <value>
    /// The userinfo endpoint URL. If not provided, the default value '/oidc/me' will be used.
    /// </value>
    public Uri? UserinfoEndpoint
    {
        get => new(BaseAddress!, _userinfoEndpoint ?? new Uri(DefaultUserinfoEndpoint, UriKind.Relative));
        init => _userinfoEndpoint = value;
    }
}