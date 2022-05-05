using HardwareStore.Core.Dto;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface IUserService
    {
        Task<AuthInfo> Login(LoginRequest request);
        Task<RegisteredUser> Register(UserDto user);
        Task<UserInfoDto> GetInfo(int userId);
        Task<UserInfoDto> EditInfo(UserInfoDto info, int userId);
    }
}
