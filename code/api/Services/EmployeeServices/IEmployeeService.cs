using Repositories.Dtos.EmployeeDtos;
using Services.EmployeeServices.ResponseEntities;

namespace Services.EmployeeServices
{
    public interface IEmployeeService
    {
        GetEmployeeSvcResponseEntity Get(GetEmployeeRequestDto requestDto);


    }
}
