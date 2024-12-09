using Microsoft.EntityFrameworkCore;
using OrderManagementData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementRepository.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IReadOnlyList<Invoice>> GetAllAsync();

        Task<Invoice> GetByIdAsync(int id);

        Task AddAsync(Invoice entity);
    }
}
