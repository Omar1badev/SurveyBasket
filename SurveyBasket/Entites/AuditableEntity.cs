namespace SurveyBasket.Entites;

public class AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? UpdatedUserId { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public ApplicataionUser User { get; set; } = default!;
    public ApplicataionUser? UpdatedUser { get; set; } 

}
