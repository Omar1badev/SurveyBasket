
using SurveyBasket.Abstraction;
using SurveyBasket.Abstraction.Errors;
using System.Security.Cryptography;

namespace SurveyBasket.Services.Auth;

public class AuthService(UserManager<ApplicataionUser> manager,IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicataionUser> manager = manager;
    private readonly IJwtProvider jwtProvider = jwtProvider;
    private readonly int RefreshTokenExpiryDays = 60;

    public async Task<Result<AuthResponse>> SingInAsync(AuthRequest request)
    {
        var user = await manager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var TruePassword = await manager.CheckPasswordAsync(user, request.Password);

        if (!TruePassword)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var (Token, ExpiresIn) = jwtProvider.GenerateToken(user);

        var RefreshToken = GenerateRefreshToken();

        var RefreshExpiresIn = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);


        user.RefreshTokens.Add(new RefreshToken
        {
            Token = RefreshToken,
            ExpiresOn = RefreshExpiresIn,
            
        });

        await manager.UpdateAsync(user);

        var response = new AuthResponse(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            Token,
            ExpiresIn * 60,
            RefreshToken,
            RefreshExpiresIn
        );

        return Result.Success(response);

    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(220));
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken)
    {
        var UserId = jwtProvider.ValidateToken(Token);

        if (UserId is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var user = await manager.FindByIdAsync(UserId);

        if (user is null) 
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
        
        var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);

        if (UserRefreshToken is null)
            return Result.Failure<AuthResponse>(UserErrors.RefreshTokenExpired);

        UserRefreshToken.RevokedOn = DateTime.UtcNow;

        var (newToken, ExpiresIn) = jwtProvider.GenerateToken(user);

        var newRefreshToken = GenerateRefreshToken();

        var RefreshExpiresIn = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);


        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = RefreshExpiresIn,

        });

        await manager.UpdateAsync(user);

        var response = new AuthResponse(
            user.Id,
            user.Email!,
            user.FirstName,
            user.LastName,
            newToken,
            ExpiresIn * 60,
            newRefreshToken,
            RefreshExpiresIn
        );

        return Result.Success(response);
    }

    public async Task<Result> RevokeRefreshTokenAsync(string Token, string RefreshToken)
    {
        var UserId = jwtProvider.ValidateToken(Token);

        if (UserId is null)
            return Result.Failure(UserErrors.RefreshTokenInvalidated);

        var user = await manager.FindByIdAsync(UserId);

        if (user is null)
            return Result.Failure(UserErrors.InvalidCredentials);

        var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);

        if (UserRefreshToken is null)
            return Result.Failure(UserErrors.RefreshTokenNotActive);

        UserRefreshToken.RevokedOn = DateTime.UtcNow;

        await manager.UpdateAsync(user);
        return Result.Success();
    }
}
