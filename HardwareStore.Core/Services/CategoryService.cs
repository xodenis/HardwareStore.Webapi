using AutoMapper;
using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using HardwareStore.Db;
using HardwareStore.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareStore.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryFullDto>> GetAllCategories() 
        {
            return _mapper.Map<List<CategoryFullDto>>(await _context.Categories.Include(c => c.Subcategories).ToListAsync());
        }

        public async Task<List<SubcategoryFullDto>> GetAllSubcategories(int categoryId)
        {
            var category = await _context.Categories.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new CategoryNotFoundException("Категория не найдена.");

            return _mapper.Map<List<SubcategoryFullDto>>(
                await _context.Subcategories
                .Where(sc => sc.Category.Id.Equals(categoryId))
                .ToListAsync());
        }

        public async Task<CategoryDto> AddCategory(CategoryDto category)
        {
            var checkCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);

            if (checkCategory != null)
                throw new CategoryAlreadyExistsException("Категория с таким именем уже существует.");

            await _context.AddAsync(_mapper.Map<Category>(category));
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<SubcategoryDto> AddSubcategory(SubcategoryDto subcategory, int categoryId)
        {
            var category = await _context.Categories.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new CategoryNotFoundException("Категория не найдена.");

            category.Subcategories.Add(_mapper.Map<Subcategory>(subcategory));

            await _context.SaveChangesAsync();

            return subcategory;
        }

        public async Task<List<CategoryDto>> AddCategoryRange(List<CategoryDto> categories)
        {
            var categoriesNames = categories.Select(c => c.Name);

            var checkCategory = _context.Categories.Where(c => categoriesNames.Contains(c.Name));

            if (checkCategory.Count() > 0)
                throw new CategoryAlreadyExistsException("Одна или несколько категорий уже существует.");

            await _context.AddRangeAsync(_mapper.Map<List<Category>>(categories));
            await _context.SaveChangesAsync();

            return categories;
        }

        public async Task<List<SubcategoryDto>> AddSubcategoryRange(List<SubcategoryDto> subcategories, int categoryId)
        {
            var category = await _context.Categories.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new CategoryNotFoundException("Категория не найдена.");

            category.Subcategories.AddRange(_mapper.Map<List<Subcategory>>(subcategories));

            await _context.SaveChangesAsync();

            return subcategories;
        }
    }
}
