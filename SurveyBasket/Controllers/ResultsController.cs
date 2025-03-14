using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Services.AddResults;

namespace SurveyBasket.Controllers;
[Route("polls/{PollId}/[controller]")]
[ApiController]
[Authorize]
public class ResultsController(IResultService service) : ControllerBase
{
    private readonly IResultService service = service;

    [HttpGet("row-data")]
    public async Task<IActionResult> GetPollVotes(int PollId)
    {
        var result = await service.GetPollVotesAsynce(PollId);
        
        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("votes-per-day")]
    public async Task<IActionResult> GetVotesPerDay(int PollId)
    {
        var result = await service.GetVotesPerDayAsynce(PollId);

        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("votes-per-question")]
    public async Task<IActionResult> GetVotesPerQuestion(int PollId)
    {
        var result = await service.GetVotesPerQuestionAsynce(PollId);

        return result.IsSuccess ? Ok(result.Value)
            : result.ToProblem();
    }
}
