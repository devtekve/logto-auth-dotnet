using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using LogtoAuth.Models.Logto.api;
using LogtoAuth.Models.Logto.oidc;
using LogtoAuth.Options;

namespace LogtoAuth.Service;

internal class LogtoService(
    IHttpClientFactory httpClientFactory,
    LogtoAuthOptions logtoOauthOptions) : ILogtoService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(LogtoService));

    public async Task<OneOf<UserDetails, Exception>> FetchUserInfoAsync(string opaqueToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", opaqueToken);
        Debug.Assert(logtoOauthOptions.UserinfoEndpoint != null, "logtoOptions.Value.Endpoints.UserinfoEndpoint != null");
        var response = await _httpClient.GetAsync(logtoOauthOptions.UserinfoEndpoint);
        if (!response.IsSuccessStatusCode)
            return new Exception("Failed to fetch user details");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return new Exception("User not found");

        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize(content, OidcResponseSerialization.Default.UserDetails);
        if (user is null)
            return new Exception("User not found");

        return user;
    }
}