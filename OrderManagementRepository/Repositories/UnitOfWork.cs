using OrderManagementData.Context;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementRepository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDbContext _context;
        private Hashtable _repository;

        public UnitOfWork(OrderManagementDbContext context)
        {
            _context = context;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repository is null)
            {
                _repository = new Hashtable();
            }

            var entityKey = typeof(TEntity).Name;
            if (!_repository.ContainsKey(entityKey))
            {
                //var repositoryType = typeof(GenericRepository<TEntity>);
                //var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity) ), _context);

                if(typeof(TEntity) == typeof(Customer))
                {
                    _repository.Add(entityKey, new CustomerRepository(_context));

                }
                else if (typeof(TEntity) == typeof(Invoice))
                {
                    _repository.Add(entityKey, new InvoiceRepository(_context));

                }
                else
                {
                    _repository.Add(entityKey, new GenericRepository<TEntity>(_context));
                }
            }

            return (IGenericRepository<TEntity>)_repository[entityKey];
        }
    }
}
