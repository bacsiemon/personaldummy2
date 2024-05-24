using Services.UserServices.RequestEntities;
using Services.UserServices.ResponseEntities;

namespace Services.UserServices
{
    public interface IAuthService
    {
        Task<LoginResponseEntity?> LoginAsync(string username, string password);

        Task<RegisterResponseEntity?> RegisterAsync(RegisterRequestEntity requestEnt);
    }
}
