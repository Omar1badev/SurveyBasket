namespace SurveyBasket.Controllers;


[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("register")]

    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var response = await service.RegisterAsync(request);

        if (response is null)
            return BadRequest(new { message = "Invalid email or password" });

        return Ok(response);
    }
}
