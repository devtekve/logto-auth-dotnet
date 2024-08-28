using LogtoAuth.Models.Logto.api;

namespace LogtoAuth.Service;

internal interface ILogtoService
{
    public Task<OneOf<UserDetails, Exception>> FetchUserInfoAsync(string opaqueToken);
}