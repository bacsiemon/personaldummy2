using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.Entities;
using Services.JWT;
using Services.JWT.Impl;
using Services.User.RequestEntities;
using Services.User.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.User.Impl
{
    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userMng;
        private readonly SignInManager<AppUser> _signInMng;
        private readonly ITokenService _tokenSvc;

        private readonly string USERNAME_NOT_FOUND = "Username Not found";
        private readonly string INCORRECT_PASSWORD = "Incorrect Password";
        private readonly string CREATE_FAILED = "Create failed";

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userMng = userManager;
            _signInMng = signInManager;
            _tokenSvc = tokenService;
        }

        public async Task<LoginResponseEntity?> LoginAsync(string username, string password) {

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
                Token = _tokenSvc.CreateToken(user),
            };
        }

        public async Task<RegisterResponseEntity?> RegisterAsync(RegisterRequestEntity requestEnt)
        {
            var appUser = new AppUser()
            {
                UserName = requestEnt.Username,
                Email = requestEnt.Email,
            };

            string jsonString = System.IO.File.ReadAllText("PrivateKey.json");
            var key = JsonConvert.DeserializeObject<PrivateKey>(jsonString);

            string role = "";
            bool keyVerification = false;
            if (requestEnt.PrivateKey.Equals(key.SU)) { role = "SuperUser"; keyVerification = true; } 
            if (requestEnt.PrivateKey.Equals(key.HR)) { role = "HumanResource"; keyVerification = true; } 
            if (requestEnt.PrivateKey.Equals(key.EM)) { role = "Employee"; keyVerification = true; } 

            if (!keyVerification) return new RegisterResponseEntity()
            {
                ErrorMessage = "Unauthorized",
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
                Token = _tokenSvc.CreateToken(appUser)
            } 
            : 
            new RegisterResponseEntity() {Result = roleResult };
            
            
        }
    }
}
