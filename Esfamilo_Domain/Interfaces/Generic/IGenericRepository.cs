using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Interfaces.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Delete(int Id);
        Task<T> Get(int Id);
        Task<IEnumerable<T>> GetAll();

    }
}
