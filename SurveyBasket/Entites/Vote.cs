namespace SurveyBasket.Entites;

public sealed class Vote
{
    public int Id { get; set; }
    public int PollId { get; set; }
    public string UserId { get; set; }
    public DateTime SubmitAt { get; set; } = DateTime.UtcNow;

    public Poll Poll { get; set; } = default!;
    public ApplicataionUser User { get; set; } = default!;

    public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];

}
