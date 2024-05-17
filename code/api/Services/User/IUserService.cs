using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services.User.RequestEntities;
using Services.User.ResponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.User
{
    public interface IUserService
    {
        Task<LoginResponseEntity?> LoginAsync(string username, string password);

        Task<RegisterResponseEntity?> RegisterAsync(RegisterRequestEntity requestEnt);
    }
}
