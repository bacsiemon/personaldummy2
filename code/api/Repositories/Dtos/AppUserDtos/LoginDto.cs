using System.ComponentModel.DataAnnotations;

namespace Repositories.Dtos.AppUserDtos
{
    public class LoginDto
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
