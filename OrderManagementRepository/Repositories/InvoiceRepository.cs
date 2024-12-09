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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly OrderManagementDbContext _context;

        public InvoiceRepository(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Invoice>> GetAllAsync()
        {
            //return await _context.Invoices
            //.Include(i => i.Order)
            //    .ThenInclude(o => o.Customer)
            //.Include(i => i.Order)
            //    .ThenInclude(o => o.OrderItems).ToListAsync();
            return await _context.Invoices
            .Include(i => i.Order).ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            //return await _context.Invoices
            //.Include(i => i.Order)
            //    .ThenInclude(o => o.Customer)
            //.Include(i => i.Order)
            //    .ThenInclude(o => o.OrderItems)
            //.FirstOrDefaultAsync(i => i.InvoiceId == id);

            return await _context.Invoices
            .Include(i => i.Order)
            .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task AddAsync(Invoice entity)
        {
            await _context.Set<Invoice>().AddAsync(entity);
        }

        
    }
}
