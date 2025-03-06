using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Controllers;


[Route("[controller]")]
[ApiController]

public class PollsController(IPollsService service) : ControllerBase
{
    private readonly IPollsService service = service;

    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var polls = await service.GetPollsAsync();

        return Ok(polls);
    } 



    
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        var poll = await service.GetPollByIdAsync(Id);

        return Ok(poll);
    }




    [HttpPost("")]
    public async Task<IActionResult> add(PollRequest request)
    {   
        var response = await service.CreatePollAsync(request);

        return Ok(response);
    }

    

    [HttpPut("{Id}")]
    public async Task<IActionResult> update(int Id , PollRequest request)
    {

        var response =await service.UpdatePollAsync(Id, request);

        return Ok(response);
    }




    [HttpDelete("{Id}")]
    public IActionResult delete(int Id,CancellationToken cancellationToken)
    {
        var response = service.DeletePollAsync(Id,cancellationToken);
        return Ok(response);
    }

    [HttpPut("toggle/{Id}")]
    public async Task<IActionResult> Toggle(int Id)
    {
        var response = await service.ToggleStatus(Id);
        return Ok(response);
    }
}
