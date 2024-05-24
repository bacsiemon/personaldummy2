using Repositories.Entities;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Dtos.AppUserDtos
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(32)]
        public string PrivateKey { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;


        public DateTime DateOfBirth { get; set; }


        public string HomeAddress { get; set; } = string.Empty;


        public string JobPosition { get; set; } = string.Empty;

        public int Salary { get; set; }

        public string DepartmentName {  get; set; } = string.Empty;
    }
}
