using Microsoft.EntityFrameworkCore;
using Repositories.Dtos.EmployeeDtos;
using Repositories.UOW;
using Services.EmployeeServices.ResponseEntities;

namespace Services.EmployeeServices.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uOW;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _uOW = unitOfWork;
        }

        public GetEmployeeSvcResponseEntity Get(GetEmployeeRequestDto requestDto)
        {
            var result = _uOW.Employees.GetAll().AsQueryable().Include(a => a.AppUser).Include(d => d.Department).AsQueryable();

            if (string.IsNullOrWhiteSpace( requestDto.EmployeeName))
            {
                result = result.Where(e => e.FullName.Contains(requestDto.EmployeeName, StringComparison.OrdinalIgnoreCase));
            }

            if (string.IsNullOrWhiteSpace(requestDto.DepartmentName))
            {
                result = result.Where(e => e.Department.DepartmentName.Contains(requestDto.DepartmentName, StringComparison.OrdinalIgnoreCase));
            }

            result = result.Skip((requestDto.PageNumber - 1 ) * requestDto.PageSize);

            var responseEnt = new GetEmployeeSvcResponseEntity()
            {
                Employees = result.ToList(),
            };

            return responseEnt;
        }

    }
}
