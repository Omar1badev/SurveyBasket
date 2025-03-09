namespace SurveyBasket.Abstraction.Errors;

public static class UserErrors
{   public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid Email/Password", StatusCodes.Status401Unauthorized);
    public static readonly Error UserNotFound = new("User.UserNotFound", "User not found", StatusCodes.Status401Unauthorized);
}
