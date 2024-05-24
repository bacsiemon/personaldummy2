using System.ComponentModel.DataAnnotations;

namespace Repositories.Dtos.EmployeeDtos
{
    public class GetEmployeeRequestDto
    {
        public int? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public string? DepartmentName { get; set; }

        [Required]
        public int PageNumber { get; set; } = 0;

        [Required]
        public int PageSize { get; set; } = 30;

        public string? JwtToken { get; set; }

        public string? RefreshToken { get; set; }
    }
}
