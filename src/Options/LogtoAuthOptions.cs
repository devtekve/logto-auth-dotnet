namespace LogtoAuth.Options;

/// <summary>
/// Represents the options for configuring LogtoAuth authentication.
/// </summary>
public class LogtoAuthOptions
{
    private const string DefaultJwksEndpoint = "/oidc/jwks";
    private const string DefaultUserinfoEndpoint = "/oidc/me";
    private const string DefaultAuthorizeEndpoint = "/oidc/authorize";
    private const string DefaultLoginEndpoint = "/oidc/login";
    private const string DefaultTokenEndpoint = "/oidc/token";
    private const string DefaultAuthorityEndpoint = "/oidc";


    private Uri? _authorizeEndpoint;
    private Uri? _loginEndpoint;
    private Uri? _tokenEndpoint;
    private Uri? _userinfoEndpoint;
    private Uri? _jwksEndpoint;
    private Uri? _authorityEndpoint;

    /// <summary>
    /// The base address of the OIDC server. This wont be null when the options are validated.
    /// </summary>
    public Uri? BaseAddress { get; set; }


    /// <summary>
    /// Gets or sets the endpoint for retrieving the JSON Web Key Set (JWKS) from the OIDC server.
    /// </summary>
    /// <remarks>
    /// The JWKS endpoint is used to obtain the public keys that can be used to verify the signature of ID tokens and other JWTs.
    /// </remarks>
    public Uri JwksEndpoint
    {
        get => new(BaseAddress!, _jwksEndpoint ?? new Uri(DefaultJwksEndpoint, UriKind.Relative));
        set => _jwksEndpoint = value;
    }

    /// <summary>
    /// Gets or sets the endpoint for token requests.
    /// This property represents the URI of the token endpoint of the OIDC server.
    /// If it is not explicitly set, the default value is "/oidc/token".
    /// </summary>
    public Uri TokenEndpoint
    {
        get => new(BaseAddress!, _tokenEndpoint ?? new Uri(DefaultTokenEndpoint, UriKind.Relative));
        set => _tokenEndpoint = value;
    }

    /// <summary>
    /// Gets or sets the login endpoint of the OIDC server.
    /// </summary>
    public Uri LoginEndpoint
    {
        get => new(BaseAddress!, _loginEndpoint ?? new Uri(DefaultLoginEndpoint, UriKind.Relative));
        set => _loginEndpoint = value;
    }

    /// <summary>
    /// Gets or sets the URI for the authorization endpoint of the OIDC server.
    /// </summary>
    public Uri AuthorizeEndpoint
    {
        get => new(BaseAddress!, _authorizeEndpoint ?? new Uri(DefaultAuthorizeEndpoint, UriKind.Relative));
        set => _authorizeEndpoint = value;
    }

    /// <summary>
    /// Gets or sets the userinfo endpoint of the OIDC server.
    /// </summary>
    public Uri? UserinfoEndpoint
    {
        get => new(BaseAddress!, _userinfoEndpoint ?? new Uri(DefaultUserinfoEndpoint, UriKind.Relative));
        set => _userinfoEndpoint = value;
    }

    /// <summary>
    /// Gets or sets the authority endpoint for Logto authentication.
    /// </summary>
    /// <value>
    /// The authority endpoint represents the URL where the Logto authentication server is located.
    /// </value>
    /// <remarks>
    /// This property should not be null when the options are validated.
    /// </remarks>
    public Uri AuthorityEndpoint
    {
        get => new(BaseAddress!, _authorityEndpoint ?? new Uri(DefaultAuthorityEndpoint, UriKind.Relative));
        set => _authorityEndpoint = value;
    }
};