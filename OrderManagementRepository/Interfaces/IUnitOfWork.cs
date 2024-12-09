using OrderManagementData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementRepository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
