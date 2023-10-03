using Grozn.Services.Dto;

namespace Grozn.Services.Interfaces;

public interface IAuthService
{

        Task<TokenResponse> LoginByUsername(string username, string password);
        Task<TokenResponse> LoginByEmail(string email, string password);
        Task<TokenResponse> GetRefreshToken(TokenResponse token);
        Task<TokenResponse> Register(string username, string password, string email);
    
}
