using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<int> CountAsync(ISepcifications<TEntity, TKey> spec);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISepcifications<TEntity, TKey> sepc, bool trackChanges = false);
        Task<TEntity> GetAsync(TKey id ); 
        Task<TEntity> GetAsync(ISepcifications<TEntity, TKey> sepc);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
