using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Entities;
using Services.JWT;
using Services.UserServices.RequestEntities;
using Services.UserServices.ResponseEntities;

namespace Services.UserServices.Impl
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<AppUser> _userMng;
        private readonly SignInManager<AppUser> _signInMng;
        private readonly ITokenService _tokenSvc;

        private readonly string USERNAME_NOT_FOUND = "Username Not found";
        private readonly string INCORRECT_PASSWORD = "Incorrect Password";
        private readonly string CREATE_FAILED = "Create failed";
        private readonly string ACCESS_DENIED = "Access denied";

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userMng = userManager;
            _signInMng = signInManager;
            _tokenSvc = tokenService;
        }

        public async Task<LoginResponseEntity?> LoginAsync(string username, string password)
        {

            AppUser? user = await _userMng.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
            if (user == null)
                return new LoginResponseEntity() { ErrorMessage = USERNAME_NOT_FOUND };


            var result = await _signInMng.CheckPasswordSignInAsync(user, password, false /*LockoutOnFailure*/);
            if (!result.Succeeded)
                return new LoginResponseEntity() { ErrorMessage = INCORRECT_PASSWORD };


            return new LoginResponseEntity()
            {
                Username = user.UserName,
                Email = user.Email,
                AccessToken = _tokenSvc.CreateToken(user),
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<RegisterResponseEntity?> RegisterAsync(RegisterRequestEntity requestEnt)
        {


            string jsonString = System.IO.File.ReadAllText("PrivateKey.json");
            var key = JsonConvert.DeserializeObject<PrivateKey>(jsonString);

            string role = "";
            bool keyVerification = false;
            if (requestEnt.PrivateKey.Equals(key.SU)) { role = "SuperUser"; keyVerification = true; }
            if (requestEnt.PrivateKey.Equals(key.HR)) { role = "HumanResource"; keyVerification = true; }
            if (requestEnt.PrivateKey.Equals(key.EM)) { role = "Employee"; keyVerification = true; }

            if (!keyVerification) return new RegisterResponseEntity()
            {
                ErrorMessage = ACCESS_DENIED,
            };

            var appUser = new AppUser()
            {
                UserName = requestEnt.Username,
                Email = requestEnt.Email,
                RefreshToken = _tokenSvc.CreateRefreshToken(),
                RefreshTokenExpiryDate = DateTime.Now.AddDays(7),
            };

            var createdUser = await _userMng.CreateAsync(appUser, requestEnt.Password);
            if (!createdUser.Succeeded)
                return new RegisterResponseEntity()
                {
                    Result = createdUser,
                    ErrorMessage = CREATE_FAILED,
                };

            IdentityResult roleResult = await _userMng.AddToRoleAsync(appUser, role);
            return roleResult.Succeeded ? new RegisterResponseEntity()
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                AccessToken = _tokenSvc.CreateToken(appUser),
                RefreshToken = appUser.RefreshToken,
            }
            :
            new RegisterResponseEntity() { Result = roleResult };


        }
    }
}
