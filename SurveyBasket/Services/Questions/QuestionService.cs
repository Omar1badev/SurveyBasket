using SurveyBasket.Abstraction;
using SurveyBasket.Abstraction.Errors;
using SurveyBasket.Contracts.Questions;

namespace SurveyBasket.Services.Questions;

public class QuestionService(ApplicationDbcontext dbcontext) : IQuestionService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result<QuestionResponse>> AddAsync(int PollId, QuestionRequest request)
    {
        var IsExpoll = await dbcontext.Polls.AnyAsync(r=>r.Id==PollId);

        if (!IsExpoll)
            return Result.Failure<QuestionResponse>(PollsErrors.NotFound);

        var questionIsexist = await dbcontext.Questions.AnyAsync(r => r.Content == request.Content && r.PollsId == PollId);

        if (questionIsexist)
            return Result.Failure<QuestionResponse>(QuestionErrors.DaplicatedTitle);

        var question = request.Adapt<Question>();
        question.PollsId = PollId;

        //request.Answers.ForEach(r => question.Answers.Add(new Answer { Content = r }));

        await dbcontext.Questions.AddAsync(question);
        await dbcontext.SaveChangesAsync();

        return Result.Success(question.Adapt<QuestionResponse>());

    }

    public async Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int PollId)
    {
        var pollIsExist =await dbcontext.Polls.AnyAsync(r => r.Id == PollId);

        if (!pollIsExist)
            return Result.Failure<IEnumerable<QuestionResponse>>(PollsErrors.NotFound);

        var questions = await dbcontext.Questions
            .Include(i => i.Answers)
            .Where(r => r.PollsId == PollId)
            .ProjectToType<QuestionResponse>()
            .AsNoTracking()
            .ToListAsync();

        return Result.Success<IEnumerable<QuestionResponse>>(questions);
    }

    public async Task<Result<QuestionResponse>> GetAsync(int PollId, int Id)
    {
        var pollIsExist = await dbcontext.Polls.AnyAsync(r => r.Id == PollId);

        if (!pollIsExist)
            return Result.Failure<QuestionResponse>(PollsErrors.NotFound);

        var question = await dbcontext.Questions
            .Where(r => r.PollsId == PollId && r.Id == Id)
            .Include(i=>i.Answers)
            .ProjectToType<QuestionResponse>()
            .SingleOrDefaultAsync();

        if (question == null)
            return Result.Failure<QuestionResponse>(QuestionErrors.InvalidCredentials);

        return Result.Success(question);

    }

    public async Task<Result> UpdateAsync(int PollId, int Id, QuestionRequest request)
    {
        var questionIsExist = await dbcontext.Questions.AnyAsync(
            f => f.PollsId == PollId
            && f.Id != Id 
            && f.Content == request.Content);

        if (questionIsExist)
            return Result.Failure(QuestionErrors.DaplicatedTitle);

        var question = await dbcontext.Questions.Include(i => i.Answers)
            .SingleOrDefaultAsync(r => r.PollsId == PollId && r.Id == Id);

        if (question == null)
            return Result.Failure(QuestionErrors.NotFound);

        question.Content = request.Content;

        var currentAnswers = question.Answers.Select(r => r.Content).ToList();

        var newAnswers = request.Answers.Except(currentAnswers).ToList();

        newAnswers.ForEach(r => question.Answers.Add(new Answer { Content = r }));

        question.Answers.ToList().ForEach(r =>
        {
            r.IsActive = request.Answers.Contains(r.Content);
        });
        await dbcontext.SaveChangesAsync();

        return Result.Success();

    }
}
