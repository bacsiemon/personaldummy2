
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string HomeAddress { get; set; } = string.Empty;
        public string JobPosition { get; set; } = string.Empty;
        public int Salary { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }


    }
}
