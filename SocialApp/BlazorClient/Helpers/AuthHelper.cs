using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorClient.Helpers;

public static class AuthHelper
{
    public static async Task<ClaimsPrincipal?> GetUser(this AuthenticationStateProvider provider)
    {
        var authState = await provider.GetAuthenticationStateAsync();
        var user = authState.User;
        var identity = user.Identity;

        if (identity is null)
        {
            Console.WriteLine("Identity is null");
            return null;
        }

        if (!identity.IsAuthenticated)
        {
            Console.WriteLine("Not authenticated");
            return null;
        }

        return user;
    }
    
    public static async Task<string?> GetGoogleUserSub(this AuthenticationStateProvider provider)
    {
        var user = await provider.GetUser();

        if (user is not null)
        {
            var id = user.FindFirst(claim => claim.Type == "sub")?.Value;

            return id;
        }

        return null;
    }
}