using Valera.Models;

namespace Valera.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
        string GenerateJwtToken(User user);
    }
}
