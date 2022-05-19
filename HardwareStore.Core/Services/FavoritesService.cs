using AutoMapper;
using HardwareStore.Core.CustomExceptions;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using HardwareStore.Db;
using HardwareStore.Db.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareStore.Core.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FavoritesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FavoritesDto> Get(int userId)
        {
            var favorites = await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(f => f.User.Id.Equals(userId));

            if (favorites == null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

                Favorites newFavorites = new() { User = user, Products = new List<FavoritesProducts>() };

                _context.Favorites.Add(newFavorites);

                await _context.SaveChangesAsync();

                return _mapper.Map<FavoritesDto>(newFavorites);
            }

            return _mapper.Map<FavoritesDto>(favorites);
        }

        public async Task<FavoritesDto> Add(int userId, int productId)
        {
            var favorites = await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(f => f.User.Id.Equals(userId));

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(productId));

            if (product == null) throw new ProductNotFoundException("Товар с таким Id не найден.");

            if (favorites == null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

                Favorites newFavorites = new() { 
                    User = user, 
                    Products = new List<FavoritesProducts>() { new FavoritesProducts() { Product = product } }
                };

                _context.Favorites.Add(newFavorites);

                await _context.SaveChangesAsync();

                return _mapper.Map<FavoritesDto>(newFavorites);
            }

            favorites.Products.Add(new FavoritesProducts() { Product = product });

            await _context.SaveChangesAsync();

            return _mapper.Map<FavoritesDto>(favorites);
        }

        public async Task<FavoritesDto> Remove(int userId, int productId)
        {
            var favorites = await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(f => f.User.Id.Equals(userId));

            var removedProduct = await _context.FavoritesProducts
                .FirstOrDefaultAsync(
                fp => fp.Favorites.User.Id.Equals(userId) && fp.Product.Id.Equals(productId));

            if (removedProduct == null) throw new ProductNotFoundException("Товар с таким Id не найден.");

            favorites.Products.Remove(removedProduct);

            await _context.SaveChangesAsync();

            return _mapper.Map<FavoritesDto>(favorites);
        }
    }
}
