using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Entites;

namespace SurveyBasket.Controllers;


[Route("[controller]")]
[ApiController]
//[Authorize]
public class PollsController(IPollsService service) : ControllerBase
{
    private readonly IPollsService service = service;

    [HttpGet("")]
    
    public async Task<IActionResult> GetAll()
    {
        var polls = await service.GetPollsAsync();

        return polls.IsSuccess ? Ok(polls.Value) :
            Problem(statusCode:StatusCodes.Status404NotFound , title:polls.Error.Code, detail:polls.Error.Description);
    } 



    
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        var poll = await service.GetPollByIdAsync(Id);

        return poll.IsSuccess ? Ok(poll.Value) :
        Problem(statusCode: StatusCodes.Status404NotFound, title: poll.Error.Code, detail: poll.Error.Description);
        
    }




    [HttpPost("")]
    public async Task<IActionResult> add(PollRequest request)
    {   
        var response = await service.CreatePollAsync(request);

        return response.IsSuccess ? Ok(response.Value)
            : Problem(statusCode: StatusCodes.Status404NotFound, title: response.Error.Code, detail: response.Error.Description);
        ;
    }

    

    [HttpPut("{Id}")]
    public async Task<IActionResult> update(int Id , PollRequest request)
    {

        var response =await service.UpdatePollAsync(Id, request);

        return response.IsSuccess ? Ok(response.Value) :
            Problem(statusCode: StatusCodes.Status404NotFound, title: response.Error.Code, detail: response.Error.Description);
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
        return response.IsSuccess? Ok(response) :
            Problem(statusCode: StatusCodes.Status404NotFound, title: response.Error.Code, detail: response.Error.Description);
    }
}
