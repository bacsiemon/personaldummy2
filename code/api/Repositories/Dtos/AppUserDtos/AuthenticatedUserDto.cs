namespace Repositories.Dtos.AppUserDtos
{
    public class AuthenticatedUserDto
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

    }
}
