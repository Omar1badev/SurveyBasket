namespace SurveyBasket.Abstraction.Errors;

public static class UserErrors
{   public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid Email/Password");
    public static readonly Error UserNotFound = new("User.UserNotFound", "User not found");
    public static readonly Error UserAlreadyExists = new("User.UserAlreadyExists", "User already exists");
    public static readonly Error RefreshTokenNotFound = new("User.RefreshTokenNotFound", "Refresh token not found");
    public static readonly Error RefreshTokenNotActive = new("User.RefreshTokenNotActive", "Refresh token not active");
    public static readonly Error RefreshTokenNotValid = new("User.RefreshTokenNotValid", "Refresh token not valid");
    public static readonly Error RefreshTokenNotExpired = new("User.RefreshTokenNotExpired", "Refresh token not expired");
    public static readonly Error RefreshTokenRevoked = new("User.RefreshTokenRevoked", "Refresh token revoked");
    public static readonly Error RefreshTokenAlreadyUsed = new("User.RefreshTokenAlreadyUsed", "Refresh token already used");
    public static readonly Error RefreshTokenInvalidated = new("User.RefreshTokenInvalidated", "Refresh token invalidated");
    public static readonly Error RefreshTokenExpired = new("User.RefreshTokenExpired", "Refresh token expired");
    public static readonly Error RefreshTokenReplaced = new("User.RefreshTokenReplaced", "Refresh token replaced");
    public static readonly Error RefreshTokenRevokederror = new("User.RefreshTokenRevoked", "Refresh token revoked");
    public static readonly Error RefreshTokenRevokeFailed = new("User.RefreshTokenRevokeFailed", "Refresh token revoke failed");
    public static readonly Error RefreshTokenGenerationFailed = new("User.RefreshTokenGenerationFailed", "Refresh token generation failed");
    public static readonly Error RefreshTokenUpdateFailed = new("User.RefreshTokenUpdateFailed", "Refresh token update failed");
    public static readonly Error RefreshTokenDeletionFailed = new("User.RefreshTokenDeletionFailed", "Refresh token deletion failed");
    public static readonly Error RefreshTokenCreationFailed = new("User.RefreshTokenCreationFailed", "Refresh token creation failed");
    public static readonly Error RefreshTokenValidationFailed = new("User.RefreshTokenValidationFailed", "Refresh token validation failed");
    public static readonly Error RefreshTokenRevocationFailed = new("User.RefreshTokenRevocationFailed", "Refresh token revocation failed");
    public static readonly Error RefreshTokenInvalidationFailed = new("User.RefreshTokenInvalidationFailed", "Refresh token invalidation failed");
}
