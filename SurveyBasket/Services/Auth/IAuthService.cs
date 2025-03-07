
namespace SurveyBasket.Services.Auth;

public interface IAuthService 
{
    Task<AuthResponse?> SingInAsync(AuthRequest request);
    Task<AuthResponse?> GetRefreshTokenAsync(string Token,string RefreshToken);
    Task<bool> RevokeRefreshTokenAsync(string Token,string RefreshToken);
}
