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
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CartService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDto> Get(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.User.Id.Equals(userId));

            if (cart == null) throw new EmptyCartException("Корзина пустая.");

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> Add(int userId, int productId, int count)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
            var cart = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.User.Id.Equals(userId));
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(productId));

            if (product == null) throw new ProductNotFoundException("Товар с таким Id не найден.");

            List<CartProducts> productsList = new() { new CartProducts { Count = count, Product = product } };

            if (cart == null)
            {
                _context.Carts.Add(new Cart() { User = user, Products = productsList });
            }
            else
            {
                var checkProduct = await _context.CartProducts.FirstOrDefaultAsync(p => p.Product.Id.Equals(product.Id));

                if (checkProduct != null)
                    checkProduct.Count += count;
                else
                    cart.Products.Add(new CartProducts() { Count = count, Product = product });
            }

            await _context.SaveChangesAsync();
            
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> Remove(int userId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.User.Id.Equals(userId));
            var removedProduct = await _context.CartProducts.FirstOrDefaultAsync(cp => cp.Cart.User.Id.Equals(userId) && cp.Product.Id.Equals(productId));

            if (cart == null) throw new EmptyCartException("Корзина пустая.");

            _context.CartProducts.Remove(removedProduct);

            await _context.SaveChangesAsync();

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> RemoveRange(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.User.Id.Equals(userId));

            if (cart == null) throw new EmptyCartException("Корзина пустая.");

            var removedProducts = await _context.CartProducts.Where(cp => cp.Cart.User.Id.Equals(userId)).ToListAsync();

            _context.CartProducts.RemoveRange(removedProducts);

            await _context.SaveChangesAsync();

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> ChangeProductCount(int userId, int productId, int newCount)
        {
            var cart = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.User.Id.Equals(userId));

            var product = await _context.CartProducts.FirstOrDefaultAsync(cp => cp.Cart.User.Id.Equals(userId) && cp.Product.Id.Equals(productId));

            product.Count = newCount;

            await _context.SaveChangesAsync();

            return _mapper.Map<CartDto>(cart);
        }
    }
}
