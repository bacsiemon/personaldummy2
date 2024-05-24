using Repositories.Entities;

namespace Services.JWT
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

        string CreateRefreshToken();
    }
}
