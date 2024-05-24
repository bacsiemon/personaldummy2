namespace Repositories.Dtos.AppUserDtos
{
    public class RegisterResponseDto
    {

        public string? Id { get; set; }
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; }

        public DateTime DateOfBirth { get; set; }


        public string HomeAddress { get; set; } = string.Empty;


        public string JobPosition { get; set; } = string.Empty;

        public int Salary { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

    }
}
