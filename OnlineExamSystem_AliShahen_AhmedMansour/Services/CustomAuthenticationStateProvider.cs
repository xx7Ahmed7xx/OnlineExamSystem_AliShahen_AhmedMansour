using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Text.Json;

namespace OnlineExamSystem_AliShahen_AhmedMansour.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ProtectedLocalStorage _localStorage;

    public CustomAuthenticationStateProvider(
        ProtectedSessionStorage sessionStorage,
        ProtectedLocalStorage localStorage)
    {
        _sessionStorage = sessionStorage;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Try to get the stored claims from session storage first
            var sessionResult = await _sessionStorage.GetAsync<string>("user_claims");
            if (sessionResult.Success && !string.IsNullOrEmpty(sessionResult.Value))
            {
                var claims = JsonSerializer.Deserialize<List<ClaimData>>(sessionResult.Value);
                return new AuthenticationState(CreateClaimsPrincipal(claims));
            }

            // If not in session, try local storage
            var localResult = await _localStorage.GetAsync<string>("user_claims");
            if (localResult.Success && !string.IsNullOrEmpty(localResult.Value))
            {
                var claims = JsonSerializer.Deserialize<List<ClaimData>>(localResult.Value);
                return new AuthenticationState(CreateClaimsPrincipal(claims));
            }

            // If no claims are found, return unauthenticated state
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public async Task UpdateAuthenticationState(ClaimsPrincipal? principal)
    {
        ClaimsPrincipal claimsPrincipal = principal ?? new ClaimsPrincipal(new ClaimsIdentity());
        
        if (principal?.Identity?.IsAuthenticated == true)
        {
            // Store the claims
            var claims = principal.Claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value }).ToList();
            var claimsJson = JsonSerializer.Serialize(claims);
            
            // Store in the appropriate storage based on current user's state
            if (_currentUser.Identity?.IsAuthenticated == true)
            {
                await _localStorage.SetAsync("user_claims", claimsJson);
            }
            else
            {
                await _sessionStorage.SetAsync("user_claims", claimsJson);
            }
        }
        else
        {
            // Clear both storages on logout
            await _sessionStorage.DeleteAsync("user_claims");
            await _localStorage.DeleteAsync("user_claims");
        }

        _currentUser = claimsPrincipal;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private ClaimsPrincipal CreateClaimsPrincipal(List<ClaimData> claimData)
    {
        var claims = claimData.Select(c => new Claim(c.Type, c.Value));
        var identity = new ClaimsIdentity(claims, "custom");
        return new ClaimsPrincipal(identity);
    }

    private class ClaimData
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
} 