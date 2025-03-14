
using SurveyBasket.Abstraction.Consts;

namespace SurveyBasket.Persistence.EntitiesConfigrations;

public class RolesConfigration :IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {

        builder.HasData(
            [
                new ApplicationRole
                {
                    Id = DefaultRoles.AdminRoleId,
                    Name = DefaultRoles.Admin,
                    NormalizedName = DefaultRoles.Admin.ToUpper(),
                    IsDefault = false,
                    IsDeleted = false
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.MemberRoleId,
                    Name = DefaultRoles.Member,
                    NormalizedName = DefaultRoles.Member.ToUpper(),
                    IsDefault = true,
                    IsDeleted = false
                }
            ]
        );
        
    }
}

