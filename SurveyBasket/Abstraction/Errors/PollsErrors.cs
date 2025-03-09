namespace SurveyBasket.Abstraction.Errors;

public static class PollsErrors
{
    public static readonly Error InvalidCredentials = new("Invalid credentials", "Invalid Poll Credentials");
    public static readonly Error NotFound = new("Invalid credentials", "NotFound");
    public static readonly Error DaplicatedTitle = new("Invalid credentials", "Daplicated Poll Title");

}
