using Microsoft.EntityFrameworkCore;
using OrderManagementData.Context;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderManagementRepository.Repositories
{
    public class CustomerRepository : ICustomerRepository 
    {
        private readonly OrderManagementDbContext _context;

        public CustomerRepository(OrderManagementDbContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(Customer entity)
        {
            await _context.Set<Customer>().AddAsync(entity);
        }

        public async Task<Customer> GetAllOrdersOfCustomer(int id)
        {
            return await _context.Set<Customer>()
            .Include(c => c.Orders)
            .ThenInclude(o => o.OrderItems)
            .FirstOrDefaultAsync(c => c.CustomerId == id);

        }

        public void Delete(Customer entity)
        {
            _context.Set<Customer>().Remove(entity);
            _context.SaveChanges();

        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync()
        {
            return await _context.Set<Customer>().ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Set<Customer>().FindAsync(id);

        }

        public void Update(Customer entity)
        {
            _context.Set<Customer>().Update(entity);
            _context.SaveChanges();

        }

    }
}
