namespace SurveyBasket.Abstraction.Errors;

public static class PollsErrors
{
    public static readonly Error InvalidCredentials = new("Invalid vredentials", "Invalid Poll Credentials");
    public static readonly Error DaplicatedTitle = new("Invalid vredentials", "Daplicated Poll Title");

}
