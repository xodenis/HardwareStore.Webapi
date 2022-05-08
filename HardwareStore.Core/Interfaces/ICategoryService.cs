using HardwareStore.Core.Dto;
using HardwareStore.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryFullDto>> GetAllCategories();
        Task<List<SubcategoryFullDto>> GetAllSubcategories(int categoryId);
        Task<CategoryDto> AddCategory(CategoryDto category);
        Task<SubcategoryDto> AddSubcategory(SubcategoryDto subcategory, int categoryId);
        Task<List<CategoryDto>> AddCategoryRange(List<CategoryDto> categories);
        Task<List<SubcategoryDto>> AddSubcategoryRange(List<SubcategoryDto> subcategories, int categoryId);
    }
}
