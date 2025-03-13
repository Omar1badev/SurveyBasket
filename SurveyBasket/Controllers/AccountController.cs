using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Users;
using SurveyBasket.Services.User;

namespace SurveyBasket.Controllers;
[Route("me")]
[ApiController]
[Authorize]
public class AccountController(IUserService service) : ControllerBase
{
    private readonly IUserService service = service;

    [HttpGet("")]
    public async Task<IActionResult> ShowUserProfile ()
    {
        var result = await service.GetUserProfile(User.GetUserId()!);

        return Ok(result.Value);
    }

    [HttpPut("info")]
    public async Task<IActionResult> UpdateUserProfile([FromBody]UpdateUserProfileRequest request)
    {
        var result = await service.UpdateUserProfile(User.GetUserId()!,request);

        return NoContent();
    }

    [HttpPut("change-passord")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await service.ChangePassword(User.GetUserId()!, request);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
