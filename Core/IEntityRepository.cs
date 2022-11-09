using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<int> AddAsync(T entity, string procedure);
        Task<int> DeleteAsync(long id, string procedure);
        Task<int> UpdateAsync(T entity, string procedure);
        Task<IEnumerable<T>> GetAllAsync(string procedure);
        Task<T> GetById(long id, string procedure);

    }
}
