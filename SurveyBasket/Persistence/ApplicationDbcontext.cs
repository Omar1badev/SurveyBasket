

namespace SurveyBasket.Persistence;

public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options , IHttpContextAccessor httpContextAccessor) : IdentityDbContext<ApplicataionUser>(options)
{
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

    public required DbSet<Poll> Polls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
