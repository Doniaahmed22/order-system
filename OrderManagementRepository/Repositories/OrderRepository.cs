using Microsoft.EntityFrameworkCore;
using OrderManagementData.Context;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementRepository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementDbContext _context;

        public OrderRepository(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order entity)
        {
            await _context.Set<Order>().AddAsync(entity);
        }

        public void Delete(Order entity)
        {
            _context.Set<Order>().Remove(entity);
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            //return await _context.Set<Order>().Include(o => o.OrderItems).ToListAsync();
            return await _context.Set<Order>().ToListAsync();

        }

        public async Task<Order> GetByIdAsync(int id)
        {
            
           //return await _context.Set<Order>().Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == id);
           return await _context.Set<Order>().FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public void Update(Order entity)
        {
            _context.Set<Order>().Update(entity);
        }


    }
}
