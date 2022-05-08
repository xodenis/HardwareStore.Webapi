using AutoMapper;
using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using HardwareStore.Db;
using HardwareStore.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HardwareStore.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductFullDto> GetById(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(p => p.Id.Equals(productId));

            if (product == null) throw new ProductNotFoundException("Товар с таким Id не найден.");

            return _mapper.Map<ProductFullDto>(product);
        }

        public async Task<List<ProductFullDto>> GetAll()
        {
            return _mapper.Map<List<ProductFullDto>>(
                await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .ToListAsync());
        }

        public async Task<ProductFullDto> Add(ProductDto model, int categoryId, int subcategoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id.Equals(categoryId));
            var subcategory = await _context.Subcategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategoryId));

            Product product = new()
            {
                Name = model.Name,
                TotalCount = model.TotalCount,
                Price = model.Price,
                PriceInfo = model.PriceInfo,
                Characteristics = model.Characteristics
            };

            product.Category = category ?? throw new CategoryNotFoundException("Категория не найдена.");
            product.Subcategory = subcategory ?? throw new CategoryNotFoundException("Подкатегория не найдена.");

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)model.Image.Length);
            }

            product.Image = imageData;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductFullDto>(product);
        }
    }
}
