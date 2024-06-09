using System.Security.Claims;

namespace OnlineBankingApp.Common.Interface
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(string username);

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
