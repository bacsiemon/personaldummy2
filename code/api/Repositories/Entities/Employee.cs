namespace Repositories.Entities
{
    public class Employee
    {

        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;


        public DateTime DateOfBirth { get; set; }


        public string HomeAddress { get; set; } = string.Empty;


        public string JobPosition { get; set; } = string.Empty;

        public int Salary { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        public string? AppUserId { get; set; }

        public AppUser? AppUser { get; set; }

    }
}
