using SurveyBasket.Contracts.Users;

namespace SurveyBasket.Services.User;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetUserProfile(string id);
}
