using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace OnlineExamSystem_AliShahen_AhmedMansour.Services
{
    public class AuthenticationResult
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }

        public static AuthenticationResult Success() => new AuthenticationResult { Succeeded = true };
        public static AuthenticationResult Failure(string error) => new AuthenticationResult { Succeeded = false, Error = error };
    }
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ProtectedLocalStorage _localStorage;

        public AuthenticationService(
            AuthenticationStateProvider authStateProvider,
            ProtectedSessionStorage sessionStorage,
            ProtectedLocalStorage localStorage)
        {
            _authStateProvider = authStateProvider;
            _sessionStorage = sessionStorage;
            _localStorage = localStorage;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password, bool rememberMe)
        {
            // TODO: Add your actual authentication logic here (e.g., check against database)
            // For demo purposes, we'll accept any email/password combination
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return AuthenticationResult.Failure("Email and password are required.");
            }

            // Create claims for the user
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                // Add role claim based on email (for demo)
                new Claim(ClaimTypes.Role, email.Contains("admin") ? "Admin" : "Student")
            };

            var identity = new ClaimsIdentity(claims, "custom");
            var principal = new ClaimsPrincipal(identity);

            // Update the authentication state
            if (_authStateProvider is CustomAuthenticationStateProvider customProvider)
            {
                await customProvider.UpdateAuthenticationState(principal);
            }

            return AuthenticationResult.Success();
        }

        public async Task LogoutAsync()
        {
            if (_authStateProvider is CustomAuthenticationStateProvider customProvider)
            {
                await customProvider.UpdateAuthenticationState(null);
            }
        }
    }
} 