using SurveyBasket.Contracts.Users;

namespace SurveyBasket.Services.User;

public class UserServices(UserManager<ApplicataionUser> manager) : IUserService
{
    private readonly UserManager<ApplicataionUser> manager = manager;

    public async Task<Result<UserProfileResponse>> GetUserProfile(string id)
    {
        var user = await manager.Users
            .Where(i=>i.Id == id)
            .ProjectToType<UserProfileResponse>()
            .SingleAsync();
            ;

        return Result.Success(user);
    }
}
