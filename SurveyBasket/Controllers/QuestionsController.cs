using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Abstraction;
using SurveyBasket.Abstraction.Errors;
using SurveyBasket.Contracts.Questions;
using SurveyBasket.Services.Questions;

namespace SurveyBasket.Controllers;
[Route("polls/{PollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionsController(IQuestionService service) : ControllerBase
{
    private readonly IQuestionService service = service;




    [HttpPost]
    public async Task<IActionResult> AddAsync(int PollId, QuestionRequest request)
    {
        var result = await service.AddAsync(PollId, request);

        if (result.IsSuccess)
            //return CreatedAtAction(nameof(Get), new {PollId,result.Value.Id,result.Value});
           return Ok(result.Value);


        return result.ToProblem();
    }




    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(int PollId,int Id)
    {
        var result = await service.GetAsync(PollId, Id);

        if (result.IsSuccess) 
        return Ok(result.Value);

        return result.ToProblem();
    }



    [HttpGet("")]
    public async Task<IActionResult> GetAll(int PollId)
    {

        var result = await service.GetAllAsync(PollId);

        if (result.IsSuccess)
            return Ok(result.Value);


        return result.ToProblem();
    }
    
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateQuestion(int PollId, int Id ,QuestionRequest request)
    {

        var result = await service.UpdateAsync(PollId , Id , request);

        if (result.IsSuccess)
            return NoContent();


        return result.ToProblem();
    }
}
