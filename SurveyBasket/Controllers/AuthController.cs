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

        if (response is null)
            return BadRequest(new { message = "Invalid email or password" });

        return Ok(response);
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.GetRefreshTokenAsync(request.Token, request.RefreshToken);

        if (response is null)
            return BadRequest(new { message = "Invalid token or refresh token" });

        return Ok(response);
    }
    
    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.RevokeRefreshTokenAsync(request.Token, request.RefreshToken);

        return response ? Ok() : BadRequest("operation failed");
    }
}
