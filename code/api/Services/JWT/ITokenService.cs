using Repositories.Entities;

namespace Services.JWT
{
    public interface ITokenService
    {
        string CreateAccessToken(AppUser user, string role);

        string CreateRefreshToken();
    }
}
