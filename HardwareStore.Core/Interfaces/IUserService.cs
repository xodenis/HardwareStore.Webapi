using HardwareStore.Core.Dto;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface IUserService
    {
        Task<AuthInfo> Login(LoginRequest request);

        Task<RegisteredUser> Register(UserDto user);
    }
}
