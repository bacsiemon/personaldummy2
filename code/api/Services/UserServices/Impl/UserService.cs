
using Microsoft.AspNetCore.Identity;
using Repositories.Dtos.AppUserDtos;
using Repositories.Entities;
using Repositories.UOW;
using Services.UserServices.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userMng;

        public UserService(UserManager<AppUser> userMng)
        {
            _userMng = userMng;
        }

        public GetUserSvcResponse Get(GetUserRequestDto requestDto)
        {
            IQueryable<AppUser> users = _userMng.Users.AsQueryable();

            //if (string.IsNullOrEmpty(requestDto.))

            return new GetUserSvcResponse();
        }
    }
}
