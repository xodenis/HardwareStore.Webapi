using HardwareStore.Core.Dto;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface IFavoritesService
    {
        Task<FavoritesDto> Get(int userId);
        Task<FavoritesDto> Add(int userId, int productId);
        Task<FavoritesDto> Remove(int userId, int productId);
    }
}
