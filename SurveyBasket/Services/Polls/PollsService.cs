
using SurveyBasket.Abstraction;
using SurveyBasket.Abstraction.Errors;

namespace SurveyBasket.Services.Polls;

public class PollsService(ApplicationDbcontext dbcontext) : IPollsService
{
    public async Task<Result<PollResponse>> CreatePollAsync(PollRequest Request)
    {
        var poll = Request.Adapt<Poll>();

        await dbcontext.Polls.AddAsync(poll);
        await dbcontext.SaveChangesAsync();
        var response = poll.Adapt<PollResponse>();
        return Result.Success(response);
    }

    public async Task<Result> DeletePollAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var poll = await dbcontext.Polls.FindAsync(pollId,cancellationToken);

        if (poll is null)
            return Result.Failure(PollsErrors.InvalidCredentials);

        dbcontext.Polls.Remove(poll);
        dbcontext.SaveChanges();

        return Result.Success();
    }

    public async Task<Result<PollResponse>> GetPollByIdAsync(int pollId)
    {
        var poll = await dbcontext.Polls.FindAsync(pollId);
        
        var response = poll.Adapt<PollResponse>();

        return poll is not null ? 
            Result.Success(response) :
            Result.Failure<PollResponse>(PollsErrors.InvalidCredentials);
    }

    public async Task<Result<IEnumerable<PollResponse>>> GetPollsAsync()

    {
        var polls = await dbcontext.Polls.AsNoTracking().ToListAsync();

        var response = polls.Adapt<IEnumerable<PollResponse>>();

        return Result.Success(response);
    }

    public async Task<Result> ToggleStatus(int Id)
    {
        var poll = await dbcontext.Polls.FindAsync(Id);
        if(poll is null)
            return Result.Failure(PollsErrors.InvalidCredentials);

        poll.IsPublished = !poll.IsPublished;
        dbcontext.Polls.Update(poll);
        dbcontext.SaveChanges();

        return Result.Success();


    }

    public async Task<Result<PollResponse>> UpdatePollAsync(int pollId, PollRequest pollRequest)
    {
        var poll = await dbcontext.Polls.FindAsync(pollId);
        if (poll is null)
        return Result.Failure<PollResponse>(PollsErrors.InvalidCredentials);

        poll.Summary = pollRequest.Summary;
        poll.Title = pollRequest.Title;
        //poll.IsPublished = pollRequest.IsPublished;
        poll.EndsAt = pollRequest.EndsAt;
        poll.StartsAt= pollRequest.StartsAt;


        dbcontext.Polls.Update(poll);
        dbcontext.SaveChanges();

        var res = poll.Adapt<PollResponse>();

        return Result.Success(res);
    }
}
