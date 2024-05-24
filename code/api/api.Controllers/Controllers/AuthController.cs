using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos.AppUserDtos;
using Services.UserServices;
using Services.UserServices.RequestEntities;
using Services.UserServices.ResponseEntities;

namespace api.Controllers.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var requestEnt = new RegisterRequestEntity()
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password,
                    PrivateKey = dto.PrivateKey,
                };

                RegisterResponseEntity? responseEnt = await _authService.RegisterAsync(requestEnt);
                return (null == responseEnt.ErrorMessage) ?
                    Ok(new AuthenticatedUserDto()
                    {
                        UserName = responseEnt.UserName,
                        Email = responseEnt.Email,
                        AccessToken = responseEnt.AccessToken
                    })
                    :
                    StatusCode(406, responseEnt.Result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            LoginResponseEntity result = await _authService.LoginAsync(dto.Username, dto.Password);

            if (result.ErrorMessage != null)
                return Unauthorized(result.ErrorMessage);

            return Ok(new AuthenticatedUserDto()
            {
                UserName = result.Username,
                Email = result.Email,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }
    }
}
