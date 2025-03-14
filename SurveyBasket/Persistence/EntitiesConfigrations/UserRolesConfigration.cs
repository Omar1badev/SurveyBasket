
using SurveyBasket.Abstraction.Consts;

namespace SurveyBasket.Persistence.EntitiesConfigrations;

public class UserRolesConfigration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {

        builder.HasData(
            new IdentityUserRole<string>
                {
                    UserId = DefaultUsers.AdminId,
                    RoleId = DefaultRoles.AdminRoleId
                }
               
            
        );
        
    }
}

