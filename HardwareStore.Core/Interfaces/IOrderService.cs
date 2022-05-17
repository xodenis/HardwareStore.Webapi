using HardwareStore.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> Get(int userId);
        Task<OrderDto> Add(int userId, OrderShortDto orderProducts);
    }
}
