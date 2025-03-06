namespace SurveyBasket.Authentication;

public interface IJwtProvider
{
    (string Token, int Expiry) GenerateToken(ApplicataionUser user);
}
