using Domain.Models;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T obj);
        void AddRange(List<T> objs);
        T Update(T obj);
        bool Delete(int id);
    }
}
