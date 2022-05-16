using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HardwareStore.Webapi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get_by_id")]
        public async Task<IActionResult> GetById(int productId)
        {
            try
            {
                var result = await _productService.GetById(productId);
                return Created("", result);
            }
            catch (ProductNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpGet("get_by_category")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            try
            {
                var result = await _productService.GetByCategory(categoryId);
                return Created("", result);
            }
            catch (ProductNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpGet("get_by_subcategory")]
        public async Task<IActionResult> GetBySubcategory(int subcategoryId)
        {
            try
            {
                var result = await _productService.GetBySubcategory(subcategoryId);
                return Created("", result);
            }
            catch (ProductNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _productService.GetAll();
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm]ProductDto product, int categoryId, int subcategoryId)
        {
            try
            {
                var result = await _productService.Add(product, categoryId, subcategoryId);
                return Created("", result);
            }
            catch (AddProductException e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
