
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Dtos.AppUserDtos;
using Repositories.Entities;
using Repositories.UOW;
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
        private readonly IUnitOfWork _uOW;

        private readonly string USERNAME_NOT_FOUND = "Username Not found";
        private readonly string INCORRECT_PASSWORD = "Incorrect Password";
        private readonly string CREATE_FAILED = "Create failed";
        private readonly string DEPT_NOT_FOUND = "Department not found";
        private readonly string ACCESS_DENIED = "Access denied";
        private readonly string DOB_ERROR = "Birth Date must be earlier than current date";

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IUnitOfWork uOW)
        {
            _userMng = userManager;
            _signInMng = signInManager;
            _tokenSvc = tokenService;
            _uOW = uOW;
        }

        public async Task<LoginResponseEntity?> LoginAsync(string username, string password)
        {

            AppUser? user = await _userMng.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
            if (user == null)
                return new LoginResponseEntity() { ErrorMessage = USERNAME_NOT_FOUND };


            SignInResult result = await _signInMng.CheckPasswordSignInAsync(user, password, false /*LockoutOnFailure*/);
            if (!result.Succeeded)
                return new LoginResponseEntity() { ErrorMessage = INCORRECT_PASSWORD };

            IList<string> roleList = await _userMng.GetRolesAsync(user);
            string firstRole = roleList.First();

            return new LoginResponseEntity()
            {
                Username = user.UserName,
                Email = user.Email,
                AccessToken = _tokenSvc.CreateAccessToken(user, firstRole),
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<RegisterResponseEntity?> RegisterAsync(RegisterDto requestDto)
        {
            if (requestDto.DateOfBirth > DateTime.Now) return new RegisterResponseEntity() { Errors = DOB_ERROR };

            string role = "";
            bool isRoleCorrect = false;
            if (requestDto.Role.Equals("SU")) { role = "SuperUser"; isRoleCorrect = true; }
            if (requestDto.Role.Equals("HR")) { role = "HumanResource"; isRoleCorrect = true; }
            if (requestDto.Role.Equals("EM")) { role = "Employee"; isRoleCorrect = true; }
            
            if (!isRoleCorrect) return new RegisterResponseEntity(){ Errors = CREATE_FAILED };

            Department? deptFindResult = _uOW.Departments.GetById(requestDto.DepartmenId);

            if (deptFindResult == null) return new RegisterResponseEntity() {Errors = DEPT_NOT_FOUND };

            var appUser = new AppUser()
            {
                UserName = requestDto.Username,
                Email = requestDto.Email,
                FullName = requestDto.FullName,
                DateOfBirth = requestDto.DateOfBirth,
                HomeAddress = requestDto.HomeAddress,
                JobPosition = requestDto.JobPosition,
                Salary = requestDto.Salary,
                DepartmentId = deptFindResult.Id,
                RefreshToken = _tokenSvc.CreateRefreshToken(),
                RefreshTokenExpiryDate = DateTime.Now.AddDays(7),
            };

            var createdUser = await _userMng.CreateAsync(appUser, requestDto.Password);
            if (!createdUser.Succeeded)
                return new RegisterResponseEntity()
                {
                    Result = createdUser,
                    Errors = CREATE_FAILED,
                };

            IdentityResult roleResult = await _userMng.AddToRoleAsync(appUser, role);

            return roleResult.Succeeded ?
                new RegisterResponseEntity()
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    FullName = appUser.FullName,
                    Role = role,
                    DateOfBirth = appUser.DateOfBirth,
                    HomeAddress = appUser.HomeAddress,
                    JobPosition = appUser.JobPosition,
                    Salary = appUser.Salary,
                    DepartmentName = deptFindResult.DepartmentName,
                    AccessToken = _tokenSvc.CreateAccessToken(appUser, role),
                    RefreshToken = appUser.RefreshToken,
                }
            :
            new RegisterResponseEntity() { Result = roleResult, Errors = CREATE_FAILED, };


        }

    }
}
