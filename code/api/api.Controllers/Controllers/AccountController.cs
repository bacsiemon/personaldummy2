using api.Controllers.Dtos.AppUserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services.JWT;
using Services.User;
using Services.User.RequestEntities;
using Services.User.ResponseEntities;

namespace api.Controllers.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var requestEnt = new RegisterRequestEntity()
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password,
                    PrivateKey = dto.PrivateKey,
                };

                RegisterResponseEntity? responseEnt = await _userService.RegisterAsync(requestEnt);
                return (null == responseEnt.ErrorMessage) ?
                    Ok(new NewUserDto()
                    {
                        UserName = responseEnt.UserName,
                        Email = responseEnt.Email,
                        Token = responseEnt.Token
                    })
                    : 
                    StatusCode(406, responseEnt.Result);

            }catch (Exception ex)
            {
                return StatusCode (500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            LoginResponseEntity result = await _userService.LoginAsync(dto.Username, dto.Password);

            if (result.ErrorMessage != null)
                return Unauthorized(result.ErrorMessage);



            return Ok(new NewUserDto()
            {
                UserName = result.Username,
                Email = result.Email,
                Token = result.Token
            }); 
        }
    }
}
