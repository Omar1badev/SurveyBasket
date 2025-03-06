
namespace SurveyBasket.Services.Auth;

public class AuthService(UserManager<ApplicataionUser> manager,IJwtProvider jwtProvider) : IAuthService
{
    private readonly IJwtProvider jwtProvider = jwtProvider;

    public async Task<AuthResponse?> RegisterAsync(AuthRequest request)
    {
        var user = await manager.FindByEmailAsync(request.Email);

        if (user is null)
            return null;

        var TruePassword = await manager.CheckPasswordAsync(user, request.Password);

        if (!TruePassword)
            return null;

        var (Token, ExpiresIn) = jwtProvider.GenerateToken(user);

        return new AuthResponse(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            Token,
            ExpiresIn * 60
        );

    }
}
