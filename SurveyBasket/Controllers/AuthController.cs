using SurveyBasket.Contracts.Auth.RefreshToken;

namespace SurveyBasket.Controllers;


[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("login")]

    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var response = await service.SingInAsync(request);

        return response.IsSuccess? Ok(response.Value) : 
            Problem(statusCode: StatusCodes.Status404NotFound, title: response.Error.Code, detail: response.Error.Description);
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.GetRefreshTokenAsync(request.Token, request.RefreshToken);

        return response.IsSuccess ? Ok(response.Value) : 
            Problem(statusCode: StatusCodes.Status404NotFound, title: response.Error.Code, detail: response.Error.Description);
    }
    
    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.RevokeRefreshTokenAsync(request.Token, request.RefreshToken);

        return response.IsSuccess ? Ok() : Problem(statusCode: StatusCodes.Status404NotFound, title: response.Error.Code, detail: response.Error.Description);
    }
}
