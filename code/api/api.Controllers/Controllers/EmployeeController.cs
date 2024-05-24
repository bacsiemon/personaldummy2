using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos.EmployeeDtos;
using Services.EmployeeServices;
using Services.EmployeeServices.ResponseEntities;

namespace api.Controllers.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeSvc;

        public EmployeeController(IEmployeeService employeeSvc)
        {
            _employeeSvc = employeeSvc;
        }



        [HttpGet("hr")]
        [Authorize(policy: "HumanResourceOrHigher")]
        public IActionResult HumanResourceAuthorizationTest()
        {
            return Ok("Human Resource Access verified");
        }

        [HttpGet("get")]
        public IActionResult Get([FromQuery] GetEmployeeRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            GetEmployeeSvcResponseEntity result = _employeeSvc.Get(requestDto);

            if (result.Errors.Any()) 
            {
                return StatusCode(500, result.Errors);
            }

            return Ok(result.Employees);
        }
    }
}
