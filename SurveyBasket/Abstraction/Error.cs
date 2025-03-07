namespace SurveyBasket.Abstraction;

public record Error(string Code , string Description)
{ 
    public static Error non => new (string.Empty,string.Empty);
}
