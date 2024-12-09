using Microsoft.EntityFrameworkCore;
using OrderManagementData.Context;
using OrderManagementData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementRepository.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetAllOrdersOfCustomer(int id);
        Task AddAsync(Customer entity);

    }
}
