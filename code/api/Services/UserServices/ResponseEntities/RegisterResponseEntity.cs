using Microsoft.AspNetCore.Identity;

namespace Services.UserServices.ResponseEntities
{
    public class RegisterResponseEntity
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public string? ErrorMessage { get; set; }

        public IdentityResult? Result { get; set; }
    }
}
