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
    public class ProductRepository : IProductRepository
    {
        private readonly OrderManagementDbContext _context;

        public ProductRepository(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product entity)
        {
            await _context.Set<Product>().AddAsync(entity);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }

        public void Update(Product entity)
        {
            _context.Set<Product>().Update(entity);
        }
    }
}
