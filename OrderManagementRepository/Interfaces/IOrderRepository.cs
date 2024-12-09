using Microsoft.EntityFrameworkCore;
using OrderManagementData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementRepository.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order entity);

        void Delete(Order entity);

        Task<IReadOnlyList<Order>> GetAllAsync();

        Task<Order> GetByIdAsync(int id);

        void Update(Order entity);

    }
}
