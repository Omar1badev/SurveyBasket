

namespace SurveyBasket.Persistence;

public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options , IHttpContextAccessor httpContextAccessor) : IdentityDbContext<ApplicataionUser, ApplicationRole , string>(options)
{
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

    public required DbSet<Poll> Polls { get; set; }
    public required DbSet<Question> Questions { get; set; }
    public required DbSet<Answer> Answers{ get; set; }
    public required DbSet<Vote> Votes { get; set; }
    public required DbSet<VoteAnswer> VoteAnswers{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        

            base.OnModelCreating(modelBuilder);

    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries<AuditableEntity>();

        foreach (var entity in entries)
        {
            var CurrentUserId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (entity.State == EntityState.Added)
            {
                entity.Property(mbox => mbox.UserId).CurrentValue = CurrentUserId!;
            }
            else if (entity.State == EntityState.Modified)
            {
                entity.Property(mbox => mbox.UpdatedUserId).CurrentValue = CurrentUserId;
                entity.Property(mbox => mbox.UpdatedAt).CurrentValue = DateTime.UtcNow;
            }

        }
        return base.SaveChangesAsync(cancellationToken);

    }
}
