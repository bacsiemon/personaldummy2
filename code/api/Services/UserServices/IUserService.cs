using Repositories.Dtos.AppUserDtos;
using Services.UserServices.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public interface IUserService
    {
        GetUserSvcResponse Get(GetUserRequestDto requestDto);
    }
}
