
namespace SurveyBasket.Persistence.EntitiesConfigrations;

public class UserConfigration :IEntityTypeConfiguration<ApplicataionUser>
{
    public void Configure(EntityTypeBuilder<ApplicataionUser> builder)
    {
        builder
            .OwnsMany(p => p.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");


        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
    }
}

