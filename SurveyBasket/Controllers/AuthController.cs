using SurveyBasket.Abstraction;
using SurveyBasket.Contracts.Auth.RefreshToken;

namespace SurveyBasket.Controllers;


[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("login")]

    public async Task<IActionResult> login([FromBody] AuthRequest request)
    {
        var response = await service.SingInAsync(request);

        return response.IsSuccess ?
            Ok(response.Value) : 
            response.ToProblem();
    }

    [HttpPost("register")]

    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await service.RegisterAsync(request);

        return response.IsSuccess ?
            Ok() :
            response.ToProblem();
    }



    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.GetRefreshTokenAsync(request.Token, request.RefreshToken);

        return response.IsSuccess ?
            Ok(response.Value) :
            response.ToProblem();
    }
    


    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.RevokeRefreshTokenAsync(request.Token, request.RefreshToken);

        return response.IsSuccess ?
            Ok() :
            response.ToProblem();
    }
}
