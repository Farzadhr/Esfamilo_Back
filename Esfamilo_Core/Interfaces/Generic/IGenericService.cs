using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Interfaces.Generic
{
    public interface IGenericService<T> where T : class
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int Id);
    }
}
