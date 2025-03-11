
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SurveyBasket.Abstraction;
using SurveyBasket.Abstraction.Errors;
using System.Security.Cryptography;

namespace SurveyBasket.Services.Auth;

public class AuthService(UserManager<ApplicataionUser> manager,
    SignInManager<ApplicataionUser> signInManager
    ,IJwtProvider jwtProvider,
    ILogger<AuthService> logger) : IAuthService
{
    private readonly UserManager<ApplicataionUser> manager = manager;
    private readonly SignInManager<ApplicataionUser> signInManager = signInManager;
    private readonly IJwtProvider jwtProvider = jwtProvider;
    private readonly ILogger<AuthService> logger = logger;
    private readonly int RefreshTokenExpiryDays = 60;

    public async Task<Result<AuthResponse>> SingInAsync(AuthRequest request)
    {

        if (await manager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
        //using user manager
        //var TruePassword = await manager.CheckPasswordAsync(user, request.Password);

        //if (!TruePassword)
        //    return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);



        //using signin manager
        var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (result.Succeeded) {
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

     return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials);

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
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

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
            return Result.Failure(UserErrors.InvalidCredentials);

        var user = await manager.FindByIdAsync(UserId);

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);

        if (UserRefreshToken is null)
            return Result.Failure(UserErrors.InvalidCredentials);

        UserRefreshToken.RevokedOn = DateTime.UtcNow;

        await manager.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        var emailisex = await manager.Users.AnyAsync(i=>i.Email == request.Email);

        if(emailisex)
            return Result.Failure(UserErrors.EmailAlreadyExist);

        var user =request.Adapt<ApplicataionUser>();

        var result = await manager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            var code = await manager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            logger.LogInformation("Configration code : {code}",code);



            return Result.Success();
        }
        var errors = result.Errors.First();
        return Result.Failure(new Error(errors.Code, errors.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result> ConfirmEmailAsync(ConfigrationEmailRequest request)
    {

        if(await manager.FindByIdAsync(request.UserId) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);


        if(user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfermation);

        var code = request.Code;
        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
        catch (FormatException)
        {

            return Result.Failure(UserErrors.InvalidCredentials);
        }
        

        var result = await manager.ConfirmEmailAsync(user,code);

        if (result.Succeeded)
        {
            return Result.Success();
        }
        var errors = result.Errors.First();
        return Result.Failure(new Error(errors.Code, errors.Description, StatusCodes.Status400BadRequest));

    }
}
