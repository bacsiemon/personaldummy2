using api.Controllers.Dtos;
using api.Controllers.Dtos.AppUserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Entities;
using Services.JWT;

namespace api.Controllers.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinMng;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInMng)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinMng = signInMng;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = dto.Username,
                    Email = dto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, dto.Password);
                if(!createdUser.Succeeded) return StatusCode(500, createdUser);

                string jsonString = System.IO.File.ReadAllText("PrivateKey.json");
                var key = JsonConvert.DeserializeObject<PrivateKey>(jsonString);

                string role = "Employee";
                if (dto.PrivateKey.Equals(key.SU)) role = "SuperUser";
                if (dto.PrivateKey.Equals(key.HR)) role = "HumanResource";
                IdentityResult roleResult = await _userManager.AddToRoleAsync(appUser, role);

                return roleResult.Succeeded ?
                    Ok(new NewUserDto()
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    })
                    : 
                    StatusCode(500, roleResult);

            }catch (Exception ex)
            {
                return StatusCode (500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.Equals( dto.Username));
            if (user == null) return Unauthorized("Username not found.");

            var result = await _signinMng.CheckPasswordSignInAsync(user, dto.Password, false /*LockoutOnFailure*/);
            if (!result.Succeeded) return Unauthorized("Incorrect Password");

            Response.Redirect
            return Ok(new NewUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
 
            
            
        }
    }
}
