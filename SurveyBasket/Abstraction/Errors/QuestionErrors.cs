namespace SurveyBasket.Abstraction.Errors;

public static class QuestionErrors
{
    public static readonly Error InvalidCredentials = new("Invalid credentials", "Invalid Question Credentials");
    public static readonly Error DaplicatedTitle = new("Invalid credentials", "Daplicated Question info");
    public static readonly Error NotFound = new("Invalid credentials", "Question not found");

}
