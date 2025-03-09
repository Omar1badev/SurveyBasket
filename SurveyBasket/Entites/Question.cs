namespace SurveyBasket.Entites;

public sealed class Question
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int PollsId { get; set; }
    public bool IsActive { get; set; } = true;
    public Poll Polls { get; set; } = default!;
    public ICollection<Answer> Answers { get; set; } = [];

}
