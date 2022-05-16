using HardwareStore.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductFullDto> GetById(int productId);
        Task<List<ProductShortDto>> GetByCategory(int categoryId);
        Task<List<ProductShortDto>> GetBySubcategory(int subcategoryId);
        Task<List<ProductShortDto>> GetAll();
        Task<ProductFullDto> Add(ProductDto model, int categoryId, int subcategoryId);
    }
}
