
using SurveyBasket.Abstraction.Consts;

namespace SurveyBasket.Persistence.EntitiesConfigrations;

public class RoleClaimConfigration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var permission = Permissions.GetAllPermissions();
        var addadminclaim = new List<IdentityRoleClaim<string>>();

        for (int i = 0; i < permission.Count; i++)
        {
            addadminclaim.Add(new IdentityRoleClaim<string>
            {
                Id = i + 1,
                RoleId = DefaultRoles.AdminRoleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission[i]
            });
        }

        builder.HasData(
            addadminclaim
        );
        
    }
}

