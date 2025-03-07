using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Abstraction;
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
        var response = await service.GetPollsAsync();

        return response.IsSuccess ? Ok(response.Value) :
            response.ToProblem(StatusCodes.Status404NotFound);
    } 



    
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        var response = await service.GetPollByIdAsync(Id);

        return response.IsSuccess ? Ok(response.Value) :
        response.ToProblem(StatusCodes.Status404NotFound);


    }




    [HttpPost("")]
    public async Task<IActionResult> add(PollRequest request)
    {   
        var response = await service.CreatePollAsync(request);

        return response.IsSuccess ?
            Ok(response.Value) :
            response.ToProblem(StatusCodes.Status404NotFound);
        
    }

    

    [HttpPut("{Id}")]
    public async Task<IActionResult> update(int Id , PollRequest request)
    {

        var response =await service.UpdatePollAsync(Id, request);

        return response.IsSuccess ? Ok(response.Value) :
            response.ToProblem(StatusCodes.Status404NotFound);
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
        return response.IsSuccess ?
            Ok(response) :
            response.ToProblem(StatusCodes.Status404NotFound);
    }
}
