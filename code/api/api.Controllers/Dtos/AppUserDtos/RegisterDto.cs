using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Dtos.AppUserDtos
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(32)]
        public string PrivateKey { get; set; } = string.Empty;
    }
}
