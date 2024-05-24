using Repositories.Entities;

namespace Services.EmployeeServices.ResponseEntities
{
    public class GetEmployeeSvcResponseEntity
    {
        public List<Employee> Employees { get; set; }

        public string? Errors { get; set; }
    }
}
