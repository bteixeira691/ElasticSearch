using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCore.Interfaces
{
    public interface IStudentCore<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> Get(Guid id);
        Task Add(T entity);
        Task Remove(Guid id);
        Task Update(Guid id , T entity);

    }
}
