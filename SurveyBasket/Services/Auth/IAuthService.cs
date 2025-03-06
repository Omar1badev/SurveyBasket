
namespace SurveyBasket.Services.Auth;

public interface IAuthService 
{
    Task<AuthResponse?> RegisterAsync(AuthRequest request);
}
