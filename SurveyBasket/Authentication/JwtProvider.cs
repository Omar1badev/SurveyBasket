
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Authentication;

public class JwtProvider : IJwtProvider
{
    public (string Token, int Expiry) GenerateToken(ApplicataionUser user)
    {
        Claim[] claims = [
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.GivenName, user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName, user.LastName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];

        var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OSGnfaoseirj845e5rUIat4earihgjf84qeyth"));

        var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var Expiriesin = 60;

        var Expiry = DateTime.Now.AddMinutes(Expiriesin);


        var token = new JwtSecurityToken(
            issuer: "SurveyBasket App",
            audience: "SurveyBasket Users",
            claims: claims,
            expires: Expiry,
            signingCredentials: signingCredentials
        );

        return (Token : new JwtSecurityTokenHandler().WriteToken(token), Expiry: Expiriesin);
    }
}
