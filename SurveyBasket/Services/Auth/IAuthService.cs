
using SurveyBasket.Abstraction;

namespace SurveyBasket.Services.Auth;

public interface IAuthService 
{
    Task<Result<AuthResponse>> SingInAsync(AuthRequest request);
    Task<Result> RegisterAsync(RegisterRequest request);
    Task<Result> ConfirmEmailAsync(ConfigrationEmailRequest request);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token,string RefreshToken);
    Task<Result> RevokeRefreshTokenAsync(string Token,string RefreshToken);
}
