using SurveyBasket.Contracts.Users;

namespace SurveyBasket.Services.User;

public class UserServices(UserManager<ApplicataionUser> manager) : IUserService
{
    private readonly UserManager<ApplicataionUser> manager = manager;

    public async Task<Result<UserProfileResponse>> GetUserProfile(string id)
    {
        var user = await manager.FindByIdAsync(id);

        return Result.Success(user.Adapt<UserProfileResponse>());
    }
}
