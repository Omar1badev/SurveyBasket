namespace SurveyBasket.Controllers;


[Route("/[controller]")]
[ApiController]

public class PollsController : ControllerBase
{
    [HttpGet("")]                                           
    public IActionResult GetAll()
    {
        return Ok();
    } 


    
    [HttpGet("{Id}")]                                           
    public IActionResult Get(int Id)
    {
        return Ok();
    }



    [HttpPost("")]
    public IActionResult add(int Id, PollRequest request)
    {
        return CreatedAtAction(nameof(Get),Id ,request);
    }

    
    [HttpPut("{Id}")]
    public IActionResult update(int Id , PollRequest request)
    {
        var response = request.Adapt<PollResponse>();
        return Ok(response);
    }
}
