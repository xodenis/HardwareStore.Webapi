using HardwareStore.Core.Dto;
using HardwareStore.Db.Models;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> Get(int userId);
        Task<CartDto> Add(int userId, int productId, int count);
        Task<CartDto> Remove(int userId, int productId);
        Task<CartDto> RemoveRange(int userId);
    }
}
