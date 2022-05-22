using AutoMapper;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using HardwareStore.Db;
using HardwareStore.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareStore.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Get(int userId)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .Where(o => o.User.Id.Equals(userId))
                .ToListAsync();

            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> Add(int userId, OrderShortDto order)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

            List<OrdersProducts> products = new();
            double totalPrice = 0;

            foreach (var item in order.Products)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(item.ProductId));
                totalPrice += product.Price * item.Count;
                products.Add(new OrdersProducts() { Count = item.Count, Product = product });
                product.TotalCount -= item.Count;

                _context.UserActions.Add(new UserAction() { UserId = userId, ProductId = product.Id });
            }

            var newOrder = new Order() { Date = DateTime.Now.Date, PaymentMethod = order.PaymentMethod, DeliveryType = order.DeliveryType, Price = totalPrice, Products = products, Status = Status.Created, User = user };

            _context.Orders.Add(newOrder);

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(newOrder);
        }
    }
}
