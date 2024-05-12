using api.Controllers.Dtos;
using api.Controllers.Dtos.AppUserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Entities;

namespace api.Controllers.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
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

                return roleResult.Succeeded ? Ok(roleResult) : StatusCode(500, roleResult);

            }catch (Exception ex)
            {
                return StatusCode (500, ex);
            }
        }

        
    }
}
