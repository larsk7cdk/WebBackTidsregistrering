using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebBackTidsregistrering.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> predicate);

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        void Create(T entity);

        Task CreateAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(int id);

        Task DeleteAsync(int id);

        int Count(Func<T, bool> predicate);
    }
}