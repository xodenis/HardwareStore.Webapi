using HardwareStore.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductFullDto> GetById(int productId);
        Task<List<ProductFullDto>> GetAll();
        Task<ProductFullDto> Add(ProductDto model, int categoryId, int subcategoryId);
    }
}
