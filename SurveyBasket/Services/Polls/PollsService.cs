
namespace SurveyBasket.Services.Polls;

public class PollsService(ApplicationDbcontext dbcontext) : IPollsService
{
    public async Task<PollResponse> CreatePollAsync(PollRequest Request)
    {
        var poll = Request.Adapt<Poll>();

        await dbcontext.Polls.AddAsync(poll);
        await dbcontext.SaveChangesAsync();

        return poll.Adapt<PollResponse>();
    }

    public bool DeletePollAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var poll = dbcontext.Polls.Find(pollId,cancellationToken);

        if (poll is null)
        {
            return false;
        }

        dbcontext.Polls.Remove(poll);
        dbcontext.SaveChanges();
        return true;
    }

    public async Task<PollResponse> GetPollByIdAsync(int pollId)
    {
        var poll = await dbcontext.Polls.FindAsync(pollId);

        if (poll is null)
            return null;
        
        return poll.Adapt<PollResponse>();
    }

    public async Task<IEnumerable<PollResponse>> GetPollsAsync()

    {
        var polls = await dbcontext.Polls.AsNoTracking().ToListAsync();

        var response = polls.Adapt<IEnumerable<PollResponse>>();

        return response;
    }

    public async Task<bool> ToggleStatus(int Id)
    {
        var poll = await dbcontext.Polls.FindAsync(Id);
        if(poll is null)
            return false;

        poll.IsPublished = !poll.IsPublished;
        dbcontext.Polls.Update(poll);
        dbcontext.SaveChanges();

        return true;


    }

    public async Task<PollResponse> UpdatePollAsync(int pollId, PollRequest pollRequest)
    {
        var poll = await dbcontext.Polls.FindAsync(pollId);
        if (poll is null)
        return null;

        poll.Summary = pollRequest.Summary;
        poll.Title = pollRequest.Title;
        //poll.IsPublished = pollRequest.IsPublished;
        poll.EndsAt = pollRequest.EndsAt;
        poll.StartsAt= pollRequest.StartsAt;


        dbcontext.Polls.Update(poll);
        dbcontext.SaveChanges();

        var res = poll.Adapt<PollResponse>();

        return res;
    }
}
