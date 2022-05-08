using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareStore.Webapi.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get_all_categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await _categoryService.GetAllCategories();
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("get_all_subcategories")]
        public async Task<IActionResult> GetAllSubcategories(int categoryId)
        {
            try
            {
                var result = await _categoryService.GetAllSubcategories(categoryId);
                return Created("", result);
            }
            catch (CategoryNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpPost("add_category")]
        public async Task<IActionResult> AddCategory(CategoryDto category)
        {
            try
            {
                var result = await _categoryService.AddCategory(category);
                return Created("", result);
            }
            catch (CategoryAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
        }

        [HttpPost("add_subcategory")]
        public async Task<IActionResult> AddSubcategory(SubcategoryDto subcategory, int categoryId)
        {
            try
            {
                var result = await _categoryService.AddSubcategory(subcategory, categoryId);
                return Created("", result);
            }
            catch (CategoryNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpPost("add_category_range")]
        public async Task<IActionResult> AddCategoryRange(List<CategoryDto> categories)
        {
            try
            {
                var result = await _categoryService.AddCategoryRange(categories);
                return Created("", result);
            }
            catch (CategoryAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
        }

        [HttpPost("add_subcategory_range")]
        public async Task<IActionResult> AddSubcategoryRange(List<SubcategoryDto> subcategories, int categoryId)
        {
            try
            {
                var result = await _categoryService.AddSubcategoryRange(subcategories, categoryId);
                return Created("", result);
            }
            catch (CategoryNotFoundException e)
            {
                return StatusCode(409, e.Message);
            }
        }
    }
}
