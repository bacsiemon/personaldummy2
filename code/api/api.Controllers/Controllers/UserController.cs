using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos.AppUserDtos;
using Services.UserServices;
using Services.UserServices.ResponseEntities;

namespace api.Controllers.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSvc;

        public UserController(IUserService userSvc)
        {
            _userSvc = userSvc;
        }



        //[HttpGet("get")]
        //public IActionResult Get([FromQuery] GetUserRequestDto requestDto)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    GetUserSvcResponse result = _userSvc.Get(requestDto);

        //    if (result.Errors.Any()) 
        //    {
        //       return StatusCode(500, result.Errors);
        //    }

        //    return Ok(result.Employees);
        //}
    }
}
